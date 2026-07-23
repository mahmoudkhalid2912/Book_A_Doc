namespace Book_A_Doc.Domain.ResultPattern.ErrorMessage;

public static class RegisterErrors
{
    
    public static readonly Error UserAlreadyExists =
        new("Register.UserAlreadyExists",
            "A user with this email already exists.",
            409);

    
    public static readonly Error UserCreationFailed =
        new("Register.UserCreationFailed",
            "Failed to create the user account.",
            500);

    
    public static readonly Error EmailConfirmationTokenGenerationFailed =
        new("Register.EmailConfirmationTokenGenerationFailed",
            "Failed to generate the email confirmation token.",
            500);

    
    public static readonly Error EmailConfirmationSendingFailed =
        new("Register.EmailConfirmationSendingFailed",
            "Failed to send the confirmation email.",
            500);

  
    public static readonly Error FullNameRequired =
        new("Register.FullNameRequired", "Full name is required.", 400);

    public static readonly Error FullNameTooLong =
        new("Register.FullNameTooLong", "Full name must not exceed 100 characters.", 400);

    public static readonly Error EmailRequired =
        new("Register.EmailRequired", "Email is required.", 400);

    public static readonly Error InvalidEmailFormat =
        new("Register.InvalidEmailFormat", "Invalid email format.", 400);

    public static readonly Error GmailOnly =
        new("Register.GmailOnly", "Only Gmail addresses are allowed.", 400);

   

    public static readonly Error PasswordRequired =
        new("Register.PasswordRequired", "Password is required.", 400);

    public static readonly Error PasswordTooShort =
        new("Register.PasswordTooShort", "Password must be at least 6 characters long.", 400);

    public static readonly Error PasswordRequiresLowercase =
        new("Register.PasswordRequiresLowercase", "Password must contain at least one lowercase letter.", 400);

    public static readonly Error PasswordRequiresUppercase =
        new("Register.PasswordRequiresUppercase", "Password must contain at least one uppercase letter.", 400);

    public static readonly Error PasswordRequiresSpecialCharacter =
        new("Register.PasswordRequiresSpecialCharacter", "Password must contain at least one special character.", 400);

    public static readonly Error PhoneNumberRequired =
        new("Register.PhoneNumberRequired", "Phone number is required.", 400);

    public static readonly Error InvalidEgyptianPhoneNumber =
        new("Register.InvalidEgyptianPhoneNumber", "Please enter a valid Egyptian phone number.", 400);

    public static readonly Error BirthDateRequired =
        new("Register.BirthDateRequired", "Birth date is required.", 400);

    public static readonly Error UserMustBeAtLeast15YearsOld =
        new("Register.UserMustBeAtLeast15YearsOld", "You must be at least 15 years old.", 400);
}