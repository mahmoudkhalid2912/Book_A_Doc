namespace Book_A_Doc.Application.Queries.Account.GetAllUsers;

public class UseresResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;


    public List<string> RoleNames { get; set; } = [];
}
