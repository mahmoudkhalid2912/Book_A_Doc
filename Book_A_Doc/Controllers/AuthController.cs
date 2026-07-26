using Book_A_Doc.ApiResponse;
using Book_A_Doc.Application.Quiers.AuthQuery.LoginQuery;
using Book_A_Doc.Application.Quiers.AuthQuery.RefreshTokenQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book_A_Doc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ApiControllerBase
{
    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] LoginQueryCommand command, [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command, [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }

    [HttpPost("RevokeRefreshToken")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenCommand command, [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }
}
