using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Quiers.AuthQuery.LoginQuery;

public class LoginQueryCommand:IRequest<Result<LoginDtoResponse>>
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; }= string.Empty;
}
