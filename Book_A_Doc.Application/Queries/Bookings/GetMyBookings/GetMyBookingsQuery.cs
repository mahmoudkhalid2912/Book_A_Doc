using Book_A_Doc.Application.Queries.Bookings.GetById;
using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.Bookings.GetMyBookings;

public record GetMyBookingsQuery(Guid PatientId)
    : IRequest<Result<IEnumerable<BookingDtoResponse>>>;
