using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaySplit.DAL.Models;

namespace PaySplit.DAL
{
    public class DbPersonConfiguration : IEntityTypeConfiguration<DbPerson>
    {
        public void Configure(EntityTypeBuilder<DbPerson> entity)
        {
            entity.HasKey(p => p.Name);
        }
    }
}