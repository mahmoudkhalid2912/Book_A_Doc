using Book_A_Doc.ApiResponse;
using Book_A_Doc.Application.Command.AuthCommands.ConfirmEmailCommand;
using Book_A_Doc.Application.Command.AuthCommands.ForgetPasswordCommand;
using Book_A_Doc.Application.Command.AuthCommands.LoginCommand;
using Book_A_Doc.Application.Command.AuthCommands.RefreshTokenCommand;
using Book_A_Doc.Application.Command.AuthCommands.RegisterCommand;
using Book_A_Doc.Application.Command.AuthCommands.ResendConfiramationEmailCommand;
using Book_A_Doc.Application.Command.AuthCommands.ResetPasswordCommand;
using Book_A_Doc.Application.Command.AuthCommands.VerifyOtpCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_A_Doc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ApiControllerBase
{
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(
        [FromBody] SignUpCommand command,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(
        [FromBody] LoginCommand command,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenCommand command,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }

    [HttpPost("RevokeRefreshToken")]
    public async Task<IActionResult> RevokeRefreshToken(
        [FromBody] RevokeRefreshTokenCommand command,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }

    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(
        [FromBody] ConfirmEmailCommand command,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }

    [HttpPost("ResendConfirmationEmail")]
    public async Task<IActionResult> ResendConfirmationEmail(
        [FromBody] ResendEmailConfiramtionCommand command,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }
    
    [HttpPost("ForgetPassword")]
    public async Task<IActionResult> ForgetPassword(
        [FromBody] ForgetPasswordCommand command,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }

    [HttpPost("VerifyOtp")]
    public async Task<IActionResult> VerifyOtp(
        [FromBody] VerifyOtpCommand command,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(
        [FromBody] ResetPasswordCommand command,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        return ToResponse(result);
    }
}
