using Book_A_Doc.Application.Interfaces;
using Book_A_Doc.Domain.ResultPattern;

namespace Book_A_Doc.Application.Command.AvailableDays.Create;

public record CreateDoctorAvailabilityCommand(
    Guid DoctorId,
    DayOfWeek DayOfWeek,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int SlotDurationInMinutes
) : ITransactionalRequest<Result<Guid>>;