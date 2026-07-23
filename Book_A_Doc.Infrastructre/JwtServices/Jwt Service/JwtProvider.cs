using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Infrastructre.JwtServices;
using Book_A_Doc.Infrastructre.JwtServices.OptionsClass;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Book_A_Doc.Application.Services.Jwt_Service;

public class JwtProvider(IOptions<JwtOptions> jwtoptions) : IJwtProvider
{
    public (string Token, int ExpiresIn) GenerateJwtToken(ApplicationUser user)
    {
        // Add Claims to the token
        Claim[] claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim  (JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Name,user.FullName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        // Create a symmetric security key
        var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.Value.Key));


        // Create signing credentials
        var signingCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var Token = new JwtSecurityToken(
            issuer: jwtoptions.Value.Issuer,
            audience: jwtoptions.Value.Audience ,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: signingCredentials
            );

        return (Token: new JwtSecurityTokenHandler().WriteToken(Token), ExpiresIn: 30);
    }
}
