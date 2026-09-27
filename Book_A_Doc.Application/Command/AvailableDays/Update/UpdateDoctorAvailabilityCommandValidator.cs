using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Command.AvailableDays.Update;

public class UpdateDoctorAvailabilityCommandValidator
    : AbstractValidator<UpdateDoctorAvailabilityCommand>
{
    public UpdateDoctorAvailabilityCommandValidator()
    {
        RuleFor(x => x.DayOfWeek)
            .IsInEnum()
            .WithMessage(
                DoctorAvailabilityErrors
                    .InvalidDayOfWeek
                    .Description);

        RuleFor(x => x)
            .Must(x => x.StartTime < x.EndTime)
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

        RuleFor(x => x)
            .Must(x =>
            {
                if (x.SlotDurationInMinutes <= 0 ||
                    x.StartTime >= x.EndTime)
                {
                    return false;
                }

                var duration =
                    (x.EndTime.ToTimeSpan() -
                     x.StartTime.ToTimeSpan())
                    .TotalMinutes;

                return duration >= x.SlotDurationInMinutes;
            })
            .WithMessage(
                DoctorAvailabilityErrors
                    .SlotDurationExceedsAvailabilityDuration
                    .Description);

        RuleFor(x => x)
            .Must(x =>
            {
                if (x.SlotDurationInMinutes <= 0 ||
                    x.StartTime >= x.EndTime)
                {
                    return false;
                }

                var duration =
                    (x.EndTime.ToTimeSpan() -
                     x.StartTime.ToTimeSpan())
                    .TotalMinutes;

                return duration % x.SlotDurationInMinutes == 0;
            })
            .WithMessage(
                DoctorAvailabilityErrors
                    .SlotDurationMustDivideExactly
                    .Description);
    }
}