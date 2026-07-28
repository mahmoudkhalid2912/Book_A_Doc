namespace Book_A_Doc.Domain.ResultPattern.ErrorMessage;

public static class EmailConfirmationError
{
 public static readonly Error UserIdIsRequired 
        = new("ConfirmEmail.UserIdIsRequired"
            , "User ID is required."
            , 400);

    public static readonly Error TokenIsRequired=
        new("ConfirmEmail.TokenIsRequired"
            , "Confirmation token is required."
            , 400);

    public static readonly Error InvalidToken = new(
        "ConfirmEmail.InvalidToken"
        , "Invalid confirmation token."
        , 400);
    public static readonly Error DuplicatedConfirmation = new(
        "ConfirmEmail.DuplicatedConfirmation"
        , "Email is already confirmed."
        , 400);
}
