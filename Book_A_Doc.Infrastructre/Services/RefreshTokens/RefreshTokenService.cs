using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Book_A_Doc.Infrastructre.Services.RefreshTokens;

public class RefreshTokenService(
    UserManager<ApplicationUser> userManager)
    : IRefreshTokenService
{
    public async Task<Result> AddRefreshTokenAsync(
        ApplicationUser user,
        string refreshToken,
        DateTime expiresOn)
    {
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = expiresOn
        });


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


    public async Task<ApplicationUser?> FindByIdWithRefreshTokensAsync(
        Guid userId)
    {
        return await userManager.Users
            .Include(x => x.RefreshTokens)
            .FirstOrDefaultAsync(x => x.Id == userId);
    }


    public Task<Result> ValidateRefreshTokenAsync(
        ApplicationUser user,
        string refreshToken)
    {
        var token = user.RefreshTokens
            .SingleOrDefault(x => x.Token == refreshToken);


        if (token is null || !token.IsActive)
        {
            return Task.FromResult(
                Result.Failure(
                    AuthErrors.InvalidRefreshToken));
        }


        return Task.FromResult(
            Result.Success());
    }


    public async Task<Result> RevokeRefreshTokenAsync(
        ApplicationUser user,
        string refreshToken)
    {
        var token = user.RefreshTokens
            .SingleOrDefault(x => x.Token == refreshToken);


        if (token is null)
        {
            return Result.Failure(
                AuthErrors.InvalidRefreshToken);
        }


        token.RevokedOn = DateTime.UtcNow;


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
}