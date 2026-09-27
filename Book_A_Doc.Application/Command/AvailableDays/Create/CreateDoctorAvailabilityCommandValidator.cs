using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Command.AvailableDays.Create;

public class CreateDoctorAvailabilityCommandValidator
    : AbstractValidator<CreateDoctorAvailabilityCommand>
{
    public CreateDoctorAvailabilityCommandValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage(
                DoctorAvailabilityErrors
                    .DoctorIdIsRequired
                    .Description);

        RuleFor(x => x.DayOfWeek)
            .IsInEnum()
            .WithMessage(
                DoctorAvailabilityErrors
                    .InvalidDayOfWeek
                    .Description);

        RuleFor(x => x.StartTime)
            .Must((command, startTime) =>
                startTime < command.EndTime)
            .WithMessage(
                DoctorAvailabilityErrors
                    .InvalidTimeRange
                    .Description);

        RuleFor(x => x.SlotDurationInMinutes)
            .GreaterThan(0)
            .WithMessage(
                DoctorAvailabilityErrors
                    .InvalidSlotDuration
                    .Description);

        RuleFor(x => x.SlotDurationInMinutes)
            .Must((command, duration) =>
            {
                var availabilityDuration =
                    (command.EndTime.ToTimeSpan()
                    - command.StartTime.ToTimeSpan())
                    .TotalMinutes;

                return duration <= availabilityDuration;
            })
            .When(x => x.SlotDurationInMinutes > 0)
            .WithMessage(
                DoctorAvailabilityErrors
                    .SlotDurationExceedsAvailabilityDuration
                    .Description);

        RuleFor(x => x.SlotDurationInMinutes)
            .Must((command, duration) =>
            {
                var availabilityDuration =
                    (command.EndTime.ToTimeSpan()
                    - command.StartTime.ToTimeSpan())
                    .TotalMinutes;

                return availabilityDuration % duration == 0;
            })
            .When(x =>
                x.SlotDurationInMinutes > 0 &&
                x.StartTime < x.EndTime)
            .WithMessage(
                DoctorAvailabilityErrors
                    .SlotDurationMustDivideExactly
                    .Description);
    }
}