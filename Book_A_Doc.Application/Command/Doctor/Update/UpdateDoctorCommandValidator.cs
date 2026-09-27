using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Command.Doctor.Update;

public class UpdateDoctorCommandValidator
    : AbstractValidator<UpdateDoctorCommand>
{
    public UpdateDoctorCommandValidator()
    {
        // Full Name
        When(x => !string.IsNullOrWhiteSpace(x.FullName), () =>
        {
            RuleFor(x => x.FullName)
                .Must(name => name!.Length >= 5)
                .WithMessage(
                    UserErrors.DoctorNameMustBeBetween5And100Characters.Description)

                .MaximumLength(100)
                .WithMessage(
                    UserErrors.DoctorNameMustBeBetween5And100Characters.Description);
        });

        // Specialty
        When(x => !string.IsNullOrWhiteSpace(x.Specialty), () =>
        {
            RuleFor(x => x.Specialty)
                .Must(specialty => specialty!.Length >= 5)
                .WithMessage(
                    UserErrors.SpecialtyMustBeBetween5And100Characters.Description)

                .MaximumLength(100)
                .WithMessage(
                    UserErrors.SpecialtyMustBeBetween5And100Characters.Description);
        });

        // Description
        When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
        {
            RuleFor(x => x.Description)
                .MaximumLength(1000);
        });

        // Years Of Experience
        When(x => x.YearsOfExperience.HasValue, () =>
        {
            RuleFor(x => x.YearsOfExperience)
                .Must(years => years <= 50)
                .WithMessage(
                    UserErrors.YearsOfExperienceMustBeLessThanOrEqualTo50.Description);
        });

        // Session Price
        When(x => x.SessionPrice.HasValue, () =>
        {
            RuleFor(x => x.SessionPrice)
                .Must(price => price > 0)
                .WithMessage(
                    UserErrors.SessionPriceMustBeGreaterThanZero.Description)

                .Must(price => price <= 10000)
                .WithMessage(
                    UserErrors.SessionPriceMustBeLessThanOrEqualTo10000.Description);
        });

        // Phone Number
        When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber), () =>
        {
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^(?:\+20|0)1[0125]\d{8}$")
                .WithMessage(
                    UserErrors.InvalidEgyptianPhoneNumber.Description);
        });

        // Birth Date
        When(x => x.BirthDate.HasValue, () =>
        {
            RuleFor(x => x.BirthDate)
                .Must(BeAtLeast22YearsOld)
                .WithMessage(
                    UserErrors.DoctorMustbeAtLeast22YearsOld.Description);
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