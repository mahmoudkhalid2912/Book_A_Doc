using Book_A_Doc.Domain.Consts;
using Book_A_Doc.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Book_A_Doc.Infrastructre.Persistence.Seed;

public static class UserSeeder
{
    public static async Task SeedAsync(
        UserManager<ApplicationUser> userManager,
        Book_A_Doc_Context context)
    {
        await SeedAdminAsync(userManager);
        await SeedDoctorsAsync(userManager, context);
    }

    private static async Task SeedAdminAsync(
        UserManager<ApplicationUser> userManager)
    {
        var existingAdmin = await userManager.FindByEmailAsync(
            DefaultUser.AdminEmail);

        if (existingAdmin is not null)
            return;

        var admin = new ApplicationUser
        {
            Id = Guid.Parse(DefaultUser.AdminId),
            UserName = DefaultUser.AdminEmail,
            Email = DefaultUser.AdminEmail,
            EmailConfirmed = true,
            SecurityStamp = DefaultUser.AdminSecurityStamp,
            ConcurrencyStamp = DefaultUser.AdminConcurrencyStamp,
            FullName = DefaultUser.AdminFullName
        };

        var result = await userManager.CreateAsync(
            admin,
            DefaultUser.AdminPassword);

        if (!result.Succeeded)
        {
            throw new Exception(
                string.Join(
                    ", ",
                    result.Errors.Select(x => x.Description)));
        }

        var roleResult = await userManager.AddToRoleAsync(
            admin,
            DefaultRoles.Admin);

        if (!roleResult.Succeeded)
        {
            throw new Exception(
                string.Join(
                    ", ",
                    roleResult.Errors.Select(x => x.Description)));
        }
    }

    private static async Task SeedDoctorsAsync(
        UserManager<ApplicationUser> userManager,
        Book_A_Doc_Context context)
    {
        var doctors = new List<(string Email, string FullName, string Password, string Specialty, string Description, byte YearsOfExperience, decimal SessionPrice)>
        {
            (
                "ahmed.hassan@bookadoc.com",
                "Dr. Ahmed Hassan",
                "P@ssw0rd123",
                "Cardiology",
                "Specialist in cardiovascular diseases and heart health.",
                12,
                500m
            ),
            (
                "mohamed.ali@bookadoc.com",
                "Dr. Mohamed Ali",
                "P@ssw0rd123",
                "Dermatology",
                "Specialist in skin, hair, and nail diseases.",
                8,
                350m
            ),
            (
                "omar.khaled@bookadoc.com",
                "Dr. Omar Khaled",
                "P@ssw0rd123",
                "Pediatrics",
                "Specialist in pediatric care and children's health.",
                10,
                400m
            ),
            (
                "youssef.mahmoud@bookadoc.com",
                "Dr. Youssef Mahmoud",
                "P@ssw0rd123",
                "Orthopedics",
                "Specialist in bones, joints, and musculoskeletal conditions.",
                15,
                600m
            )
        };

        foreach (var doctorData in doctors)
        {
            var existingUser = await userManager.FindByEmailAsync(
                doctorData.Email);

            if (existingUser is not null)
                continue;

            var doctorUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = doctorData.Email,
                Email = doctorData.Email,
                EmailConfirmed = true,
                FullName = doctorData.FullName
            };

            var result = await userManager.CreateAsync(
                doctorUser,
                doctorData.Password);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(
                        ", ",
                        result.Errors.Select(x => x.Description)));
            }

            var roleResult = await userManager.AddToRoleAsync(
                doctorUser,
                DefaultRoles.Doctor);

            if (!roleResult.Succeeded)
            {
                throw new Exception(
                    string.Join(
                        ", ",
                        roleResult.Errors.Select(x => x.Description)));
            }

            var doctor = new Doctor
            {
                UserId = doctorUser.Id,
                Specialty = doctorData.Specialty,
                Description = doctorData.Description,
                YearsOfExperience = doctorData.YearsOfExperience,
                SessionPrice = doctorData.SessionPrice
            };

            context.Doctors.Add(doctor);
        }

        await context.SaveChangesAsync();
    }
}