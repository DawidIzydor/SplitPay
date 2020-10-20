using System.Collections.Generic;
using System.Linq;
using PaySplit.Abstractions.PaySources;
using PaySplit.Abstractions.SplitPay;

namespace PaySplit.Wpf
{
    internal static class SplitPayDataGridItemsFactory
    {
        public static IEnumerable<SplitPayDataGridItem> CreateSplitPayDataGridItems(
            IEnumerable<ISplitPaymentItem> splitPaymentItems)
        {
            return splitPaymentItems.Select(splitPaymentItem => new SplitPayDataGridItem
            {
                Name = ((Person) splitPaymentItem.PaySource).Name,
                Amount = $"{splitPaymentItem.Amount:0.00} zł",
                Percent = $"{100 * splitPaymentItem.Amount / splitPaymentItem.PaySource.Funds:0.00}%"
            });
        }
    }
}