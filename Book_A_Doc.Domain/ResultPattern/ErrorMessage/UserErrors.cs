namespace Book_A_Doc.Domain.ResultPattern.ErrorMessage;

public static class UserErrors
{
    public static readonly Error UserNotFound = new(
        "User.UserNotFound"
        , "User not found."
        , 404);
}
