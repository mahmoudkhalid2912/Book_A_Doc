using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.Roles.GetRoles;

public class GetAllRolesQuery
    : IRequest<Result<List<GetAllRolesResponse>>>
{
}
