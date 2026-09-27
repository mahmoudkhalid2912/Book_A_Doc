using Book_A_Doc.Application.Interfaces;
using Book_A_Doc.Domain.ResultPattern;

namespace Book_A_Doc.Application.Command.AvailableDays.Delete;

public record DeleteDoctorAvailabilityCommand(
    Guid Id,
    Guid DoctorId
) : ITransactionalRequest<Result>;