using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using PaySplit.Abstractions.SplitPay;

namespace PaySplit.Wpf
{
    public partial class SplitPayWindow
    {
        public SplitPayWindow()
        {
            InitializeComponent();
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

            IEnumerable<ISplitPaymentItem> splitPaymentItems;
            try
            {
                LockUi(true);
                splitPaymentItems = await Task.Run(() => SplitPaymentCalculator.CalculateSplitPaymentAsync(moneyAmount))
                    .ConfigureAwait(true);
            }
            finally
            {
                LockUi(false);
            }

            ResultsDataGrid.ItemsSource = SplitPayDataGridItemsFactory.CreateSplitPayDataGridItems(splitPaymentItems);
        }

        private void LockUi(bool doLock)
        {
            CalculateButton.IsEnabled = !doLock;
            MoneyTextBox.IsEnabled = !doLock;
        }
    }
}