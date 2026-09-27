using FluentValidation;

namespace Book_A_Doc.Application.Command.AvailableDays.Delete;

public class DeleteDoctorAvailabilityCommandValidator
    : AbstractValidator<DeleteDoctorAvailabilityCommand>
{
    public DeleteDoctorAvailabilityCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.DoctorId)
            .NotEmpty();
    }
}