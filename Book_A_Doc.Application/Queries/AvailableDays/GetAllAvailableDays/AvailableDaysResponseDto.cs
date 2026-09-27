namespace Book_A_Doc.Application.Queries.AvailableDays;

public class AvailableDaysResponseDto
{
    public Guid Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int SlotDurationInMinutes { get; set; }
}