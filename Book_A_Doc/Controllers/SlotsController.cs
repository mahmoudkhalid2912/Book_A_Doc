using Book_A_Doc.ApiResponse;
using Book_A_Doc.Application.Commands.Slots.Activate;
using Book_A_Doc.Application.Commands.Slots.Deactivate;
using Book_A_Doc.Application.Queries.Slots.GetAll;
using Book_A_Doc.Application.Queries.Slots.GetByDoctor;
using Book_A_Doc.Application.Queries.Slots.GetById;
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

    
    [HttpGet("Doctor/{doctorId:guid}")]
    [Authorize(Roles = DefaultRoles.Doctor + "," + DefaultRoles.Admin)]
    public async Task<IActionResult> GetDoctorSlots(
        [FromRoute] Guid doctorId,
        [FromQuery] DateOnly startDate,
        [FromQuery] DateOnly endDate)
    {
        var query = new GetDoctorSlotsQuery(doctorId, startDate, endDate);
        var result = await mediator.Send(query);
        return ToResponse(result);
    }

    
    [HttpPatch("{id:guid}/deactivate")]
    [Authorize(Roles = DefaultRoles.Doctor)]
    public async Task<IActionResult> DeactivateSlot(Guid id)
    {
        var result = await mediator.Send(new DeactivateSlotCommand(id));
        return ToResponse(result);
    }

    [HttpPatch("{id:guid}/activate")]
    [Authorize(Roles = DefaultRoles.Doctor)]
    public async Task<IActionResult> ActivateSlot(Guid id)
    {
        var result = await mediator.Send(new ActivateSlotCommand(id));
        return ToResponse(result);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = DefaultRoles.Doctor + "," + DefaultRoles.Patient)]
    public async Task<IActionResult> GetSlotById(Guid id)
    {
        var result = await mediator.Send(new GetSlotByIdQuery(id));
        return ToResponse(result);
    }



}
