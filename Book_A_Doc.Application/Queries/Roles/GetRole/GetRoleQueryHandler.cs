using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using MediatR;

namespace Book_A_Doc.Application.Queries.Roles.GetRole;

public class GetRoleQueryHandler(IIdentityService identityService) : IRequestHandler<GetRoleQuery, Result<GetRoleResponse>>
{
    public async Task<Result<GetRoleResponse>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role= await identityService.GetRoleByIdAsync(request.Id);
        if(role is null)
        {
            return Result.Failure<GetRoleResponse>(AuthErrors.RoleNotFound);
        }
        var response = new GetRoleResponse
        {
            Id = role.Id,
            RoleName = role.Name!,
            IsDefault = role.IsDefault,
            IsDeleted = role.IsDeleted
        };

       return Result.Success(response, AuthMessages.RoleRetrieved);
    }
}
