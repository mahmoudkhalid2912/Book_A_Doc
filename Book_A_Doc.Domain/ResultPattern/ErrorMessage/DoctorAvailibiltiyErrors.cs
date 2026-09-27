namespace Book_A_Doc.Domain.ResultPattern.ErrorMessage;

public static class DoctorAvailabilityErrors
{
    // ---------------------------
    // Query Errors
    // ---------------------------

    public static Error NoAvailableDaysFoundForThisDoctor = new(
        "No available days found for this doctor.",
        "The requested doctor does not have any active availability days in the system.",
        404);

    public static Error AvailabilityNotFound = new(
        "Doctor availability not found.",
        "The requested doctor availability could not be found in the system.",
        404);


    // ---------------------------
    // Validation Errors
    // ---------------------------
    public static Error DoctorIdIsRequired = new(
        "Doctor ID is required.",
        "The doctor ID must be provided and cannot be empty.",
        400);
    public static Error InvalidDayOfWeek = new(
        "Invalid day of week.",
        "The provided day of week is not valid.",
        400);

    public static Error InvalidTimeRange = new(
        "Invalid time range.",
        "The start time must be earlier than the end time.",
        400);

    public static Error InvalidSlotDuration = new(
        "Invalid slot duration.",
        "The slot duration must be greater than zero.",
        400);

    public static Error SlotDurationExceedsAvailabilityDuration = new(
        "Invalid slot duration.",
        "The slot duration cannot be greater than the availability duration.",
        400);

    public static Error SlotDurationMustDivideExactly = new(
        "Invalid slot duration.",
        "The slot duration must divide the availability duration exactly.",
        400);


    // ---------------------------
    // Business Rule Errors
    // ---------------------------

    public static Error AvailabilityOverlapsExistingAvailability = new(
        "Availability time conflict.",
        "The provided availability overlaps with an existing availability for this doctor on the same day.",
        409);
}