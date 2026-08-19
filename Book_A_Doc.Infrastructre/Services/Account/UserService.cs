using Book_A_Doc.Application.Queries.Account.GetUserProfile;
using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Book_A_Doc.Infrastructre.Services.Account;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{

    public async Task<UserProfileDto> GetUserProfileAsync(Guid userId)
    {
        var user = await userManager.Users
            .Where(x => x.Id == userId&&x.IsDeleted==false)
            .Select(x => new
            {
                x.FullName,
                x.Email,
                x.PhoneNumber,
                x.BirthDate
            })
            .SingleAsync();

        return new UserProfileDto
        {
            Name = user.FullName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber!,
            Age = CalculateAge(user.BirthDate)
        };
    }

    private static int CalculateAge(DateOnly? birthDate)
    {
        if (birthDate is null)
            return 0;

        var today = DateOnly.FromDateTime(DateTime.Today);

        var age = today.Year - birthDate.Value.Year;

        if (today < birthDate.Value.AddYears(age))
            age--;

        return age;
    }
}