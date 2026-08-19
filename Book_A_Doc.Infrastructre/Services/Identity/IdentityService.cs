using Book_A_Doc.Application.Queries.Account.GetAllUsers;
using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using Book_A_Doc.Infrastructre.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Book_A_Doc.Infrastructre.Services.Identity;

public class IdentityService(
    UserManager<ApplicationUser> userManager
    , Book_A_Doc_Context context,
    RoleManager<ApplicationRole> roleManager)
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

    public async Task<Result> ResetPasswordAsync(string Email, string NewPassword)
    {
        var user = await userManager.FindByEmailAsync(Email);

        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);


        user.PasswordHash = userManager
            .PasswordHasher
            .HashPassword(user, NewPassword);


        var result = await userManager.UpdateAsync(user);


        return result.Succeeded
            ? Result.Success()
            : Result.Failure(AuthErrors.PasswordResetFailed);
    }

    public async Task AddToRoleAsync(ApplicationUser user, string Role)
    => await userManager.AddToRoleAsync(user, Role);

    public async Task<Result> CreateUserWithRoleAsync(
     ApplicationUser user,
     string password,
     string role)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync();

        try
        {
            // 1. Create User
            var createResult =
                await userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                await transaction.RollbackAsync();

                return Result.Failure(
                    AuthErrors.UserCreationFailed);
            }

            // 2. Assign Role
            var roleResult =
                await userManager.AddToRoleAsync(user, role);

            if (!roleResult.Succeeded)
            {
                await transaction.RollbackAsync();

                return Result.Failure(
                    AuthErrors.RoleAssignmentFailed);
            }

            // 3. Commit Transaction
            await transaction.CommitAsync();

            return Result.Success();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        => await userManager.GetRolesAsync(user);

    public async Task<List<ApplicationRole>> GetAllRolesAsync(
      CancellationToken cancellationToken)
    {
        return await roleManager.Roles
            .ToListAsync(cancellationToken);
    }

    public async Task<ApplicationRole?> GetRoleByIdAsync(Guid id)
    => await roleManager.FindByIdAsync(id.ToString());

    public async Task<List<UseresResponse>> GetAllUsersWithRolesAsync(CancellationToken cancellationToken)
        => await( from user in userManager.Users
                  join userRole in context.UserRoles on user.Id equals userRole.UserId
                  join role in context.Roles on userRole.RoleId equals role.Id
                  select new UseresResponse
                  {
                      Id = user.Id,
                      Name = user.FullName,
                      Email = user.Email!,
                      PhoneNumber = user.PhoneNumber!,
                      RoleNames = new List<string> { role.Name! }
                  })
            .ToListAsync(cancellationToken);

    public Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    => userManager.UpdateAsync(user);
}
        
