using Book_A_Doc.Application.Command.AuthCommands.LoginCommand;
using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using Book_A_Doc.Infrastructre.JwtServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Book_A_Doc.Application.Command.AuthCommands.RefreshTokenCommand;

public class RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager,IJwtProvider jwtProvider) : IRequestHandler<RefreshTokenCommand, Result<LoginResponse>>
{
    public readonly int RefreshTokenExpiryDays = 30;
    public async Task<Result<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Validate the JWT token and get the user ID
        var UserId = jwtProvider.ValidateToken(request.Token);

        if(UserId is null)
        {
            return Result.Failure<LoginResponse>(LoginErrors.InvalidCredentials);
        }

        // Get the user from the database along with their refresh tokens
        var user = await userManager.Users
       .Include(x => x.RefreshTokens)
      .FirstOrDefaultAsync(x => x.Id == UserId.Value, cancellationToken);

        if (user == null)
        {
            return Result.Failure<LoginResponse>(LoginErrors.InvalidCredentials);
        }

        // Find the refresh token in the user's refresh tokens
        var refreshToken = user.RefreshTokens
       .SingleOrDefault(x => x.Token == request.RefreshToken);

        if (refreshToken is null || !refreshToken.IsActive)
        {
            return Result.Failure<LoginResponse>(LoginErrors.InvalidRefreshToken);
        }

        refreshToken.RevokedOn = DateTime.UtcNow;

        // Generate a new JWT token
        var (newJwtToken, ExpiresIn) = jwtProvider.GenerateJwtToken(user);
        var newRefreshToken = jwtProvider.GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);

        // Save the refresh token and its expiration to the user entity
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration
        });
        await userManager.UpdateAsync(user);

        // Return the new JWT token and refresh token to the client

        var response = new LoginResponse
        {
            UserId = user.Id,
            Email = user.Email!,
            FullName = user.FullName,
            Token = newJwtToken,
            TokenExpireIn=ExpiresIn,
            RefreshToken = newRefreshToken,
            RefreshTokenExpiration = refreshTokenExpiration
        };

        return Result.Success<LoginResponse>(response,AuthMessages.RefreshTokenSuccess);
    }
}
