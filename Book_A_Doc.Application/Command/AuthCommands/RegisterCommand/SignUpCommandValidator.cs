namespace Book_A_Doc.Application.Command.AuthCommands.RegisterCommand;

using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;


public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {

        
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage(RegisterErrors.FullNameRequired.Description)
            .MaximumLength(100)
            .WithMessage(RegisterErrors.FullNameTooLong.Description);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(RegisterErrors.EmailRequired.Description)
            .EmailAddress()
            .WithMessage(RegisterErrors.InvalidEmailFormat.Description)
            .Must(email => email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            .WithMessage(RegisterErrors.GmailOnly.Description);

        RuleFor(x => x.Password)
     .Cascade(CascadeMode.Continue)
     .NotEmpty()
         .WithMessage(RegisterErrors.PasswordRequired.Description)
     .MinimumLength(6)
         .WithMessage(RegisterErrors.PasswordTooShort.Description)
     .Matches("[a-z]")
         .WithMessage(RegisterErrors.PasswordRequiresLowercase.Description)
     .Matches("[A-Z]")
         .WithMessage(RegisterErrors.PasswordRequiresUppercase.Description)
     .Matches(@"[\W_]")
         .WithMessage(RegisterErrors.PasswordRequiresSpecialCharacter.Description);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(RegisterErrors.PhoneNumberRequired.Description)
            .Matches(@"^(?:\+20|0)1[0125]\d{8}$")
            .WithMessage(RegisterErrors.InvalidEgyptianPhoneNumber.Description);

        RuleFor(x => x.BirthDay)
            .NotEmpty()
            .WithMessage(RegisterErrors.BirthDateRequired.Description)
            .Must(BeAtLeast15YearsOld)
            .WithMessage(RegisterErrors.UserMustBeAtLeast15YearsOld.Description);
    }

    private static bool BeAtLeast15YearsOld(DateOnly? birthDate)
    {
        if (birthDate is null)
            return false;

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var age = today.Year - birthDate.Value.Year;

        if (birthDate.Value > today.AddYears(-age))
            age--;

        return age >= 15;
    }
}