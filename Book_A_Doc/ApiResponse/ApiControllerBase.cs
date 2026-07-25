using Book_A_Doc.API.Extensions;
using Book_A_Doc.Domain.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Book_A_Doc.ApiResponse;

public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult ToResponse<T>(Result<T> result)
    {
        return result.ToApiResponse(this);
    }

    protected IActionResult ToResponse(Result result)
    {
        return result.ToApiResponse(this);
    }
}
