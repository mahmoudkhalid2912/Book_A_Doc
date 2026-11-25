using Book_A_Doc.Application.Queries.Bookings.GetById;
using Book_A_Doc.Domain.Interfaces.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Queries.Bookings.GetMyBookings;

public class GetMyBookingsQueryHandler(IBookingRepository repository)
    : IRequestHandler<GetMyBookingsQuery, Result<IEnumerable<BookingDtoResponse>>>
{
    public async Task<Result<IEnumerable<BookingDtoResponse>>> Handle(
        GetMyBookingsQuery request,
        CancellationToken cancellationToken)
    {
        var bookings = await repository.GetByPatientAsync(request.PatientId, cancellationToken);

        if (bookings.Count == 0)
        {
            return Result.Failure<IEnumerable<BookingDtoResponse>>(
                BookingsError.BookingsNotFound);
        }

        var response = bookings.Select(booking => new BookingDtoResponse
        {
            BookingId = booking.Id,
            SlotId = booking.AvailabilitySlotId,
            DoctorId = booking.DoctorId,
            Date = booking.AvailabilitySlot.Date,
            StartTime = booking.AvailabilitySlot.StartTime,
            EndTime = booking.AvailabilitySlot.EndTime,
            Amount = booking.Amount,
            Status = booking.Status.ToString()
        }).ToList();

        return Result.Success<IEnumerable<BookingDtoResponse>>(
            response,
            BookingsMessages.BookingsRetrieved);
    }
}
