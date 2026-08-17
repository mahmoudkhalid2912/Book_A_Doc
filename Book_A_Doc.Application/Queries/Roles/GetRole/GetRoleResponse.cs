namespace Book_A_Doc.Application.Queries.Roles.GetRole;

public class GetRoleResponse
{
    public Guid Id { get; set; }
    public string RoleName { get; set; } = string.Empty;

    public bool IsDefault { get; set; }

    public bool IsDeleted { get; set; }
}
