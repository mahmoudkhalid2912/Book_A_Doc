namespace Book_A_Doc.Domain.ResultPattern;

public sealed class ValidationResult<T>
    : Result<T>, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(default!, false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationResult<T> WithErrors(
        Error[] errors)
        => new(errors);
}