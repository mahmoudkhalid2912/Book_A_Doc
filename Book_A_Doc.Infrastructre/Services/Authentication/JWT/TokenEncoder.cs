using Book_A_Doc.Application.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Book_A_Doc.Infrastructre.Services.Authentication.JWT;

public class TokenEncoder : ITokenEncoder
{
    public string Encode(string value)
    {
        return WebEncoders.Base64UrlEncode(
            Encoding.UTF8.GetBytes(value));
    }

    public string Decode(string value)
    {
        return Encoding.UTF8.GetString(
            WebEncoders.Base64UrlDecode(value));
    }
}
