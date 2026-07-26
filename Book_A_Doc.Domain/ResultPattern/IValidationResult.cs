namespace Book_A_Doc.Domain.ResultPattern;

public interface IValidationResult
{
    public static readonly Error ValidationError =
        new(
            "Validation",
            "One or more validation errors occurred.",
            400);

    Error[] Errors { get; }
}