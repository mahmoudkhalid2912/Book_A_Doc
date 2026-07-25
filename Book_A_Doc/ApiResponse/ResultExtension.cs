using Book_A_Doc.ApiResponse;
using Book_A_Doc.Domain.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Book_A_Doc.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToApiResponse<T>(
        this Result<T> result,
        ControllerBase controller)
    {
        if (result.IsSuccess)
        {
            return controller.Ok(new ApiResponse<T>
            {
                Message = result.Message ?? "Success",
                Data = result.Value
            });
        }

        return new ObjectResult(new ApiResponse<T>
        {
            Message = result.Error.Description,
            Errors =
            [
                new ApiError
                {
                    Code = result.Error.Code,
                    Description = result.Error.Description
                }
            ]
        })
        {
            StatusCode = result.Error.StatusCode
        };
    }

    public static IActionResult ToApiResponse(
        this Result result,
        ControllerBase controller)
    {
        if (result.IsSuccess)
        {
            return controller.Ok(new ApiResponse<object>
            {
                Message = result.Message ?? "Success"
            });
        }

        return new ObjectResult(new ApiResponse<object>
        {
            Message = result.Error.Description,
            Errors =
            [
                new ApiError
                {
                    Code = result.Error.Code,
                    Description = result.Error.Description
                }
            ]
        })
        {
            StatusCode = result.Error.StatusCode
        };
    }
}