using Microsoft.EntityFrameworkCore;

namespace PaySplit.DAL.Sqlite
{
    public class PaySplitSqliteDbContext : PaySplitBaseContext
    {
        private readonly string _sqliteDbName;

        public PaySplitSqliteDbContext(string sqliteDbName = "SplitPay.db")
        {
            _sqliteDbName = sqliteDbName;
        }

        public PaySplitSqliteDbContext(DbContextOptions options, string sqliteDbName = "SplitPay.db") : base(options)
        {
            _sqliteDbName = sqliteDbName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Filename={_sqliteDbName}");
        }
    }
}
