using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Microsoft.AspNetCore.Identity;

namespace Book_A_Doc.Infrastructre.Services.Authentication;

public class AuthenticationService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
    : IAuthenticationService
{
    public async Task<string> GenerateEmailConfirmationTokenAsync(
        ApplicationUser user)
    {
        return await userManager.GenerateEmailConfirmationTokenAsync(user);
    }


    public async Task<Result> ConfirmEmailAsync(
        ApplicationUser user,
        string token)
    {
        var result = await userManager.ConfirmEmailAsync(user, token);

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


    public async Task<Result> PasswordSignInAsync(
        ApplicationUser user,
        string password)
    {
        var result = await signInManager.PasswordSignInAsync(
            user,
            password,
            isPersistent: false,
            lockoutOnFailure: false);


        if (result.Succeeded)
        {
            return Result.Success();
        }


        if (result.IsNotAllowed)
        {
            return Result.Failure(
                AuthErrors.EmailNotConfirmed);
        }


        return Result.Failure(
            AuthErrors.InvalidCredentials);
    }
}