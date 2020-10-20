using System.Collections.Generic;
using PaySplit.Abstractions.Payment;

namespace PaySplit.Abstractions.SplitPay
{
    public interface IPaymentSplitter
    {
        IEnumerable<ISplitPaymentItem> Split(IPayment payment);
    }
}