using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.ResetPasswordCommand;

public class ResetPasswordCommand: IRequest<Result>
{
    public string Email { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;
}

