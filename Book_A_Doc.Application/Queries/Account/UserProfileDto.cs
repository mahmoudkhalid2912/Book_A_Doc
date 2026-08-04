namespace Book_A_Doc.Application.Queries.Account;

public class UserProfileDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public int Age { get; set; }
}
