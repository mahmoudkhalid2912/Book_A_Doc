namespace Book_A_Doc.Application.Quiers.AuthQuery.LoginQuery;

using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;
public class LoginQueryCommandValidation:AbstractValidator<LoginQueryCommand>
{
    public LoginQueryCommandValidation()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage(LoginErrors.EmailIsRequried.Description)
            .EmailAddress().WithMessage(LoginErrors.InvalidEmailFormat.Description);

        RuleFor(x => x.Password).NotEmpty().WithMessage(LoginErrors.PasswordIsRequried.Description);
    }
}
