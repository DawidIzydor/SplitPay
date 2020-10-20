using System.Collections.Generic;
using System.Linq;
using PaySplit.Abstractions.Payment;
using PaySplit.Abstractions.PaySources;

namespace PaySplit.Abstractions.SplitPay
{
    public class ProportionalSplitter : IPaymentSplitter
    {
        private readonly IPaySourcesProvider _paySourcesProvider;

        public ProportionalSplitter(IPaySourcesProvider paySourcesProvider)
        {
            _paySourcesProvider = paySourcesProvider;
        }

        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue"></see>.</exception>
        public IEnumerable<ISplitPaymentItem> Split(IPayment payment)
        {
            var paySources = _paySourcesProvider.GetPaySources().ToList();
            var fundsSum = paySources.Select(p => p.Funds).Sum();

            foreach (var paySource in paySources)
            {
                yield return new SplitPaymentItem(paySource, payment.Amount * CalculateSplitPercentage(paySource.Funds, fundsSum));
            }
        }

        private static decimal CalculateSplitPercentage(decimal oneFunds, decimal fundsSum)
        {
            if (fundsSum == 0)
            {
                return 0.5m;
            }

            return oneFunds / fundsSum;
        }
    }
}