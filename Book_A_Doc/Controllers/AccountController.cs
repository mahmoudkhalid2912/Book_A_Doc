using Book_A_Doc.API.Extensions;
using Book_A_Doc.ApiResponse;
using Book_A_Doc.Application.Command.Account;
using Book_A_Doc.Application.Queries.Account;
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
    public async Task<IActionResult> UpdateProfile(UpdateUserCommand command,[FromServices] IMediator mediator)
    {
        command.UserId = User.GetUserId();

        var result = await mediator.Send(command);

        return ToResponse(result);
    }
}
