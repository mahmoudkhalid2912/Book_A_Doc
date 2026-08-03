using FluentValidation;

namespace Book_A_Doc.Application.Command.AuthCommands.RefreshTokenQuery;

public class RevokeRefreshTokenCommandValidator:AbstractValidator<RevokeRefreshTokenCommand>
{
    public RevokeRefreshTokenCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x=> x.RefreshToken).NotEmpty();
    }
}
