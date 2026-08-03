namespace Book_A_Doc.Application.Command.AuthCommands.LoginQuery;

public class LoginResponse
{
    public Guid UserId { get; set; }

    public String FullName { get; set; } = string.Empty;

    public string Email { get; set; }= string.Empty;

    public string Token { get; set; } = string.Empty;

    public int TokenExpireIn { get; set; }

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime RefreshTokenExpiration { get; set; }
}
