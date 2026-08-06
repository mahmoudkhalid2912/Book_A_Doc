using Book_A_Doc.Domain.ResultPattern;
using MediatR;
using System.Text.Json.Serialization;

namespace Book_A_Doc.Application.Command.Account.ChangeUserPasswordCommand;

public class ChangeUserPasswordCommand:IRequest<Result>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    
}
