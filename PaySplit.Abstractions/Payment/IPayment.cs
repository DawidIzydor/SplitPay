namespace PaySplit.Abstractions.Payment
{
    public interface IPayment
    {
        decimal Amount { get; }
    }
}