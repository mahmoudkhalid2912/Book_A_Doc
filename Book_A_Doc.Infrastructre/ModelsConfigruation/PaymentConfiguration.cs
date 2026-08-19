

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book_A_Doc.Infrastructre.ModelsConfigruation;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {

        builder.Property(x => x.Amount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.Status)
               .IsRequired();

        builder.Property(x => x.TransactionId)
               .HasMaxLength(200);

        builder.Property(x => x.PaymentUrl)
               .HasMaxLength(1000);

        builder.HasOne(x => x.Booking)
               .WithOne(x => x.Payment)
               .HasForeignKey<Payment>(x => x.BookingId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.TransactionId)
               .IsUnique()
               .HasFilter("[TransactionId] IS NOT NULL");
    }
}
