using Book_A_Doc.API.Extensions;
using Book_A_Doc.ApiResponse;
using Book_A_Doc.Application.Quiers.AuthQuery.LoginQuery;
using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
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

}
