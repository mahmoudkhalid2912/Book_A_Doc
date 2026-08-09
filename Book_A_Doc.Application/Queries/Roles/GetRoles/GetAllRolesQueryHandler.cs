using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.Roles.GetRoles;

public class GetAllRolesQueryHandler(
    IIdentityService identityService)
    : IRequestHandler<
        GetAllRolesQuery,
        Result<List<GetAllRolesResponse>>>
{
    public async Task<Result<List<GetAllRolesResponse>>> Handle(
        GetAllRolesQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await identityService.GetAllRolesAsync(
            cancellationToken);

        var response = roles.Select(role =>
            new GetAllRolesResponse
            {
                Id = role.Id,
                Name = role.Name!,
                IsDefault = role.IsDefault,
                IsDeleted = role.IsDeleted
            }).ToList();

        return Result.Success(response);
    }
}