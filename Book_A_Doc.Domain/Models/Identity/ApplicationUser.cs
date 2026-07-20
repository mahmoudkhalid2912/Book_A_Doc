using Microsoft.AspNetCore.Identity;

namespace Book_A_Doc.Domain.Models.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }

    public string? ProfileImageUrl { get; set; }

    public Doctor? Doctor { get; set; }

    public Patient? Patient { get; set; }
}
