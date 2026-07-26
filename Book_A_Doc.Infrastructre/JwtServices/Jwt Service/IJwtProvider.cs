using Book_A_Doc.Domain.Models.Identity;
using System.Security.Cryptography;

namespace Book_A_Doc.Infrastructre.JwtServices;

public interface IJwtProvider
{
    (string Token, int ExpiresIn) GenerateJwtToken(ApplicationUser user);
    string GenerateRefreshToken();

    Guid? ValidateToken (string token);
}
