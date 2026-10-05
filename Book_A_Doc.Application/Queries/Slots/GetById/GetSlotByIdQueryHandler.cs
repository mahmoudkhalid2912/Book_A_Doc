using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.Interfaces.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Queries.Slots.GetById;

public class GetSlotByIdQueryHandler(
    ISlotRepository repository,
    IIdentityService identityService)
    : IRequestHandler<GetSlotByIdQuery, Result<SlotDtoResponse>>
{
    public async Task<Result<SlotDtoResponse>> Handle(
        GetSlotByIdQuery request,
        CancellationToken cancellationToken)
    {
        
        var slot = await repository.GetByIdAsync(request.SlotId, cancellationToken);

        
        if (slot is null)
        {
            return Result.Failure<SlotDtoResponse>(SlotsError.SlotNotFound);
        }

        
        var doctor = await identityService.FindByIdAsync(slot.DoctorId);
        if (doctor is null)
        {
            return Result.Failure<SlotDtoResponse>(SlotsError.SlotNotFound);
        }

        var response = new SlotDtoResponse
        {
            SlotId = slot.Id,
            DoctorName = doctor?.FullName ?? "Unknown",
            Date = slot.Date,
            StartTime = slot.StartTime,
            EndTime = slot.EndTime,
            IsActive = slot.IsActive,

            
            IsBooked = slot.Booking is not null
                    && (slot.Booking.Status == BookingStatus.PendingPayment
                     || slot.Booking.Status == BookingStatus.Confirmed)
        };

        return Result.Success(response, SlotsMessages.SlotRetrieved);
    }
}