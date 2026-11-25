namespace Book_A_Doc.Application.Queries.Bookings.GetById;

public record BookingDtoResponse
{
    public Guid BookingId { get; init; }
    public Guid SlotId { get; init; }
    public Guid DoctorId { get; init; }
    public DateOnly Date { get; init; }
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }
    public decimal Amount { get; init; }
    public string Status { get; init; } = string.Empty;
}
