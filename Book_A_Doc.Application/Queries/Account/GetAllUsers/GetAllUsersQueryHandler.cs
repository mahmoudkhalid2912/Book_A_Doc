using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;
using System.Security.Principal;

namespace Book_A_Doc.Application.Queries.Account.GetAllUsers;

public class GetAllUsersQueryHandler(IIdentityService identityService) : IRequestHandler<GetAllUserQuery, Result<List<UseresResponse>>>
{
    public async Task<Result<List<UseresResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users = await identityService
            .GetAllUsersWithRolesAsync(cancellationToken);

        return Result.Success(
            users,
            AccountMessages.UsersRetrivedSuccessfully);

    }
}
