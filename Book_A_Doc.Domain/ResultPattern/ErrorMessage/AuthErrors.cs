namespace Book_A_Doc.Domain.ResultPattern.ErrorMessage;

public static class AuthErrors
{
    // Register
    public static readonly Error UserAlreadyExists =
        new(
            "Auth.UserAlreadyExists",
            "A user with this email already exists.",
            409);

    public static readonly Error UserCreationFailed =
        new(
            "Auth.UserCreationFailed",
            "Failed to create the user account.",
            500);


    // Email Confirmation
    public static readonly Error EmailConfirmationTokenGenerationFailed =
        new(
            "Auth.EmailConfirmationTokenGenerationFailed",
            "Failed to generate the email confirmation token.",
            500);

    public static readonly Error EmailConfirmationSendingFailed =
        new(
            "Auth.EmailConfirmationSendingFailed",
            "Failed to send the confirmation email.",
            500);

    public static readonly Error EmailAlreadyConfirmed =
        new(
            "Auth.EmailAlreadyConfirmed",
            "Email is already confirmed.",
            400);

    public static readonly Error InvalidConfirmationToken =
        new(
            "Auth.InvalidConfirmationToken",
            "Invalid confirmation token.",
            400);


    // Validation
    public static readonly Error FullNameRequired =
        new(
            "Auth.FullNameRequired",
            "Full name is required.",
            400);

    public static readonly Error FullNameTooLong =
        new(
            "Auth.FullNameTooLong",
            "Full name must not exceed 100 characters.",
            400);

    public static readonly Error EmailRequired =
        new(
            "Auth.EmailRequired",
            "Email is required.",
            400);

    public static readonly Error InvalidEmailFormat =
        new(
            "Auth.InvalidEmailFormat",
            "Invalid email format.",
            400);

    public static readonly Error GmailOnly =
        new(
            "Auth.GmailOnly",
            "Only Gmail addresses are allowed.",
            400);


    public static readonly Error PasswordRequired =
        new(
            "Auth.PasswordRequired",
            "Password is required.",
            400);

    public static readonly Error PasswordTooShort =
        new(
            "Auth.PasswordTooShort",
            "Password must be at least 6 characters long.",
            400);

    public static readonly Error PasswordRequiresLowercase =
        new(
            "Auth.PasswordRequiresLowercase",
            "Password must contain at least one lowercase letter.",
            400);

    public static readonly Error PasswordRequiresUppercase =
        new(
            "Auth.PasswordRequiresUppercase",
            "Password must contain at least one uppercase letter.",
            400);

    public static readonly Error PasswordRequiresSpecialCharacter =
        new(
            "Auth.PasswordRequiresSpecialCharacter",
            "Password must contain at least one special character.",
            400);


    public static readonly Error PhoneNumberRequired =
        new(
            "Auth.PhoneNumberRequired",
            "Phone number is required.",
            400);

    public static readonly Error InvalidEgyptianPhoneNumber =
        new(
            "Auth.InvalidEgyptianPhoneNumber",
            "Please enter a valid Egyptian phone number.",
            400);


    public static readonly Error BirthDateRequired =
        new(
            "Auth.BirthDateRequired",
            "Birth date is required.",
            400);

    public static readonly Error UserMustBeAtLeast15YearsOld =
        new(
            "Auth.UserMustBeAtLeast15YearsOld",
            "You must be at least 15 years old.",
            400);


    // Login
    public static readonly Error InvalidCredentials =
        new(
            "Auth.InvalidCredentials",
            "Invalid email or password.",
            401);

    public static readonly Error EmailNotConfirmed =
        new(
            "Auth.EmailNotConfirmed",
            "Email is not confirmed.",
            401);

    public static readonly Error InvalidRefreshToken =
        new(
            "Auth.InvalidRefreshToken",
            "Invalid refresh token.",
            401);


    // OTP
    public static readonly Error InvalidOtp =
        new(
            "Auth.InvalidOtp",
            "Invalid OTP code.",
            400);

    public static readonly Error OtpExpired =
        new(
            "Auth.OtpExpired",
            "OTP code has expired.",
            400);


    // Forgot / Reset Password
    public static readonly Error UserNotFound =
        new(
            "Auth.UserNotFound",
            "User was not found.",
            404);

    public static readonly Error PasswordResetFailed =
        new(
            "Auth.PasswordResetFailed",
            "Failed to reset password.",
            400);

    public static readonly Error PasswordChangedSuccessfully =
        new(
            "Auth.PasswordChangedSuccessfully",
            "Password changed successfully.",
            200);

    public static readonly Error RoleAssignmentFailed = new(
        "Auth.RoleAssignmentFailed",
        "Failed to assign role.",
        400
    );

    //Roles
    public static readonly Error RoleNotFound = new(
        "Auth.RoleNotFound",
        "Role was not found.",
        404
    );
}