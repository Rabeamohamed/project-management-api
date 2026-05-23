using Microsoft.Extensions.Caching.Distributed;
using ProjectManagement.Application.Common.Interfaces;
using System.Text.Json;

namespace ProjectManagement.Infrastructure.Services;
public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
        Console.WriteLine(_cache.GetType().FullName);
    }
    public async Task<T?> GetAsync<T>(string key)
    {
        var cachedData = await _cache.GetStringAsync(key);

        if (cachedData is null)
            return default;

        return JsonSerializer.Deserialize<T>(cachedData);
    }
    public async Task SetAsync<T>(string key,T value,TimeSpan expiration)
    {
        var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };
        var jsonData =JsonSerializer.Serialize(value);
        await _cache.SetStringAsync(key,jsonData,options);

    }
    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}