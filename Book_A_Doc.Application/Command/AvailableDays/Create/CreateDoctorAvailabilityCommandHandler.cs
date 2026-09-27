using Book_A_Doc.Application.Command.AvailableDays.Create;
using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

public class CreateDoctorAvailabilityCommandHandler(
    IDoctorAvailabilityRepository repository)
    : IRequestHandler<CreateDoctorAvailabilityCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateDoctorAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        var hasOverlap = await repository.HasOverlapAsync(
            request.DoctorId,
            request.DayOfWeek,
            request.StartTime,
            request.EndTime,
            cancellationToken: cancellationToken);

        if (hasOverlap)
        {
            return Result.Failure<Guid>(
                DoctorAvailabilityErrors
                    .AvailabilityOverlapsExistingAvailability);
        }

        var availability = new DoctorAvailability
        {
            Id = Guid.NewGuid(),
            DoctorId = request.DoctorId,
            DayOfWeek = request.DayOfWeek,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            SlotDurationInMinutes =
                request.SlotDurationInMinutes,
            IsActive = true
        };

        await repository.AddAsync(
            availability,
            cancellationToken);

        return Result.Success(availability.Id,DoctorAvailabilityMessages.AvailabilityCreated);
    }
}