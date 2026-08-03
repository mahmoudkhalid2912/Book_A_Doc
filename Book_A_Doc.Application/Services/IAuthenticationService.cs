using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;

namespace Book_A_Doc.Application.Services;

public interface IAuthenticationService
{
    Task<string> GenerateEmailConfirmationTokenAsync(
        ApplicationUser user);


    Task<Result> ConfirmEmailAsync(
        ApplicationUser user,
        string token);


    Task<Result> PasswordSignInAsync(
        ApplicationUser user,
        string password);
}
