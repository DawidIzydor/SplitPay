using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaySplit.DAL.Models;

namespace PaySplit.DAL
{
    public class DbPaymentConfiguration : IEntityTypeConfiguration<DbPayment>
    {
        public void Configure(EntityTypeBuilder<DbPayment> entity)
        {
  
        }
    }
}