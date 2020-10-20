using System;
using System.Collections.Generic;
using System.Linq;
using PaySplit.Abstractions;
using PaySplit.Abstractions.Payment;
using PaySplit.Abstractions.PaySources;
using PaySplit.Abstractions.SplitPay;
using Xunit;

namespace PaySplit.Tests
{
    public class EqualSplitterTests
    {
        [Fact]
        public void ShouldSplitBetweenTwo()
        {
            var amount = 100;
            var person1Pay = 50;
            var person2Pay = 50;
            var person1 = new Person();
            var person2 = new Person();
            var es = new EqualSplitter(new PaySourcesProvider(person1, person2));

            var results = es.Split(new ImmutablePayment(amount)).ToList();

            var person1CalculatedPayment = results.Find(p => p.PaySource == person1);
            var person2CalculatedPayment = results.Find(p => p.PaySource == person2);

            Assert.Equal(person1Pay, person1CalculatedPayment.Amount);
            Assert.Equal(person2Pay, person2CalculatedPayment.Amount);
        }
    }
}
