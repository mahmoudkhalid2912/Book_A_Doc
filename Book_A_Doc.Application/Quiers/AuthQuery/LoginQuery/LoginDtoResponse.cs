namespace Book_A_Doc.Application.Quiers.AuthQuery.LoginQuery;

public class LoginDtoResponse
{
    public Guid UserId { get; set; }

    public String FullName { get; set; } = string.Empty;

    public string Email { get; set; }= string.Empty;

    public string Token { get; set; } = string.Empty;

    public int TokenExpireIn { get; set; }
}
