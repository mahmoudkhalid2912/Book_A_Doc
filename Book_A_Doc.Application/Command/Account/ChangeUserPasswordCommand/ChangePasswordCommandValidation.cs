using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Command.Account.ChangeUserPasswordCommand;

public class ChangePasswordCommandValidation:AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangePasswordCommandValidation()
    {

        ApplyPasswordRules(RuleFor(x => x.OldPassword));

        ApplyPasswordRules(RuleFor(x => x.NewPassword));

        RuleFor(x=>x.NewPassword).NotEqual(x => x.OldPassword)
            .WithMessage(AccountErrors.NewPasswordCannotBeSameAsOld.Description);
    }

    private static void ApplyPasswordRules(
       IRuleBuilderInitial<ChangeUserPasswordCommand, string> rule)
    {
        rule.Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(AccountErrors.PasswordRequired.Description)
            .MinimumLength(6)
            .WithMessage(AccountErrors.PasswordTooShort.Description)
            .Matches("[a-z]")
            .WithMessage(AccountErrors.PasswordRequiresLowercase.Description)
            .Matches("[A-Z]")
            .WithMessage(AccountErrors.PasswordRequiresUppercase.Description)
            .Matches(@"[\W_]")
            .WithMessage(AccountErrors.PasswordRequiresSpecialCharacter.Description);
    }
}
