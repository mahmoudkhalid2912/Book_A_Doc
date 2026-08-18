using Book_A_Doc.Domain.Models.Identity;

public class AvailabilitySlot
{
    public Guid Id { get; set; }

    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public Booking? Booking { get; set; }
}