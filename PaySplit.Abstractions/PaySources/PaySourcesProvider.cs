using System.Collections.Generic;

namespace PaySplit.Abstractions.PaySources
{
    public class PaySourcesProvider : IPaySourcesProvider
    {
        private readonly IPaySource[] _persons;

        public PaySourcesProvider(params IPaySource[] persons)
        {
            _persons = persons;
        }

        public IEnumerable<IPaySource> GetPaySources()
        {
            return _persons;
        }
    }
}