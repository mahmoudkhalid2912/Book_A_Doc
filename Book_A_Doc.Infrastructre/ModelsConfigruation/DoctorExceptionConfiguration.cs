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
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.IsAvailable)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.DoctorId,
            x.Date
        })
        .IsUnique();

        builder.HasOne<Doctor>()
            .WithMany()
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}