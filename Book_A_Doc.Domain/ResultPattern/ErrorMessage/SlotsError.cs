namespace Book_A_Doc.Domain.ResultPattern.ErrorMessage;

public static  class SlotsError
{
    public static readonly Error SlotNotFound = new(
       "Slots.SlotNotFound",
       "The requested slot was not found.",
       404);

    public static readonly Error SlotsNotFound = new(
        "Slots.SlotsNotFound",
        "No slots were found.",
        404);

    public static readonly Error DateRequired = new(
        "Slots.DateRequired",
        "Date is required.",
        400);

    public static readonly Error DateInPast = new(
        "Slots.DateInPast",
        "Date cannot be before today's date in Egypt.",
        400);

    public static readonly Error DoctorIdRequired = new(
       "Slots.DoctorIdRequired",
       "Doctor ID is required and cannot be empty.",
       400);

    public static readonly Error DoctorNotFound = new(
        "Slots.DoctorNotFound",
        "The requested doctor was not found.",
        404);

    public static readonly Error DoctorNotAvailable = new(
        "Slots.DoctorNotAvailable",
        "The doctor is not available on this date.",
        400);

    public static readonly Error SlotAlreadyBooked = new(
        "Slots.SlotAlreadyBooked",
        "This slot is already booked.",
        409);

    public static readonly Error SlotAlreadyCancelled = new(
        "Slots.SlotAlreadyCancelled",
        "This slot is already cancelled.",
        400);

    public static readonly Error SlotNotAvailableForBooking = new(
        "Slots.SlotNotAvailableForBooking",
        "This slot is not available for booking.",
        400);

    public static readonly Error SlotCannotBeCancelled = new(
        "Slots.SlotCannotBeCancelled",
        "This slot cannot be cancelled.",
        400);
    public static readonly Error SlotCannotBeModified = new(
       "Slots.SlotCannotBeModified",
       "This slot cannot be modified.",
       400);

    public static readonly Error SlotTimeConflict = new(
        "Slots.SlotTimeConflict",
        "There is a time conflict with another slot.",
        409);

    public static readonly Error SlotDurationInvalid = new(
        "Slots.SlotDurationInvalid",
        "The slot duration is invalid.",
        400);

    public static readonly Error SlotDateOutOfWorkingHours = new(
       "Slots.SlotDateOutOfWorkingHours",
       "The slot is outside the doctor's working hours.",
       400);

    public static readonly Error MaxSlotsPerDayExceeded = new(
        "Slots.MaxSlotsPerDayExceeded",
        "The maximum number of slots per day has been exceeded.",
        400);

    public static readonly Error DoctorNotAvailableOnThisDay = new(
        "Slots.DoctorNotAvailableOnThisDay",
        "The doctor is not available on this day.",
        400);

    public static readonly Error InvalidDateRange = new(
        "Slots.InvalidDateRange",
        "End date must be after or equal to start date.",
        400);

    public static readonly Error DateRangeTooLarge = new(
        "Slots.DateRangeTooLarge",
        "Date range cannot exceed 31 days.",
        400);

    public static readonly Error SlotIdRequired = new(
    "Slots.SlotIdRequired",
    "Slot ID is required.",
    400);

    public static readonly Error SlotAlreadyDeactivated = new(
        "Slots.SlotAlreadyDeactivated",
        "This slot is already deactivated.",
        400);

    public static readonly Error SlotAlreadyActive = new(
    "Slots.SlotAlreadyActive",
    "This slot is already active.",
    400);

}
