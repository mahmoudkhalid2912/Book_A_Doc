namespace Book_A_Doc.Domain.ResultPattern;

public class Result<T> : Result
{
    private readonly T _value;

    public Result(
        T value,
        bool isSuccess,
        Error error,
        string? message = null)
        : base(isSuccess, error, message)
    {
        _value = value;
    }

    public T Value =>
        IsSuccess
            ? _value
            : throw new InvalidOperationException(
                "Cannot access the value of a failed result.");
}