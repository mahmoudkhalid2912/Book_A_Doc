namespace Book_A_Doc.Application.Queries.Slots.GetById;

public record SlotDtoResponse
{
    public Guid SlotId { get; init; }
    public string DoctorName { get; init; } = string.Empty;
    public DateOnly Date { get; init; }
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }
    public bool IsActive { get; init; }
    public bool IsBooked { get; init; }
}