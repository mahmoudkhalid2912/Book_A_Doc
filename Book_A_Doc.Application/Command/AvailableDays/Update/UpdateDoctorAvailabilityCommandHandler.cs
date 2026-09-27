using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Command.AvailableDays.Update;

public class UpdateDoctorAvailabilityCommandHandler(
    IDoctorAvailabilityRepository repository)
    : IRequestHandler<
        UpdateDoctorAvailabilityCommand,
        Result>
{
    public async Task<Result> Handle(
        UpdateDoctorAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        var availability =
            await repository.GetTrackedByIdAsync(
                request.Id,
                request.DoctorId,
                cancellationToken);

        if (availability is null)
        {
            return Result.Failure(
                DoctorAvailabilityErrors
                    .AvailabilityNotFound);
        }

        if (request.IsActive)
        {
            var hasOverlap =
                await repository.HasOverlapAsync(
                    request.DoctorId,
                    request.DayOfWeek,
                    request.StartTime,
                    request.EndTime,
                    request.Id,
                    cancellationToken);

            if (hasOverlap)
            {
                return Result.Failure(
                    DoctorAvailabilityErrors
                        .AvailabilityOverlapsExistingAvailability);
            }
        }

        availability.DayOfWeek =
            request.DayOfWeek;

        availability.StartTime =
            request.StartTime;

        availability.EndTime =
            request.EndTime;

        availability.SlotDurationInMinutes =
            request.SlotDurationInMinutes;

        availability.IsActive =
            request.IsActive;

        return Result.Success(DoctorAvailabilityMessages.AvailabilityUpdated);
    }
}