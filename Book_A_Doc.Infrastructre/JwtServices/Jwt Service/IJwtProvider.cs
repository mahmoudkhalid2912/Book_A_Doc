using Book_A_Doc.Domain.Models.Identity;

namespace Book_A_Doc.Infrastructre.JwtServices;

public interface IJwtProvider
{
    (string Token, int ExpiresIn) GenerateJwtToken(ApplicationUser user);
}
