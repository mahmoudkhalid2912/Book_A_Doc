using Book_A_Doc.ApiResponse;
using Book_A_Doc.Application.Command.AuthCommands.ConfirmEmailCommand;
using Book_A_Doc.Application.Command.AuthCommands.LoginQuery;
using Book_A_Doc.Application.Command.AuthCommands.RefreshTokenQuery;
using Book_A_Doc.Application.Command.AuthCommands.RegisterCommand;
using Book_A_Doc.Application.Command.AuthCommands.ResendConfiramationEmailCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book_A_Doc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ApiControllerBase
{

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command, [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }

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

    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand command, [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }

    [HttpPost("ResendConfirmationEmail")]
    public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendEmailConfiramtionCommand command, [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }
}
