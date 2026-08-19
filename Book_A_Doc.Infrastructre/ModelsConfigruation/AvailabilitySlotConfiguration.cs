using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book_A_Doc.Infrastructure.Persistence.Configurations;

public class AvailabilitySlotConfiguration : IEntityTypeConfiguration<AvailabilitySlot>
{
    public void Configure(EntityTypeBuilder<AvailabilitySlot> builder)
    {

        builder.Property(x => x.Date)
               .IsRequired();

        builder.Property(x => x.StartTime)
               .IsRequired();

        builder.Property(x => x.EndTime)
               .IsRequired();

        // Doctor → AvailabilitySlots
        builder.HasOne(x => x.Doctor)
               .WithMany()
               .HasForeignKey(x => x.DoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        // AvailabilitySlot → Booking
        builder.HasOne(x => x.Booking)
               .WithOne(x => x.AvailabilitySlot)
               .HasForeignKey<Booking>(x => x.AvailabilitySlotId)
               .OnDelete(DeleteBehavior.Restrict);

        // Prevent duplicate slots for the same doctor and date/time
        builder.HasIndex(x => new
        {
            x.DoctorId,
            x.Date,
            x.StartTime,
            x.EndTime
        })
        .IsUnique();
    }
}