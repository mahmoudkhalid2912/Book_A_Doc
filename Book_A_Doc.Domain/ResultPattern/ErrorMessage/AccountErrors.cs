namespace Book_A_Doc.Domain.ResultPattern.ErrorMessage;

public static class AccountErrors
{
   
    public static readonly Error UserNameIsTooLong =
        new("AccountErrors.UserNameIsTooLong"
            , "Name Is Too Long it should be less than 100 characters"
            , 400);
   
    public static readonly Error UserMustBeAtLeast15YearsOld = new(
        "AccountErrors.UserMustBeAtLeast15YearsOld"
        , "User must be at least 15 years old"
        , 400
    );
    public static readonly Error InvalidEgyptianPhoneNumber = new(
        "AccountErrors.InvalidEgyptianPhoneNumber"
        , "Invalid Egyptian Phone Number"
        , 400
    );

    public static readonly Error UnableToUpdateUser = new(
        "AccountErrors.UnableToUpdateUser"
        , "Unable to update user"
        , 400
        );
    public static readonly Error OldPasswordRequired = new(
     "AccountErrors.OldPasswordRequired",
     "Old password is required.",
     400
 );

    public static readonly Error PasswordRequired = new(
        "AccountErrors.PasswordRequired",
        "Password is required.",
        400
    );

    public static readonly Error PasswordTooShort = new(
        "AccountErrors.PasswordTooShort",
        "Password must be at least 6 characters long.",
        400
    );

    public static readonly Error PasswordRequiresLowercase = new(
        "AccountErrors.PasswordRequiresLowercase",
        "Password must contain at least one lowercase letter.",
        400
    );

    public static readonly Error PasswordRequiresUppercase = new(
        "AccountErrors.PasswordRequiresUppercase",
        "Password must contain at least one uppercase letter.",
        400
    );

    public static readonly Error PasswordRequiresSpecialCharacter = new(
        "AccountErrors.PasswordRequiresSpecialCharacter",
        "Password must contain at least one special character.",
        400
    );

    public static readonly Error NewPasswordCannotBeSameAsOld = new(
        "AccountErrors.NewPasswordCannotBeSameAsOld",
        "New password cannot be the same as the old password.",
        400
    );

    
}
