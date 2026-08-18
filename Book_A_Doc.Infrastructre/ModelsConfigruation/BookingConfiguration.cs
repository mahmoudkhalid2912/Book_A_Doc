using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book_A_Doc.Infrastructre.ModelsConfigruation;
public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
               .IsRequired();

        builder.Property(x => x.Amount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.HasOne(x => x.Patient)
               .WithMany()
               .HasForeignKey(x => x.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Doctor)
               .WithMany()
               .HasForeignKey(x => x.DoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AvailabilitySlot)
               .WithOne(x => x.Booking)
               .HasForeignKey<Booking>(x => x.AvailabilitySlotId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Payment)
               .WithOne(x => x.Booking)
               .HasForeignKey<Payment>(x => x.BookingId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.AvailabilitySlotId)
       .IsUnique()
       .HasFilter(
           $"[Status] IN ({(int)BookingStatus.PendingPayment}, {(int)BookingStatus.Confirmed})");
    }
}
