namespace Book_A_Doc.Domain.ResultPattern.ErrorMessage;

public static class LoginErrors
{
    public static readonly Error InvalidCredentials =
        new("Login.InvalidCredentials",
            "Invalid email or password.",
            401);

    public static readonly Error PasswordIsRequried = new(
        "Login.PasswordIsRequired",
        "Password is required.",
        401
    );

    public static readonly Error EmailIsRequried = new(
        "Login.EmailIsRequired",
        "Email is required.",
        401
    );

    public static readonly Error InvalidEmailFormat= new(
        "Login.InvalidEmailFormat",
        "Invalid email format.",
        401
    );


}
