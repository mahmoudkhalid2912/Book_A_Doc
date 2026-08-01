using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Command.AuthCommands.RegisterCommand;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        // Full Name
        RuleFor(x => x.FullName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(RegisterErrors.FullNameRequired.Description);

        When(x => !string.IsNullOrWhiteSpace(x.FullName), () =>
        {
            RuleFor(x => x.FullName)
                .MaximumLength(100)
                .WithMessage(RegisterErrors.FullNameTooLong.Description);
        });

        // Email
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(RegisterErrors.EmailRequired.Description);

        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(RegisterErrors.InvalidEmailFormat.Description)
                .Must(email => email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                .WithMessage(RegisterErrors.GmailOnly.Description);
        });

        // Password
        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(RegisterErrors.PasswordRequired.Description);

        When(x => !string.IsNullOrWhiteSpace(x.Password), () =>
        {
            RuleFor(x => x.Password)
                .MinimumLength(6)
                .WithMessage(RegisterErrors.PasswordTooShort.Description)
                .Matches("[a-z]")
                .WithMessage(RegisterErrors.PasswordRequiresLowercase.Description)
                .Matches("[A-Z]")
                .WithMessage(RegisterErrors.PasswordRequiresUppercase.Description)
                .Matches(@"[\W_]")
                .WithMessage(RegisterErrors.PasswordRequiresSpecialCharacter.Description);
        });

        // Phone Number
        RuleFor(x => x.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(RegisterErrors.PhoneNumberRequired.Description);

        When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber), () =>
        {
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^(?:\+20|0)1[0125]\d{8}$")
                .WithMessage(RegisterErrors.InvalidEgyptianPhoneNumber.Description);
        });

        // Birth Date
        RuleFor(x => x.BirthDay)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(RegisterErrors.BirthDateRequired.Description);

        When(x => x.BirthDay.HasValue, () =>
        {
            RuleFor(x => x.BirthDay)
                .Must(BeAtLeast15YearsOld)
                .WithMessage(RegisterErrors.UserMustBeAtLeast15YearsOld.Description);
        });
    }

    private static bool BeAtLeast15YearsOld(DateOnly? birthDate)
    {
        if (!birthDate.HasValue)
            return false;

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var age = today.Year - birthDate.Value.Year;

        if (birthDate.Value > today.AddYears(-age))
            age--;

        return age >= 15;
    }
}