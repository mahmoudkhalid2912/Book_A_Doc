using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.Account.GetUserProfile;

public sealed record GetUserProfileQuery(Guid UserId) : IRequest<Result<UserProfileDto>>;
