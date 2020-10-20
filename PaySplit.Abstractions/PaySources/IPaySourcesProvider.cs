using System.Collections.Generic;

namespace PaySplit.Abstractions.PaySources
{
    public interface IPaySourcesProvider
    {
        IEnumerable<IPaySource> GetPaySources();
    }
}