namespace Book_A_Doc.Application.Queries.Slots.GetByDoctor;

public record DoctorSlotDtoResponse
{
    public Guid SlotId { get; init; }
    public DateOnly Date { get; init; }
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }
    public bool IsActive { get; init; }

    public bool IsBooked { get; init; }
    public bool IsCancelled { get; init; }
    public Guid? PatientId { get; init; }
}