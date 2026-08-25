using Book_A_Doc.Domain.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace Book_A_Doc.Domain.Models;

public class DoctorException:BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public string Reason { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = false;
    
}
