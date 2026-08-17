using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.Roles.GetRole;

public record GetRoleQuery(Guid Id) : IRequest<Result<GetRoleResponse>>;

