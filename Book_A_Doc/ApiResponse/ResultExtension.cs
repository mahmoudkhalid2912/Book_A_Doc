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
        // ================================
        // SUCCESS
        // ================================
        if (result.IsSuccess)
        {
            return controller.Ok(new ApiResponse<T>
            {
                Message = result.Message ?? "Success",
                Data = result.Value
            });
        }

        // ================================
        // VALIDATION ERROR
        // ================================
        if (result is IValidationResult validationResult)
        {
            return new ObjectResult(new ApiResponse<T>
            {
                Message = result.Error.Description,

                Errors = validationResult.Errors
                    .GroupBy(error => error.Code)
                    .Select(group => new ApiError
                    {
                        Field = group.Key,
                        Descriptions = group
                            .Select(error => error.Description)
                            .ToList()
                    })
                    .ToList()
            })
            {
                StatusCode = result.Error.StatusCode
            };
        }

        // ================================
        // BUSINESS ERROR
        // ================================
        return new ObjectResult(new ApiResponse<T>
        {
            Message = result.Error.Description,

            Error = new ApiBusinessError
            {
                Code = result.Error.Code,
                Description = result.Error.Description
            }
        })
        {
            StatusCode = result.Error.StatusCode
        };
    }


    public static IActionResult ToApiResponse(
        this Result result,
        ControllerBase controller)
    {
        // ================================
        // SUCCESS
        // ================================
        if (result.IsSuccess)
        {
            return controller.Ok(new ApiResponse<object>
            {
                Message = result.Message ?? "Success"
            });
        }

        // ================================
        // VALIDATION ERROR
        // ================================
        if (result is IValidationResult validationResult)
        {
            return new ObjectResult(new ApiResponse<object>
            {
                Message = result.Error.Description,

                Errors = validationResult.Errors
                    .GroupBy(error => error.Code)
                    .Select(group => new ApiError
                    {
                        Field = group.Key,
                        Descriptions = group
                            .Select(error => error.Description)
                            .ToList()
                    })
                    .ToList()
            })
            {
                StatusCode = result.Error.StatusCode
            };
        }

        // ================================
        // BUSINESS ERROR
        // ================================
        return new ObjectResult(new ApiResponse<object>
        {
            Message = result.Error.Description,

            Error = new ApiBusinessError
            {
                Code = result.Error.Code,
                Description = result.Error.Description
            }
        })
        {
            StatusCode = result.Error.StatusCode
        };
    }
}