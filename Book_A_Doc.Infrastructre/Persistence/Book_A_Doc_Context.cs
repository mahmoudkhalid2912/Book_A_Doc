using Book_A_Doc.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Book_A_Doc.Infrastructre.Persistence;

public class Book_A_Doc_Context(DbContextOptions<Book_A_Doc_Context> dbContextOptions):IdentityDbContext<ApplicationUser,ApplicationRole, Guid>(dbContextOptions)
{
   public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }

    public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(Book_A_Doc_Context).Assembly);
    }
}
