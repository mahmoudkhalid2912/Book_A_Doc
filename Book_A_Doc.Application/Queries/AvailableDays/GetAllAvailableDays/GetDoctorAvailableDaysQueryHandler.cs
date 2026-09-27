using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Queries.AvailableDays;

public class GetDoctorAvailableDaysQueryHandler(
    IDoctorAvailabilityRepository repository)
    : IRequestHandler<
        GetDoctorAvailableDaysQuery,
        Result<IEnumerable<AvailableDaysResponseDto>>>
{
    public async Task<Result<IEnumerable<AvailableDaysResponseDto>>> Handle(
        GetDoctorAvailableDaysQuery request,
        CancellationToken cancellationToken)
    {
        var availabilities = await repository.GetByDoctorIdAsync(
            request.DoctorId,
            cancellationToken);

        if(!availabilities.Any())
        {
           return Result.Failure<IEnumerable<AvailableDaysResponseDto>>
                (DoctorAvailabilityErrors.NoAvailableDaysFoundForThisDoctor);
        }

        var response = availabilities.Select(a => new AvailableDaysResponseDto
        {
            Id = a.Id,
            DayOfWeek = a.DayOfWeek,
            StartTime = a.StartTime,
            EndTime = a.EndTime,
            SlotDurationInMinutes = a.SlotDurationInMinutes
        });

        return Result<IEnumerable<AvailableDaysResponseDto>>.Success(response,DoctorAvailabilityMessages.AvailbaleDaysRetrievedSuccessfully);
    }
}