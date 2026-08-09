using Book_A_Doc.Domain.Models.Identity;


public interface IJwtProvider
{
    (string Token, int ExpiresIn) GenerateJwtToken(ApplicationUser user, IEnumerable<string> roles);
    string GenerateRefreshToken();

    Guid? ValidateToken (string token);
}
