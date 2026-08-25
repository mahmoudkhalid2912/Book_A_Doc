using System.ComponentModel.DataAnnotations;

namespace Book_A_Doc.Domain.Models;

public class Holiday:BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
