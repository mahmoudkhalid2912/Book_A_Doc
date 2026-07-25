namespace Book_A_Doc.Domain.ResultPattern;

public class Result
{
    protected Result(bool isSuccess, Error error, string? message = null)
    {
        if ((isSuccess && error != Error.None) ||
            (!isSuccess && error == Error.None))
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
        Message = message;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public string? Message { get; }

    public static Result Success(string? message = null)
        => new(true, Error.None, message);

    public static Result Failure(Error error)
        => new(false, error);

    public static Result<TValue> Success<TValue>(
        TValue value,
        string? message = null)
        => new(value, true, Error.None, message);

    public static Result<TValue> Failure<TValue>(Error error)
        => new(default!, false, error);
}

public class Result<TValue> : Result
{
    private readonly TValue _value;

    public Result(
        TValue value,
        bool isSuccess,
        Error error,
        string? message = null)
        : base(isSuccess, error, message)
    {
        _value = value;
    }

    public TValue Value =>
        IsSuccess
            ? _value
            : throw new InvalidOperationException(
                "The value of a failure result cannot be accessed.");
}