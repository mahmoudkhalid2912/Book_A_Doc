using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Command.AuthCommands.ResendConfiramationEmailCommand;

public class ResendEmailConfirmationCommandValidator : AbstractValidator<ResendEmailConfiramtionCommand>
{
    public ResendEmailConfirmationCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(EmailConfirmationError.EmailIsRequired.Description)
            .EmailAddress()
            .WithMessage(EmailConfirmationError.InvalidEmail.Description)
            .Must(email => email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            .WithMessage(EmailConfirmationError.InvalidEmail.Description);
    }
}