using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Command.Account.UpdateUserProfileCommand;

public class UpdateUserCommandValidation:AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100)
            .WithMessage(AccountErrors.UserNameIsTooLong.Description);

        When(x => x.BirthDate.HasValue, () =>
        {
            RuleFor(x => x.BirthDate)
                .Must(BeAtLeast15YearsOld)
                .WithMessage(AccountErrors.UserMustBeAtLeast15YearsOld.Description);
        });

        When(x => !string.IsNullOrWhiteSpace(x.Phone), () =>
        {
            RuleFor(x => x.Phone)
                .Matches(@"^(?:\+20|0)1[0125]\d{8}$")
                .WithMessage(AccountErrors.InvalidEgyptianPhoneNumber.Description);
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
