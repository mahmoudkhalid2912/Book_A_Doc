using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Command.Doctor.Add;

public class AddDoctorCommandValidation:AbstractValidator<AddDoctorCommand>
{
    public AddDoctorCommandValidation()
    {

        RuleFor(d => d.Email).NotNull().WithMessage(AuthErrors.EmailRequired.Description);
        RuleFor(x => x.Email)
               .EmailAddress()
               .WithMessage(AuthErrors.InvalidEmailFormat.Description)
               .Must(email => email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
               .WithMessage(AuthErrors.GmailOnly.Description);

        RuleFor(d=>d.Password)
            .NotNull().WithMessage(AuthErrors.PasswordRequired.Description)
            .MinimumLength(6).WithMessage(AuthErrors.PasswordTooShort.Description)
            .Matches("[a-z]").WithMessage(AuthErrors.PasswordRequiresLowercase.Description)
            .Matches("[A-Z]").WithMessage(AuthErrors.PasswordRequiresUppercase.Description)
            .Matches(@"[\W_]").WithMessage(AuthErrors.PasswordRequiresSpecialCharacter.Description);


        RuleFor(d => d.FullName)
            .NotNull().MaximumLength(100)
            .WithMessage(UserErrors.DoctorNameMustBeBetween5And100Characters.Description)
            .MinimumLength(5)
            .WithMessage(UserErrors.DoctorNameMustBeBetween5And100Characters.Description);

        RuleFor(d => d.Specialty)
            .NotNull().
            MinimumLength(5).
            WithMessage(UserErrors. SpecialtyMustBeBetween5And100Characters.Description)
            .MaximumLength(100)
            .WithMessage(UserErrors.SpecialtyMustBeBetween5And100Characters.Description);

        RuleFor(d => d.YearsOfExperience)
            .NotNull()
            .LessThanOrEqualTo((byte)50).
            WithMessage(UserErrors.YearsOfExperienceMustBeLessThanOrEqualTo50.Description);

        RuleFor(d => d.SessionPrice)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Session price must be greater than 0.");

        RuleFor(d=>d.SessionPrice)
            .LessThanOrEqualTo(10000)
             .WithMessage(UserErrors.SessionPriceMustBeLessThanOrEqualTo10000.Description);

        RuleFor(d => d.PhoneNumber)
                .Matches(@"^(?:\+20|0)1[0125]\d{8}$")
                .WithMessage(UserErrors.InvalidEgyptianPhoneNumber.Description);

        When(d=>d.BirthDate.HasValue, () =>
        {
            RuleFor(x => x.BirthDate)
                .Must(BeAtLeast22YearsOld)
                .WithMessage(UserErrors.DoctorMustbeAtLeast22YearsOld.Description);
        });

    }

    private static bool BeAtLeast22YearsOld(DateOnly? birthDate)
    {
        if (!birthDate.HasValue)
            return false;

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var age = today.Year - birthDate.Value.Year;

        if (birthDate.Value > today.AddYears(-age))
            age--;

        return age >= 22;
    }
}
