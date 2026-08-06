using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.VerifyOtpCommand;

public class VerifyOtpCommand:IRequest<Result>
{
    public string Email { get; set; } = string.Empty;

    public string Code { get; set; }= string.Empty;
}
