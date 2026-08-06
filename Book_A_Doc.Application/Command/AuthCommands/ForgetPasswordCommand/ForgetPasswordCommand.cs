using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.ForgetPasswordCommand;

public class ForgetPasswordCommand:IRequest<Result>
{
    public string Email { get; set; } = string.Empty;
}
