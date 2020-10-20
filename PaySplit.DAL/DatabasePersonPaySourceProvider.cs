using System.Collections.Generic;
using PaySplit.Abstractions.PaySources;

namespace PaySplit.DAL
{
    public class DatabasePersonPaySourceProvider : IPaySourcesProvider
    {
        private readonly PaySplitBaseContext _dbContext;

        public DatabasePersonPaySourceProvider(PaySplitBaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<IPaySource> GetPaySources()
        {
            return _dbContext.Persons;
        }
    }
}