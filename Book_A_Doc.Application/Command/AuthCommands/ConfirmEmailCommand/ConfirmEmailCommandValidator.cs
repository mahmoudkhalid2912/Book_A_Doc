using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Command.AuthCommands.ConfirmEmailCommand;

public class ConfirmEmailCommandValidator:AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage(EmailConfirmationError.UserIdIsRequired.Description);
        RuleFor(x => x.Token).NotEmpty().WithMessage(EmailConfirmationError.TokenIsRequired.Description);
    }
}
