using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaySplit.Abstractions.Payment;
using PaySplit.Abstractions.SplitPay;
using PaySplit.DAL;
using PaySplit.DAL.Sqlite;

namespace PaySplit.Wpf
{
    public static class SplitPaymentCalculator
    {
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue"></see>.</exception>
        public static async Task<IEnumerable<ISplitPaymentItem>> CalculateSplitPaymentAsync(decimal moneyAmount)
        {
            await using var db = new PaySplitSqliteDbContext();
            var calc = new ProportionalSplitter(new DatabasePersonPaySourceProvider(db));

            return calc.Split(moneyAmount.AsPayment()).ToList();
        }
    }
}