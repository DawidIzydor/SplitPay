using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PaySplit.Abstractions.Payment;
using PaySplit.Abstractions.PaySources;
using PaySplit.Abstractions.SplitPay;
using PaySplit.DAL;
using PaySplit.DAL.Models;
using PaySplit.DAL.Sqlite;

namespace PaySplit.Wpf
{
    public partial class SplitPayWindow
    {
        private SaveWrapper _saveWrapper;
        private string _splitterToUse = nameof(EqualSplit);

        public SplitPayWindow()
        {
            InitializeComponent();
            AllForOneNameLock(false);
        }

        /// <exception cref="T:System.OverflowException">
        ///     represents a number less than
        ///     <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.
        /// </exception>
        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            decimal moneyAmount;
            try
            {
                moneyAmount = decimal.Parse(MoneyTextBox.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Wrong money format {ex.Message}");
                return;
            }

            var allForOne = AllForOneName.Text;

            try
            {
                LockUi(true);
                var saveWrapper = await Task.Run(async () =>
                    {
                        await using var db = new PaySplitSqliteDbContext();
                        var calc = GetPaymentSplitter(db, allForOne);

                        var paymentItems = calc.Split(moneyAmount.AsPayment()).ToList();
                        return new SaveWrapper
                        {
                            Items = paymentItems,
                            GeneratorName = calc.GetType().Name,
                            Amount = moneyAmount
                        };
                    })
                    .ConfigureAwait(true);

                ResultsDataGrid.ItemsSource =
                    SplitPayDataGridItemsFactory.CreateSplitPayDataGridItems(saveWrapper.Items).ToList();
                ResultsDataGrid.Show();
                _saveWrapper = saveWrapper;
                SaveButton.Show();
            }
            finally
            {
                LockUi(false);
            }
        }

        /// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
        private IPaymentSplitter GetPaymentSplitter(PaySplitBaseContext db, string allForOne)
        {
            return _splitterToUse switch
            {
                nameof(EqualSplit) => new EqualSplitter(new DatabasePersonPaySourceProvider(db)),
                nameof(ProportionalSplit) => new ProportionalSplitter(new DatabasePersonPaySourceProvider(db)),
                nameof(AllForOne) => new ProportionalSplitter(new OneProvider(db, allForOne)),
                _ => throw new ArgumentOutOfRangeException(_splitterToUse)
            };
        }

        private void AllForOneNameLock(bool doLock)
        {
            if (AllForOneName != null)
            {
                AllForOneName.IsEnabled = !doLock && (AllForOne?.IsChecked ?? false);
            }
        }

        private void LockUi(bool doLock)
        {
            CalculateButton.IsEnabled = !doLock;
            MoneyTextBox.IsEnabled = !doLock;

            AllForOne.IsEnabled = !doLock;
            EqualSplit.IsEnabled = !doLock;
            ProportionalSplit.IsEnabled = !doLock;
            AllForOneNameLock(doLock);

            SaveButton.IsEnabled = _saveWrapper != default && !doLock;
        }

        private void SplitMode_OnChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioSender)
            {
                _splitterToUse = radioSender.Name;
            }

            AllForOneNameLock(false);
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_saveWrapper == null)
            {
                MessageBox.Show("Nothing to save!");
                return;
            }

            var saveWrapper = _saveWrapper;

            try
            {
                LockUi(true);
                await AddPaymentAsync(saveWrapper).ConfigureAwait(true);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Problem saving data: {ex.Message}");
            }
            finally
            {
                LockUi(false);
            }
        }

        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the
        ///     database.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        private static async Task AddPaymentAsync(SaveWrapper saveWrapper)
        {
            await using var db = new PaySplitSqliteDbContext();
            var newPayment = new DbPayment
            {
                Amount = saveWrapper.Amount,
                Generator = saveWrapper.GeneratorName
            };
            await db.Payments.AddAsync(newPayment).ConfigureAwait(false);
            await db.PaymentElements.AddRangeAsync(saveWrapper.Items.Select(i => new PaymentElement
            {
                Amount = i.Amount,
                Payment = newPayment,
                PersonName = ((Person) i.PaySource).Name
            })).ConfigureAwait(false);

            await db.SaveChangesAsync().ConfigureAwait(false);
        }

        private class SaveWrapper
        {
            public IEnumerable<ISplitPaymentItem> Items { get; set; }
            public string GeneratorName { get; set; }
            public decimal Amount { get; set; }
        }


        private class OneProvider : IPaySourcesProvider
        {
            private readonly PaySplitBaseContext _dbContext;
            private readonly string _payName;

            public OneProvider(PaySplitBaseContext dbContext, string payName)
            {
                _dbContext = dbContext;
                _payName = payName;
            }

            public IEnumerable<IPaySource> GetPaySources()
            {
                yield return _dbContext.Persons.Find(_payName);
            }
        }
    }
}