using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.AvailableDays;

public record GetDoctorAvailableDaysQuery(
    Guid DoctorId
) : IRequest<Result<IEnumerable<AvailableDaysResponseDto>>>;