using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.LoginQuery;

public class LoginQueryCommandHandler(
    IIdentityService identityService,
    IJwtProvider jwtProvider,
    IAuthenticationService authenticationService,
    IRefreshTokenService refreshTokenService)
    : IRequestHandler<LoginQueryCommand, Result<LoginResponse>>
{
    private const int RefreshTokenExpiryDays = 30;

    public async Task<Result<LoginResponse>> Handle(
        LoginQueryCommand request,
        CancellationToken cancellationToken)
    {
        // Check if user exists
        var user = await identityService.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Failure<LoginResponse>(LoginErrors.InvalidCredentials);
        }

        // Validate credentials
        var loginResult = await authenticationService.PasswordSignInAsync(
            user,
            request.Password);

        if (loginResult.IsFailure)
        {
            return Result.Failure<LoginResponse>(loginResult.Error);
        }

        // Generate JWT Token
        var (token, expiresIn) = jwtProvider.GenerateJwtToken(user);

        // Generate Refresh Token
        var refreshToken = jwtProvider.GenerateRefreshToken();
        var refreshTokenExpiration =
            DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);

        // Save Refresh Token
        var addRefreshTokenResult = await refreshTokenService.AddRefreshTokenAsync(
            user,
            refreshToken,
            refreshTokenExpiration);

        if (addRefreshTokenResult.IsFailure)
        {
            return Result.Failure<LoginResponse>(addRefreshTokenResult.Error);
        }

        // Build response
        var response = new LoginResponse
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email!,
            Token = token,
            TokenExpireIn = expiresIn,
            RefreshToken = refreshToken,
            RefreshTokenExpiration = refreshTokenExpiration
        };

        return Result.Success(response, AuthMessages.LoginSuccess);
    }
}