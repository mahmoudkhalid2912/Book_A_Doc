using Book_A_Doc.Application.Interfaces;
using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Command.AvailableDays.Update;

public record UpdateDoctorAvailabilityCommand(
    Guid Id,
    Guid DoctorId,
    DayOfWeek DayOfWeek,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int SlotDurationInMinutes,
    bool IsActive
) : ITransactionalRequest<Result>;