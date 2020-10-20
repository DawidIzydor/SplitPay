using PaySplit.Abstractions.PaySources;

namespace PaySplit.Abstractions.SplitPay
{
    public class SplitPaymentItem : ISplitPaymentItem
    {
        public SplitPaymentItem(IPaySource paySource, decimal amount)
        {
            PaySource = paySource;
            Amount = amount;
        }
        public IPaySource PaySource { get; }
        public decimal Amount { get; }
    }
}