using Book_A_Doc.ApiResponse;
using Book_A_Doc.Application.Command.AvailableDays.Create;
using Book_A_Doc.Application.Command.AvailableDays.Delete;
using Book_A_Doc.Application.Command.AvailableDays.Update;
using Book_A_Doc.Application.Queries.AvailableDays;
using Book_A_Doc.Application.Queries.DoctorAvailability;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_A_Doc.Controllers;

[Route("api/doctor-availability")]
[ApiController]
[Authorize]
public class DoctorAvailabilityController(IMediator sender) : ApiControllerBase
{
    [HttpGet("doctor/{doctorId:guid}")]
    [Authorize(Roles = "Patient,Doctor,Admin")]
    public async Task<IActionResult> GetDoctorAvailableDays(
        [FromRoute] Guid doctorId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetDoctorAvailableDaysQuery(doctorId),
            cancellationToken);

        return ToResponse(result);
    }

    [HttpPost("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateForDoctor(
        [FromBody] CreateDoctorAvailabilityCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return ToResponse(result);
    }

    [HttpPost("my")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> CreateForMyself(
        [FromBody] CreateDoctorAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        var doctorId = CurrentUser.GetUserId(User);

        var command = new CreateDoctorAvailabilityCommand(
            doctorId,
            request.DayOfWeek,
            request.StartTime,
            request.EndTime,
            request.SlotDurationInMinutes);

        var result = await sender.Send(command, cancellationToken);
        return ToResponse(result);
    }

    [HttpGet("{AvailabilityId:guid}/{DoctorId:guid}")]
    [Authorize(Roles = "Patient,Doctor,Admin")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid AvailabilityId,[FromRoute]Guid DoctorId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetDoctorAvailabilityByIdQuery(AvailabilityId, DoctorId),
            cancellationToken);

        return ToResponse(result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateDoctorAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        var doctorId = CurrentUser.GetUserId(User);

        var command = new UpdateDoctorAvailabilityCommand(
            id,
            doctorId,
            request.DayOfWeek,
            request.StartTime,
            request.EndTime,
            request.SlotDurationInMinutes,
            request.IsActive);

        var result = await sender.Send(command, cancellationToken);
        return ToResponse(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var doctorId = CurrentUser.GetUserId(User);
        var result = await sender.Send(
            new DeleteDoctorAvailabilityCommand(id, doctorId),
            cancellationToken);

        return ToResponse(result);
    }
}
