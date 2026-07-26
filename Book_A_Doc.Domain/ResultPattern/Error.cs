namespace Book_A_Doc.Domain.ResultPattern;

public record Error(
    string Code,
    string Description,
    int? StatusCode)
{
    public static readonly Error None =
        new(string.Empty, string.Empty, null);
}