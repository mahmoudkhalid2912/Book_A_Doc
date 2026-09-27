using Book_A_Doc.Domain.Models;
using Book_A_Doc.Domain.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book_A_Doc.Infrastructure.Persistence.Configurations;

public class DoctorExceptionConfiguration
    : IEntityTypeConfiguration<DoctorException>
{
    public void Configure(EntityTypeBuilder<DoctorException> builder)
    {
        builder.ToTable("DoctorExceptions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DoctorId)
            .IsRequired(false);

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasMaxLength(500)
            .IsRequired();

        // Prevent duplicate exceptions
        // for the same doctor on the same date.
        builder.HasIndex(x => new
        {
            x.DoctorId,
            x.Date
        })
        .IsUnique();

        // Only one global exception is allowed per date.
        builder.HasIndex(x => x.Date)
            .IsUnique()
            .HasFilter("[DoctorId] IS NULL");

        builder.HasOne<Doctor>()
            .WithMany()
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}