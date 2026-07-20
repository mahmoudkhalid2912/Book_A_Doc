using Book_A_Doc.Domain.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book_A_Doc.Infrastructre.ModelsConfigruation;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");

        builder.HasKey(d => d.UserId);

        
        builder.HasOne(d => d.User)
            .WithOne(u => u.Doctor)
            .HasForeignKey<Doctor>(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(d => d.Description)
            .HasMaxLength(1000);

        builder.Property(d => d.YearsOfExperience)
            .IsRequired();

        builder.Property(d => d.SessionPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(d => d.AverageRating)
            .HasColumnType("decimal(3,2)")
            .HasDefaultValue(0);

        builder.Property(d => d.ReviewCount)
            .HasDefaultValue(0);
    }
}
