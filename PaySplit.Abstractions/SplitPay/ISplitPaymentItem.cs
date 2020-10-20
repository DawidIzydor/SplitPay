using PaySplit.Abstractions.PaySources;

namespace PaySplit.Abstractions.SplitPay
{
    public interface ISplitPaymentItem
    {
        IPaySource PaySource { get; }
        decimal Amount { get; }
    }
}