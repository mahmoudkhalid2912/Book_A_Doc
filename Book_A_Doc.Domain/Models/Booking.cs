using Book_A_Doc.Domain.Models;
using Book_A_Doc.Domain.Models.Identity;

public class Booking:BaseEntity
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public Guid AvailabilitySlotId { get; set; }
    public AvailabilitySlot AvailabilitySlot { get; set; } = null!;

    public BookingStatus Status { get; set; }

    public decimal Amount { get; set; }

    public Payment? Payment { get; set; }
}