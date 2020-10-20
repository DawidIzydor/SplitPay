namespace PaySplit.Abstractions.Payment
{
    public class ImmutablePayment : IPayment
    {
        public ImmutablePayment(decimal amount)
        {
            Amount = amount;
        }
        public decimal Amount { get;  }
    }
}