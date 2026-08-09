using Book_A_Doc.Domain.Consts;
using Book_A_Doc.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Book_A_Doc.Infrastructure.Persistence.Seed;

public static class RoleSeeder
{
    public static async Task SeedAsync(
        RoleManager<ApplicationRole> roleManager)
    {
        var adminRole = new ApplicationRole
        {
            Id = Guid.Parse(DefaultRoles.AdminId),
            Name = DefaultRoles.Admin,
            NormalizedName = DefaultRoles.Admin.ToUpperInvariant(),
            IsDefault = false,
            IsDeleted = false
        };

        if (!await roleManager.RoleExistsAsync(DefaultRoles.Admin))
        {
            await roleManager.CreateAsync(adminRole);
        }

        var doctorRole = new ApplicationRole
        {
            Id = Guid.Parse(DefaultRoles.DoctorId),
            Name = DefaultRoles.Doctor,
            NormalizedName = DefaultRoles.Doctor.ToUpperInvariant(),
            IsDefault = false,
            IsDeleted = false
        };

        if (!await roleManager.RoleExistsAsync(DefaultRoles.Doctor))
        {
            await roleManager.CreateAsync(doctorRole);
        }

        var patientRole = new ApplicationRole
        {
            Id = Guid.Parse(DefaultRoles.PatientId),
            Name = DefaultRoles.Patient,
            NormalizedName = DefaultRoles.Patient.ToUpperInvariant(),
            IsDefault = true,
            IsDeleted = false
        };

        if (!await roleManager.RoleExistsAsync(DefaultRoles.Patient))
        {
            await roleManager.CreateAsync(patientRole);
        }
    }


}