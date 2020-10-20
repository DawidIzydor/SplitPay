using System.Linq;
using PaySplit.Abstractions.Payment;
using PaySplit.Abstractions.PaySources;
using PaySplit.Abstractions.SplitPay;
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

    public class ProportionalSplitterTests
    {
        [Fact]
        public void ShouldSplitBetweenTwo()
        {
            var amount = 100;
            var person1Pay = 60;
            var person2Pay = 40;
            var person1 = new Person(){Funds = 600};
            var person2 = new Person(){Funds = 400};
            var es = new ProportionalSplitter(new PaySourcesProvider(person1, person2));

            var results = es.Split(new Payment(amount)).ToList();

            var person1CalculatedPayment = results.Find(p => p.PaySource == person1);
            var person2CalculatedPayment = results.Find(p => p.PaySource == person2);

            Assert.Equal(person1Pay, person1CalculatedPayment.Amount);
            Assert.Equal(person2Pay, person2CalculatedPayment.Amount);
        }

        [Fact]
        public void ShouldSplitEquallyIfBothHaveZero()
        {
            var amount = 100;
            var person1Pay = 50;
            var person2Pay = 50;
            var person1 = new Person() { Funds = 0 };
            var person2 = new Person() { Funds = 0 };
            var es = new ProportionalSplitter(new PaySourcesProvider(person1, person2));

            var results = es.Split(new Payment(amount)).ToList();

            var person1CalculatedPayment = results.Find(p => p.PaySource == person1);
            var person2CalculatedPayment = results.Find(p => p.PaySource == person2);

            Assert.Equal(person1Pay, person1CalculatedPayment.Amount);
            Assert.Equal(person2Pay, person2CalculatedPayment.Amount);

        }

    }
}