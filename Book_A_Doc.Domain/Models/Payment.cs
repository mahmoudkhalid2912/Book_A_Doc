using Book_A_Doc.Domain.Models;

public class Payment : BaseEntity
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public decimal Amount { get; set; }

    public PaymentStatus Status { get; set; }

    public string? TransactionId { get; set; }

    public string? PaymentUrl { get; set; }

    public DateTime? PaidOn { get; set; }
}