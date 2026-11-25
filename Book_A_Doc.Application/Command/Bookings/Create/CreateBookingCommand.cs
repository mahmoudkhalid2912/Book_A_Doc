using Book_A_Doc.Application.Interfaces;
using Book_A_Doc.Domain.ResultPattern;

namespace Book_A_Doc.Application.Commands.Bookings.Create;

public record CreateBookingCommand(Guid SlotId, Guid PatientId)
    : ITransactionalRequest<Result<Guid>>;
