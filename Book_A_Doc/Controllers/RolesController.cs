using Book_A_Doc.ApiResponse;
using Book_A_Doc.Application.Queries.Roles.GetRole;
using Book_A_Doc.Application.Queries.Roles.GetRoles;
using Book_A_Doc.Domain.Consts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_A_Doc.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles =DefaultRoles.Admin)]
public class RolesController : ApiControllerBase
{
    [HttpGet("Get-All")]
    public async Task<IActionResult> GetAllRoles([FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new GetAllRolesQuery());
        return ToResponse(result);
    }
    [HttpGet("Get-By-Id/{id}")]
    public async Task<IActionResult> GetRoleById([FromServices] IMediator mediator,[FromRoute]Guid id)
    {
        var result= await mediator.Send(new GetRoleQuery(id));
        return ToResponse(result);
    }

}
