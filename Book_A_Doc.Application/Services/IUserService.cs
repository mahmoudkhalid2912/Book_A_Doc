using Book_A_Doc.Application.Queries.Account;

namespace Book_A_Doc.Application.Services;

public interface IUserService
{
    Task<UserProfileDto> GetUserProfileAsync(Guid userId);
}

