using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Book_A_Doc.Infrastructre.Services.Identity;

public class IdentityService(
    UserManager<ApplicationUser> userManager)
    : IIdentityService
{
    public async Task<ApplicationUser?> FindByIdAsync(Guid userId)
        => await userManager.FindByIdAsync(userId.ToString());


    public async Task<ApplicationUser?> FindByEmailAsync(string email)
        => await userManager.FindByEmailAsync(email);


    public async Task<bool> EmailExistsAsync(string email)
        => await userManager.Users
            .AnyAsync(x => x.Email == email);


    public async Task<Result> CreateUserAsync(
        ApplicationUser user,
        string password)
    {
        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();

            return Result.Failure(
                new Error(
                    error.Code,
                    error.Description,
                    StatusCodes.BadRequest));
        }

        return Result.Success();
    }


    public async Task<Result> UpdateUserAsync(
        ApplicationUser user)
    {
        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();

            return Result.Failure(
                new Error(
                    error.Code,
                    error.Description,
                    StatusCodes.BadRequest));
        }

        return Result.Success();
    }

    public async Task<Result> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
    {
        var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();

            return Result.Failure(
                new Error(
                    error.Code,
                    error.Description,
                    StatusCodes.BadRequest));
        }

        return Result.Success(AccountMessages.PasswordChangedSuccessfully);
    }
}