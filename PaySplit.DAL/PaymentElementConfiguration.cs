using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaySplit.DAL.Models;

namespace PaySplit.DAL
{
    public class PaymentElementConfiguration : IEntityTypeConfiguration<PaymentElement>
    {
        public void Configure(EntityTypeBuilder<PaymentElement> entity)
        {
            entity.HasKey(p => p.Id);

            entity.HasOne(p => p.Payment)
                .WithMany(p => p.PaymentElements)
                .HasForeignKey(p => p.PaymentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.Person)
                .WithMany(p => p.PaymentElements)
                .HasForeignKey(p=>p.PersonName)
                .IsRequired();
        }
    }
}