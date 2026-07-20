using Microsoft.AspNetCore.Identity;

namespace Book_A_Doc.Domain.Models.Identity;

public class Patient :BaseEntity
{

    public Guid UserId { get; set; } 

    public ApplicationUser User { get; set; } = null!;
}
