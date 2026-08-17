using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.Account.GetAllUsers;

public class GetAllUserQuery:IRequest<Result<List<UseresResponse>>>
{

}
