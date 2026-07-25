namespace Book_A_Doc.ApiResponse;

public class ApiResponse<T>
{
    public string Message { get; init; } = string.Empty;

    public T? Data { get; init; }

    public IEnumerable<ApiError>? Errors { get; init; }
}

public class ApiError
{
    public string Code { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}