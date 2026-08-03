namespace Book_A_Doc.Application.Command.AuthCommands.LoginCommand;

using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;
public class LoginCommandValidation:AbstractValidator<LoginCommand>
{
    public LoginCommandValidation()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage(LoginErrors.EmailIsRequried.Description)
            .EmailAddress().WithMessage(LoginErrors.InvalidEmailFormat.Description);

        RuleFor(x => x.Password).NotEmpty().WithMessage(LoginErrors.PasswordIsRequried.Description);
    }
}
