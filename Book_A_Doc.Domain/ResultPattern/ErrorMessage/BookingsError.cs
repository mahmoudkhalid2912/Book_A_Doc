namespace Book_A_Doc.Domain.ResultPattern.ErrorMessage;

public static class BookingsError
{
    public static readonly Error BookingNotFound = new(
        "Bookings.BookingNotFound",
        "The requested booking was not found.",
        404);

    public static readonly Error BookingsNotFound = new(
        "Bookings.BookingsNotFound",
        "No bookings were found.",
        404);

    public static readonly Error BookingIdRequired = new(
        "Bookings.BookingIdRequired",
        "Booking ID is required.",
        400);

    public static readonly Error PatientIdRequired = new(
        "Bookings.PatientIdRequired",
        "Patient ID is required.",
        400);

    public static readonly Error PatientNotFound = new(
        "Bookings.PatientNotFound",
        "The patient profile was not found.",
        404);

    public static readonly Error SlotInPast = new(
        "Bookings.SlotInPast",
        "This slot has already started or passed.",
        400);

    public static readonly Error BookingCannotBeCancelled = new(
        "Bookings.BookingCannotBeCancelled",
        "Only pending or confirmed bookings can be cancelled.",
        400);
}
