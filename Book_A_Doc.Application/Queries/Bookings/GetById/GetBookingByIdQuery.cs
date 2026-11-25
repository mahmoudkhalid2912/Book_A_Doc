using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.Bookings.GetById;

public record GetBookingByIdQuery(Guid BookingId, Guid PatientId)
    : IRequest<Result<BookingDtoResponse>>;
