namespace Book_A_Doc.Domain.ResultPattern.ErrorMessage;

public static class UserErrors
{
    public static readonly Error UserNotFound = new(
        "User.UserNotFound"
        , "User not found."
        , 404);

     public static readonly Error DoctorNotFound=new Error(
         "Doctor.DoctorNotFound"
         , "Doctor not found."
         , 404 );

    public static readonly Error DoctorNameMustBeBetween5And100Characters = new(
   "Doctor.Name.InvalidLength",
   "Doctor name must be between 5 and 100 characters.",
   400
      );

    public static readonly Error SpecialtyMustBeBetween5And100Characters = new(
        "Doctor.Specialty.InvalidLength",
        "Specialty must be between 5 and 100 characters.",
        400
    );

    public static readonly Error YearsOfExperienceMustBeLessThanOrEqualTo50 = new(
        "Doctor.YearsOfExperience.InvalidValue",
        "Years of experience must be less than or equal to 50.",
        400
    );

    public static readonly Error SessionPriceMustBeGreaterThanZero = new(
        "Doctor.SessionPrice.InvalidValue",
        "Session price must be greater than 0.",
        400
    );

    public static readonly Error SessionPriceMustBeLessThanOrEqualTo10000 = new(
        "Doctor.SessionPrice.InvalidValue",
        "Session price must be less than or equal to 10000.",
        400
    );

    public static readonly Error InvalidEgyptianPhoneNumber = new(
        "User.PhoneNumber.InvalidFormat",
        "Invalid Egyptian phone number.",
        400
    );

    public static readonly Error DoctorMustbeAtLeast22YearsOld=new(
        "Doctor.Age.InvalidValue",
         "Doctor must be at least 22 years old.",
         400
    );

    
}
