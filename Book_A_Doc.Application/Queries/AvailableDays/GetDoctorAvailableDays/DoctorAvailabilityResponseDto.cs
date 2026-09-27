namespace Book_A_Doc.Application.Queries.AvailableDays.GetDoctorAvailableDays;

public class DoctorAvailabilityResponseDto
{
    public Guid Id { get; set; }

    public Guid DoctorId { get; set; }

    public string DoctorName { get; set; } = null!;

    public DayOfWeek DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int SlotDurationInMinutes { get; set; }

    public bool IsActive { get; set; }
}
    
