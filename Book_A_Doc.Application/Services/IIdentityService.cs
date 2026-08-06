using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;

namespace Book_A_Doc.Application.Services;

public interface IIdentityService
{
    Task<bool> EmailExistsAsync(string email);

    Task<Result> CreateUserAsync(
        ApplicationUser user,
        string password);

    Task<ApplicationUser?> FindByIdAsync(
        Guid userId);
    
    Task<ApplicationUser?> FindByEmailAsync(
        string email);
    Task<Result> UpdateUserAsync(
        ApplicationUser user);

    Task<Result> ChangePasswordAsync(
        ApplicationUser user,
        string oldPassword,
        string newPassword);

    Task<Result> ResetPasswordAsync(string Email,string NewPassword);
}