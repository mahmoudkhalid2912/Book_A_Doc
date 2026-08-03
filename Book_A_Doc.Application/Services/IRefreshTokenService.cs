using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;

public interface IRefreshTokenService
{
    Task<Result> AddRefreshTokenAsync(
        ApplicationUser user,
        string refreshToken,
        DateTime expiresOn);


    Task<ApplicationUser?> FindByIdWithRefreshTokensAsync(
        Guid userId);


    Task<Result> ValidateRefreshTokenAsync(
        ApplicationUser user,
        string refreshToken);


    Task<Result> RevokeRefreshTokenAsync(
        ApplicationUser user,
        string refreshToken);
}