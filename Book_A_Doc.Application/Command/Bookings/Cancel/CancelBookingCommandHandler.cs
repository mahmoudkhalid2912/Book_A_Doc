using Book_A_Doc.Domain.Interfaces.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Commands.Bookings.Cancel;

public class CancelBookingCommandHandler(IBookingRepository repository)
    : IRequestHandler<CancelBookingCommand, Result>
{
    public async Task<Result> Handle(
        CancelBookingCommand request,
        CancellationToken cancellationToken)
    {
        var booking = await repository.GetByIdAsync(request.BookingId, cancellationToken);

        // A patient can only see and cancel their own bookings.
        if (booking is null || booking.PatientId != request.PatientId)
        {
            return Result.Failure(BookingsError.BookingNotFound);
        }

        if (booking.Status != BookingStatus.PendingPayment
            && booking.Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingsError.BookingCannotBeCancelled);
        }

        booking.Status = BookingStatus.Cancelled;
        booking.UpdatedOn = DateTime.UtcNow;

        repository.Update(booking);

        return Result.Success(BookingsMessages.BookingCancelled);
    }
}
