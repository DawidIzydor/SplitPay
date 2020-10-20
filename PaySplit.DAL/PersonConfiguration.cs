using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaySplit.DAL.Models;

namespace PaySplit.DAL
{
    public class PersonConfiguration : IEntityTypeConfiguration<DbPerson>
    {
        public void Configure(EntityTypeBuilder<DbPerson> builder)
        {
            builder.HasKey(p => p.Name);
        }
    }
}