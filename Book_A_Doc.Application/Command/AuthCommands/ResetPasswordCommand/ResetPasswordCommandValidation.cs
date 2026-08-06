using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Command.AuthCommands.ResetPasswordCommand;

public class ResetPasswordCommandValidation:AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidation()
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

        // NewPassword
        // Password
        RuleFor(x => x.NewPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(AuthErrors.PasswordRequired.Description);

        When(x => !string.IsNullOrWhiteSpace(x.NewPassword), () =>
        {
            RuleFor(x => x.NewPassword)
                .MinimumLength(6)
                .WithMessage(AuthErrors.PasswordTooShort.Description)
                .Matches("[a-z]")
                .WithMessage(AuthErrors.PasswordRequiresLowercase.Description)
                .Matches("[A-Z]")
                .WithMessage(AuthErrors.PasswordRequiresUppercase.Description)
                .Matches(@"[\W_]")
                .WithMessage(AuthErrors.PasswordRequiresSpecialCharacter.Description);
        });
    }
}
