using Book_A_Doc.ApiResponse;
using Book_A_Doc.Application.Queries.Doctors.GetAllDoctors;
using Book_A_Doc.Application.Queries.Doctors.GetDoctor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_A_Doc.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DoctorController : ApiControllerBase
{
    [HttpGet("All")]
    [Authorize(Roles = "Admin,Patient")]
    public async Task<IActionResult> GetAllDoctors(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllDoctorsQuery(), cancellationToken);
        return ToResponse(result);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Patient")]
    public async Task<IActionResult> GetDoctor(
    [FromServices] IMediator mediator,
    [FromQuery] Guid id,
    CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new GetDoctorQuery(id),
            cancellationToken);

        return ToResponse(result);
    }
}
