namespace PaySplit.Abstractions.PaySources
{
    public class Person : IPaySource
    {
        public string Name { get; set; }
        public decimal Funds { get; set; }
    }
}