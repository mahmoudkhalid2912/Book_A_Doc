using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.ConfirmEmailCommand;

public class ConfirmEmailCommand:IRequest<Result>
{
    public Guid UserId {  get; set; }

    public string Token { get; set; }=string.Empty;
}
