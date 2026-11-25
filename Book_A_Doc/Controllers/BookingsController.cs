using Book_A_Doc.ApiResponse;
using Book_A_Doc.Application.Commands.Bookings.Cancel;
using Book_A_Doc.Application.Commands.Bookings.Create;
using Book_A_Doc.Application.Queries.Bookings.GetById;
using Book_A_Doc.Application.Queries.Bookings.GetMyBookings;
using Book_A_Doc.Domain.Consts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_A_Doc.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = DefaultRoles.Patient)]
public class BookingsController(IMediator mediator) : ApiControllerBase
{
    public record CreateBookingRequest(Guid SlotId);

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
    {
        var result = await mediator.Send(
            new CreateBookingCommand(request.SlotId, User.GetUserId()));
        return ToResponse(result);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyBookings()
    {
        var result = await mediator.Send(new GetMyBookingsQuery(User.GetUserId()));
        return ToResponse(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBookingById(Guid id)
    {
        var result = await mediator.Send(new GetBookingByIdQuery(id, User.GetUserId()));
        return ToResponse(result);
    }

    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> CancelBooking(Guid id)
    {
        var result = await mediator.Send(new CancelBookingCommand(id, User.GetUserId()));
        return ToResponse(result);
    }
}
