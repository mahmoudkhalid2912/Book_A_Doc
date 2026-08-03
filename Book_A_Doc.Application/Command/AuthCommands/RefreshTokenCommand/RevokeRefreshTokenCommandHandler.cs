using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Book_A_Doc.Application.Command.AuthCommands.RefreshTokenQuery;

public class RevokeRefreshTokenCommandHandler(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IRequestHandler<RevokeRefreshTokenCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Validate the JWT token and get the user ID
        var UserId = jwtProvider.ValidateToken(request.Token);

        if (UserId is null)
        {
            return Result.Failure<bool>(LoginErrors.InvalidCredentials);
        }

        // Get the user from the database along with their refresh tokens
        var user = await userManager.Users
       .Include(x => x.RefreshTokens)
      .FirstOrDefaultAsync(x => x.Id == UserId.Value, cancellationToken);

        if (user == null)
        {
            return Result.Failure<bool>(LoginErrors.InvalidCredentials);
        }

        // Find the refresh token in the user's refresh tokens
        var refreshToken = user.RefreshTokens
       .SingleOrDefault(x => x.Token == request.RefreshToken);

        if (refreshToken is null || !refreshToken.IsActive)
        {
            return Result.Failure<bool>(LoginErrors.InvalidRefreshToken);
        }

        refreshToken.RevokedOn = DateTime.UtcNow;
        await userManager.UpdateAsync(user);
        return Result.Success(true);
    }
}
