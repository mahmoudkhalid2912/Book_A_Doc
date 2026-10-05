using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.Slots.GetByDoctor;

public record GetDoctorSlotsQuery(
    Guid DoctorId,
    DateOnly StartDate,
    DateOnly EndDate)
    : IRequest<Result<IEnumerable<DoctorSlotDtoResponse>>>;