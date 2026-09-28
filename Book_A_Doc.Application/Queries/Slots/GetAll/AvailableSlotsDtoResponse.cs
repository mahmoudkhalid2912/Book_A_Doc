namespace Book_A_Doc.Application.Queries.Slots.GetAll;

public class AvailableSlotsDtoResponse
{
    public string DoctorName { get; set; } = null!;
    public Guid SlotId{ get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool IsActive { get; set; } = true;
}
