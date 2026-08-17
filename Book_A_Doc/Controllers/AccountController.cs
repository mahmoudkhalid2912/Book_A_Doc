using Book_A_Doc.ApiResponse;
using Book_A_Doc.Application.Command.Account.ChangeUserPasswordCommand;
using Book_A_Doc.Application.Command.Account.UpdateUserProfileCommand;
using Book_A_Doc.Application.Queries.Account;
using Book_A_Doc.Application.Queries.Account.GetAllUsers;
using Book_A_Doc.Application.Queries.Account.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_A_Doc.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class AccountController() : ApiControllerBase
{
    [HttpGet("profile")]
    public async Task<IActionResult> GetUserProfile([FromServices]IMediator mediator)
    {
        var userId = User.GetUserId();
        var result = await mediator.Send(new GetUserProfileQuery(userId));
       return ToResponse(result);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody]UpdateUserCommand command,[FromServices] IMediator mediator)
    {
        command.UserId = User.GetUserId();

        var result = await mediator.Send(command);

        return ToResponse(result);
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody]ChangeUserPasswordCommand command, [FromServices] IMediator mediator)
    {
        command.UserId = User.GetUserId();
        var result = await mediator.Send(command);
        return ToResponse(result);
    }

    [HttpGet("all-users")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers([FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new GetAllUserQuery());
        return ToResponse(result);
    }
}
