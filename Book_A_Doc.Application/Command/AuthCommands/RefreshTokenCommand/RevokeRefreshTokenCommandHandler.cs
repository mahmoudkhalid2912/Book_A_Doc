using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.RefreshTokenCommand;

public class RevokeRefreshTokenCommandHandler(
    IJwtProvider jwtProvider,
    IRefreshTokenService refreshTokenService)
    : IRequestHandler<RevokeRefreshTokenCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        RevokeRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var userId = jwtProvider.ValidateToken(request.Token);

        if (userId is null)
        {
            return Result.Failure<bool>(
                LoginErrors.InvalidCredentials);
        }

        var user =
            await refreshTokenService.FindByIdWithRefreshTokensAsync(
                userId.Value);

        if (user is null)
        {
            return Result.Failure<bool>(
                LoginErrors.InvalidCredentials);
        }

        var result =
            await refreshTokenService.RevokeRefreshTokenAsync(
                user,
                request.RefreshToken);

        if (result.IsFailure)
        {
            return Result.Failure<bool>(result.Error);
        }

        return Result.Success(true);
    }
}
