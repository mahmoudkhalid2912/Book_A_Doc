using Book_A_Doc.Application.Command.AuthCommands.LoginQuery;
using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.RefreshTokenQuery;

public class RefreshTokenCommandHandler(
    IJwtProvider jwtProvider,
    IRefreshTokenService refreshTokenService)
    : IRequestHandler<RefreshTokenCommand, Result<LoginResponse>>
{
    private const int RefreshTokenExpiryDays = 30;

    public async Task<Result<LoginResponse>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        // Validate JWT
        var userId = jwtProvider.ValidateToken(request.Token);

        if (userId is null)
        {
            return Result.Failure<LoginResponse>(LoginErrors.InvalidCredentials);
        }

        // Get user with refresh tokens
        var user = await refreshTokenService.FindByIdWithRefreshTokensAsync(userId.Value);

        if (user is null)
        {
            return Result.Failure<LoginResponse>(LoginErrors.InvalidCredentials);
        }

        // Validate refresh token
        var validateRefreshTokenResult =
            await refreshTokenService.ValidateRefreshTokenAsync(
                user,
                request.RefreshToken);

        if (validateRefreshTokenResult.IsFailure)
        {
            return Result.Failure<LoginResponse>(validateRefreshTokenResult.Error);
        }

        // Revoke current refresh token
        var revokeResult = await refreshTokenService.RevokeRefreshTokenAsync(
            user,
            request.RefreshToken);

        if (revokeResult.IsFailure)
        {
            return Result.Failure<LoginResponse>(revokeResult.Error);
        }

        // Generate new tokens
        var (newJwtToken, expiresIn) = jwtProvider.GenerateJwtToken(user);

        var newRefreshToken = jwtProvider.GenerateRefreshToken();
        var refreshTokenExpiration =
            DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);

        var addRefreshTokenResult =
            await refreshTokenService.AddRefreshTokenAsync(
                user,
                newRefreshToken,
                refreshTokenExpiration);

        if (addRefreshTokenResult.IsFailure)
        {
            return Result.Failure<LoginResponse>(addRefreshTokenResult.Error);
        }

        var response = new LoginResponse
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email!,
            Token = newJwtToken,
            TokenExpireIn = expiresIn,
            RefreshToken = newRefreshToken,
            RefreshTokenExpiration = refreshTokenExpiration
        };

        return Result.Success(response, AuthMessages.RefreshTokenSuccess);
    }
}