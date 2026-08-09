using Microsoft.AspNetCore.Identity;

namespace Book_A_Doc.Domain.Models.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public bool IsDefault { get; set; }

    public bool IsDeleted { get; set; }
}
