using System.Security.Claims;

namespace Book_A_Doc;

public static class CurrentUser
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userId, out var id))
            throw new UnauthorizedAccessException("Invalid or missing user id.");

        return id;
    }
}
