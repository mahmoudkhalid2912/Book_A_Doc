using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book_A_Doc.Infrastructure.Persistence.Configurations;

public class DoctorAvailabilityConfiguration
    : IEntityTypeConfiguration<DoctorAvailability>
{
    public void Configure(EntityTypeBuilder<DoctorAvailability> builder)
    {

        builder.Property(x => x.DayOfWeek)
               .IsRequired();

        builder.Property(x => x.StartTime)
               .IsRequired();

        builder.Property(x => x.EndTime)
               .IsRequired();

        builder.Property(x => x.SlotDurationInMinutes)
               .IsRequired();

        builder.Property(x => x.IsActive)
               .IsRequired();

        builder.HasOne(x => x.Doctor)
               .WithMany()
               .HasForeignKey(x => x.DoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.DoctorId,
            x.DayOfWeek,
            x.StartTime,
            x.EndTime
        })
        .IsUnique();
    }
}