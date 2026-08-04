using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Command.Account;

public class UpdateUserCommandHandler(IIdentityService identityService):IRequestHandler<UpdateUserCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await identityService.FindByIdAsync(request.UserId);

        user!.FullName = request.Name;
        if (!string.IsNullOrWhiteSpace(request.Phone))
        {
            user!.PhoneNumber = request.Phone;
        }
        if (request.BirthDate.HasValue)
        {
            user!.BirthDate = request.BirthDate.Value;
        }
        

        var UpdatedResult = await identityService.UpdateUserAsync(user);
        if (UpdatedResult.IsFailure)
        {
           return  Result.Failure<bool>(AccountErrors.UnableToUpdateUser);
        }

        return Result.Success<bool>(true,AccountMessages.UserUpdatedSuccessfully);
    }
}
