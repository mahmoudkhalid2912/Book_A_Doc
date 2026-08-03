using Book_A_Doc.Application.Command.AuthCommands.LoginQuery;
using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.RefreshTokenQuery;

public class RefreshTokenCommand:IRequest<Result<LoginResponse>>
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
