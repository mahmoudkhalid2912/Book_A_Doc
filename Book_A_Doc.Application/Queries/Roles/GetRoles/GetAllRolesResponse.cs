namespace Book_A_Doc.Application.Queries.Roles.GetRoles;

public class GetAllRolesResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public bool IsDefault { get; set; }

    public bool IsDeleted { get; set; }
}
