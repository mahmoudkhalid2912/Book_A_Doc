using FluentValidation;

namespace Book_A_Doc.Application.Command.AuthCommands.RefreshTokenQuery;

public class RefreshTokenCommandValidator:AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();

        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}
