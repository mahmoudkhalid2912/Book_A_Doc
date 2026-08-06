using Book_A_Doc.Application.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Cryptography;

namespace Book_A_Doc.Infrastructre.Services.OTP;

public class OTPService(IDistributedCache cache) : IOtpService
{
    public async Task<string> GenerateAndStoreAsync(string key, TimeSpan expiration)
    {
        var code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

        await cache.SetStringAsync(
            key,
            code,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            });

        return code;
    }

    public async Task RemoveAsync(string key)
    {
        await cache.RemoveAsync(key);
    }

    public async Task<bool> ValidateAsync(string key, string code)
    {
        var storedCode = await cache.GetStringAsync(key);

        if (storedCode is null)
            return false;

        return storedCode == code;
    }
}
