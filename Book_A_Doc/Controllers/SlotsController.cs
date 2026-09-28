using Book_A_Doc.ApiResponse;
using Book_A_Doc.Application.Queries.Slots.GetAll;
using Book_A_Doc.Domain.Consts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_A_Doc.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SlotsController(IMediator mediator) : ApiControllerBase
{
    [HttpGet("AvailableSlots")]
    [Authorize(Roles = DefaultRoles.Doctor + "," + DefaultRoles.Patient)]
    public async Task<IActionResult> GetAvailableSlots(
        [FromQuery] GetAvailableSlotsQuery query)
    {
        var result = await mediator.Send(query);
        return ToResponse(result);
    }
}
