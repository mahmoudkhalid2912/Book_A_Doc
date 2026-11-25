using Book_A_Doc.Application.Interfaces;
using Book_A_Doc.Domain.ResultPattern;

namespace Book_A_Doc.Application.Commands.Bookings.Cancel;

public record CancelBookingCommand(Guid BookingId, Guid PatientId)
    : ITransactionalRequest<Result>;
