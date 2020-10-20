using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using PaySplit.DAL.Models;
using PaySplit.DAL.Sqlite;

namespace PaySplit.Wpf
{
    /// <summary>
    ///     Logika interakcji dla klasy AddPersonWindow.xaml
    /// </summary>
    public partial class AddModifyPersonWindow
    {
        private readonly string _name;

        public AddModifyPersonWindow(string name = "")
        {
            _name = name;
            InitializeComponent();

            if (EditMode())
            {
                NameTextBox.IsEnabled = false;
                AddButton.Hide();
                ModifyButton.Show();
            }
            else
            {
                NameTextBox.IsEnabled = true;
                AddButton.Show();
                ModifyButton.Hide();
            }
        }

        private bool EditMode()
        {
            return !string.IsNullOrWhiteSpace(_name);
        }

        private async void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            decimal money;
            try
            {
                money = decimal.Parse(MoneyTextBox.Text);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show($"Money is out of range: {ex.Message}");
                return;
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Money has wrong format: {ex.Message}");
                return;
            }

            try
            {
                LockUi(true);

                await UpdateAsync(_name, money).ConfigureAwait(true);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Problem saving data: {ex.Message}");
            }
            finally
            {
                LockUi(true);
            }
        }

        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
        ///     An error is encountered while saving to the
        ///     database.
        /// </exception>
        private static async Task UpdateAsync(string name, decimal money)
        {
            await using var db = new PaySplitSqliteDbContext();
            var person = await db.Persons.FindAsync(name).ConfigureAwait(false);
            person.Funds = money;
            await db.SaveChangesAsync().ConfigureAwait(false);
        }

        public override async void EndInit()
        {
            base.EndInit();

            // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
            await UpdateMoneyTextBoxAsync().ConfigureAwait(true);
        }

        private async Task UpdateMoneyTextBoxAsync()
        {
            if (EditMode())
            {
                await using var db = new PaySplitSqliteDbContext();
                var user = await db.FindAsync<DbPerson>(_name).ConfigureAwait(true);
                MoneyTextBox.Text = user.Funds.ToString(CultureInfo.InvariantCulture);
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTextBox.Text;
            decimal money;
            try
            {
                money = decimal.Parse(MoneyTextBox.Text);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show($"Money is out of range: {ex.Message}");
                return;
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Money has wrong format: {ex.Message}");
                return;
            }

            try
            {
                LockUi(true);
                await Task.Run( ()=>AddAsync(name, money).ConfigureAwait(false)).ConfigureAwait(true);
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

        private void LockUi(bool doLock)
        {
            MoneyTextBox.IsEnabled = !doLock;
            NameTextBox.IsEnabled = !EditMode() && !doLock;
        }

        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">An error is encountered while saving to the
        ///     database.</exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.</exception>
        private static async Task AddAsync(string name, decimal money)
        {
            await using var db = new PaySplitSqliteDbContext();
            await db.Persons.AddAsync(new DbPerson
            {
                Name = name,
                Funds = money
            }).ConfigureAwait(false);
            await db.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}