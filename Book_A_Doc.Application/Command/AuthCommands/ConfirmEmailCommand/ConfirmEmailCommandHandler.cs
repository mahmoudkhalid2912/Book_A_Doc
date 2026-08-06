using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.ConfirmEmailCommand;

public class ConfirmEmailCommandHandler(IIdentityService identityService, ITokenEncoder tokenEncoder,IAuthenticationService authenticationService) : IRequestHandler<ConfirmEmailCommand, Result>
{
    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await identityService.FindByIdAsync(request.UserId);
        if(user is null)
        {
            return Result.Failure(AuthErrors.InvalidConfirmationToken);
        }

        if(user.EmailConfirmed)
        {
            return Result.Failure(AuthErrors.EmailAlreadyConfirmed);
        }
        var token = request.Token;

        try
        {
            token = tokenEncoder.Decode(token);
        }
        catch (FormatException)
        {
            return Result.Failure(AuthErrors.InvalidConfirmationToken);
        }
        var result = await authenticationService.ConfirmEmailAsync(user, token);

        if (result.IsFailure)
        {
            return result;
        }

        return Result.Success(AuthMessages.EmailConfirmed);
    }
}
