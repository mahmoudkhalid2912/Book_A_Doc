using Book_A_Doc.Domain.Models.Identity;

public class DoctorAvailability
{
    public Guid Id { get; set; }

    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public DayOfWeek DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public int SlotDurationInMinutes { get; set; }

    public bool IsActive { get; set; }
}