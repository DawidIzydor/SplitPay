namespace PaySplit.Abstractions.Payment
{
    public static class PaymentExt
    {
        public static IPayment AsPayment(this decimal @this)
        {
            return new ImmutablePayment(@this);
        }
    }
}