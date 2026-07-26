using Book_A_Doc.Application.Quiers.AuthQuery.LoginQuery;
using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Quiers.AuthQuery.RefreshTokenQuery;

public class RefreshTokenCommand:IRequest<Result<LoginDtoResponse>>
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
