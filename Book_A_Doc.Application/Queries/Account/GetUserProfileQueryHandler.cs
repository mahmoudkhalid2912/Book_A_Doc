using MediatR;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;


namespace Book_A_Doc.Application.Queries.Account;

public class GetUserProfileQueryHandler(IUserService userService) : IRequestHandler<GetUserProfileQuery, Result<UserProfileDto>>
{
   

    public async Task<Result<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var userProfile = await userService.GetUserProfileAsync(request.UserId);

        return Result.Success(
            userProfile,
            UserMessages.UserRetrievedSuccessfully); ;

    }
}
