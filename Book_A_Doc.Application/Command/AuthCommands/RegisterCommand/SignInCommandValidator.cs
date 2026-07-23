namespace Book_A_Doc.Application.Command.AuthCommands.RegisterCommand;

using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

/// <summary>
/// Validates the <see cref="SignInCommand"/> request.
///
/// Validation Rules:
/// <list type="bullet">
/// <item>
/// <description>
/// <b>Full Name:</b> Required and must not exceed 100 characters.
/// </description>
/// </item>
/// <item>
/// <description>
/// <b>Email:</b> Required, must be in a valid email format, and only Gmail addresses are allowed.
/// </description>
/// </item>
/// <item>
/// <description>
/// <b>Password:</b> Required, at least 6 characters long, must contain at least one uppercase letter,
/// one lowercase letter, and one special character.
/// </description>
/// </item>
/// <item>
/// <description>
/// <b>Phone Number:</b> Required and must be a valid Egyptian mobile phone number
/// (e.g. 01093308387 or +201093308387).
/// </description>
/// </item>
/// <item>
/// <description>
/// <b>Birth Date:</b> Required and the user must be at least 15 years old.
/// </description>
/// </item>
/// </list>
/// </summary>
public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
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

    private static bool BeAtLeast15YearsOld(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age))
            age--;

        return age >= 15;
    }
}