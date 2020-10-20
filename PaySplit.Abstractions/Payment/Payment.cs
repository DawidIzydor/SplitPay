namespace PaySplit.Abstractions.Payment
{
    public class Payment : IPayment
    {
        public Payment(decimal amount)
        {
            Amount = amount;
        }
        public decimal Amount { get;  }
    }
}