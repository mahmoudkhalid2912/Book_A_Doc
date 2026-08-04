using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;
using System.Security.Principal;

namespace Book_A_Doc.Application.Command.Account.ChangeUserPasswordCommand;

public class ChangeUserCommandHandler(IIdentityService identityService) : IRequestHandler<ChangeUserPasswordCommand, Result>
{
    public async Task<Result> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user= await identityService.FindByIdAsync(request.UserId);

        var result= await identityService.ChangePasswordAsync(user!, request.OldPassword, request.NewPassword);
        if(result.IsFailure)
        {
            return result;
        }

        return Result.Success(AccountMessages.PasswordChangedSuccessfully);
    }
}
