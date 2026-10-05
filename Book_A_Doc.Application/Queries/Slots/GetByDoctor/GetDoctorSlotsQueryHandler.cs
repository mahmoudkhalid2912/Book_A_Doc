using Book_A_Doc.Domain.Interfaces.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Queries.Slots.GetByDoctor;

public class GetDoctorSlotsQueryHandler(ISlotRepository repository)
    : IRequestHandler<GetDoctorSlotsQuery, Result<IEnumerable<DoctorSlotDtoResponse>>>
{
    public async Task<Result<IEnumerable<DoctorSlotDtoResponse>>> Handle(
        GetDoctorSlotsQuery request,
        CancellationToken cancellationToken)
    {
        
        var slots = (await repository.GetSlotsByDoctorAndDateRangeAsync(
            request.DoctorId,
            request.StartDate,
            request.EndDate,
            cancellationToken)).ToList();

       
        if (slots.Count == 0)
        {
            return Result.Failure<IEnumerable<DoctorSlotDtoResponse>>(
                SlotsError.SlotsNotFound);
        }

       
        var response = slots.Select(slot => new DoctorSlotDtoResponse
        {
            SlotId = slot.Id,
            Date = slot.Date,
            StartTime = slot.StartTime,
            EndTime = slot.EndTime,
            IsActive = slot.IsActive,

            
            IsBooked = slot.Booking != null
                    && (slot.Booking.Status == BookingStatus.PendingPayment
                     || slot.Booking.Status == BookingStatus.Confirmed),

            IsCancelled = slot.Booking != null
                       && slot.Booking.Status == BookingStatus.Cancelled,

            PatientId = slot.Booking?.PatientId
        }).ToList();

        return Result.Success<IEnumerable<DoctorSlotDtoResponse>>(response, SlotsMessages.DoctorSlotsRetrieved);
    }
}