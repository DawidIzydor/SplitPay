using PaySplit.Abstractions.Payment;
using Xunit;

namespace PaySplit.Tests
{
    public class PaymentExtTests
    {
        [Fact]
        public void ShouldParsePaymentFromDecimal()
        {
            var dec = 12.3m;

            var result = dec.AsPayment();

            Assert.Equal(dec, result.Amount);
        }
    }
}