using Book_A_Doc.Domain.Models.Identity;

namespace Book_A_Doc.Application.Services;

public interface IOtpService
{
    Task<string> GenerateAndStoreAsync(string key, TimeSpan expiration);

    Task<bool> ValidateAsync(string key, string code);

    Task RemoveAsync(string key);
}
