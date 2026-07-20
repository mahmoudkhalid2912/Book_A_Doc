using Microsoft.AspNetCore.Identity;

namespace Book_A_Doc.Domain.Models.Identity;

public class Doctor :BaseEntity
{
    public Guid UserId { get; set; } 
    public ApplicationUser User { get; set; } = null!;

    public string? Description { get; set; } = string.Empty;

    public byte YearsOfExperience { get; set; }

    public decimal SessionPrice { get; set; }

    public decimal AverageRating { get;}

    public int ReviewCount { get;}
}
