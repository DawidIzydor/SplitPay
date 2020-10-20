using System.Collections.Generic;
using System.Linq;
using PaySplit.Abstractions.Payment;
using PaySplit.Abstractions.PaySources;

namespace PaySplit.Abstractions.SplitPay
{
    public class EqualSplitter : IPaymentSplitter
    {
        private readonly IPaySourcesProvider _paySourcesProvider;

        public EqualSplitter(IPaySourcesProvider paySourcesProvider)
        {
            _paySourcesProvider = paySourcesProvider;
        }

        public IEnumerable<ISplitPaymentItem> Split(IPayment payment)
        {
            var paySources = _paySourcesProvider.GetPaySources().ToList();
            foreach (var paySource in paySources)
            {
                yield return new SplitPaymentItem(paySource, payment.Amount/paySources.Count);
            }
        }
    }
}