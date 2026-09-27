using Book_A_Doc.Application.Queries.AvailableDays.GetDoctorAvailableDays;
using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using MediatR;

namespace Book_A_Doc.Application.Queries.DoctorAvailability;

public class GetDoctorAvailabilityByIdQueryHandler(
    IDoctorAvailabilityRepository repository)
    : IRequestHandler<
        GetDoctorAvailabilityByIdQuery,
        Result<DoctorAvailabilityResponseDto>>
{
    public async Task<Result<DoctorAvailabilityResponseDto>> Handle(
        GetDoctorAvailabilityByIdQuery request,
        CancellationToken cancellationToken)
    {
        var availability = await repository.GetByIdAsync(
            request.AvailabilityId, request.DoctorId, cancellationToken);

        if (availability is null)
        {
            return Result.Failure<DoctorAvailabilityResponseDto>(
                DoctorAvailabilityErrors.AvailabilityNotFound);
        }

        var response = new DoctorAvailabilityResponseDto
        {
            Id = availability.Id,
            DoctorId = availability.DoctorId,
            DoctorName = availability.Doctor.FullName,
            DayOfWeek = availability.DayOfWeek,
            StartTime = availability.StartTime,
            EndTime = availability.EndTime,
            SlotDurationInMinutes =
                availability.SlotDurationInMinutes,
            IsActive = availability.IsActive
        };

        return Result.Success(response);
    }
}