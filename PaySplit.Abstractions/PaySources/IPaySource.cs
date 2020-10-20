namespace PaySplit.Abstractions.PaySources
{
    public interface IPaySource
    {
        decimal Funds { get; }
    }
}