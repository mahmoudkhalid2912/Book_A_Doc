using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.RegisterCommand;

public class SignInCommand:IRequest<Result>
{
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateOnly BirthDay { get; set; } 
   
    public string Password { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}
