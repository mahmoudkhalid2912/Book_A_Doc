using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Command.AuthCommands.VerifyOtpCommand;

public class VerifyOtpCommandValidation:AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpCommandValidation()
    {
        // Email
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(AuthErrors.EmailRequired.Description);

        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(AuthErrors.InvalidEmailFormat.Description)
                .Must(email => email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                .WithMessage(AuthErrors.GmailOnly.Description);
        });

        //Code
        RuleFor(x => x.Code).
            Cascade(CascadeMode.Stop).NotEmpty();
            
            
    }
}
