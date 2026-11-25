using Book_A_Doc.Domain.Interfaces.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Queries.Bookings.GetById;

public class GetBookingByIdQueryHandler(IBookingRepository repository)
    : IRequestHandler<GetBookingByIdQuery, Result<BookingDtoResponse>>
{
    public async Task<Result<BookingDtoResponse>> Handle(
        GetBookingByIdQuery request,
        CancellationToken cancellationToken)
    {
        var booking = await repository.GetByIdAsync(request.BookingId, cancellationToken);

        if (booking is null || booking.PatientId != request.PatientId)
        {
            return Result.Failure<BookingDtoResponse>(BookingsError.BookingNotFound);
        }

        var response = new BookingDtoResponse
        {
            BookingId = booking.Id,
            SlotId = booking.AvailabilitySlotId,
            DoctorId = booking.DoctorId,
            Date = booking.AvailabilitySlot.Date,
            StartTime = booking.AvailabilitySlot.StartTime,
            EndTime = booking.AvailabilitySlot.EndTime,
            Amount = booking.Amount,
            Status = booking.Status.ToString()
        };

        return Result.Success(response, BookingsMessages.BookingRetrieved);
    }
}
