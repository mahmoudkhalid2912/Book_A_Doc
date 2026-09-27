using Book_A_Doc.Application.Queries.AvailableDays.GetDoctorAvailableDays;
using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.DoctorAvailability;

public record GetDoctorAvailabilityByIdQuery(
    Guid Id
) : IRequest<Result<DoctorAvailabilityResponseDto>>;