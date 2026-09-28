using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.Interfaces.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using MediatR;

namespace Book_A_Doc.Application.Queries.Slots.GetAll;

public class GetAvailiableSlotsQueryHandler(
    IIdentityService identityService,
    ISlotRepository repository)
    : IRequestHandler<GetAvailableSlotsQuery, Result<IEnumerable<AvailableSlotsDtoResponse>>>
{
    public async Task<Result<IEnumerable<AvailableSlotsDtoResponse>>> Handle(
        GetAvailableSlotsQuery request,
        CancellationToken cancellationToken)
    {
       
        var doctor = await identityService.FindByIdAsync(request.DoctorId);

        if (doctor is null)
        {
            return Result.Failure<IEnumerable<AvailableSlotsDtoResponse>>(UserErrors.DoctorNotFound);
        }

        
        var availableSlots = (await repository.GetAvailableSlotsByDoctorAndDateAsync(
            request.DoctorId,
            request.Date,
            cancellationToken)).ToList();

        if (availableSlots.Count == 0)
        {
            return Result.Failure<IEnumerable<AvailableSlotsDtoResponse>>(SlotsError.SlotsNotFound);
        }

        
        var availableSlotsDto = availableSlots.Select(slot => new AvailableSlotsDtoResponse
        {
            DoctorName = doctor.FullName,
            SlotId = slot.Id,
            StartTime = slot.StartTime,
            EndTime = slot.EndTime,
            IsActive = slot.IsActive
        }).ToList();

        return Result.Success<IEnumerable<AvailableSlotsDtoResponse>>(availableSlotsDto);
    }
}