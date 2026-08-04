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
}
