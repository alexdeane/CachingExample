using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace CachingExample.ApplicationCore.Services;

/// <summary>
/// Interface for caching service
/// </summary>
public interface ICachingService
{
    /// <summary>
    /// Get object from the cache
    /// </summary>
    /// <param name="key">The cache key</param>
    /// <returns>The deserialized object of type <see cref="T"/>, or <b>null</b> if no entry exists</returns>
    Task<T?> GetObject<T>(string key) where T : class;

    /// <summary>
    /// Sets an object to the cache, using the specified key
    /// </summary>
    Task SetObject<T>(string key, T value) where T : class;
}

public class CachingService : ICachingService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions;

    private static readonly JsonSerializerOptions JsonSerializerOptions
        = new(JsonSerializerDefaults.Web);

    public CachingService(IDistributedCache cache, IOptions<CacheOptions> options)
    {
        _cache = cache;
        _distributedCacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = options.Value.ExpiryTime
        };
    }

    public async Task<T?> GetObject<T>(string key) where T : class
    {
        var cacheResult = await _cache.GetAsync(key);

        if (cacheResult is null)
        {
            return null;
        }

        using var stream = new MemoryStream(cacheResult);

        return await JsonSerializer.DeserializeAsync<T>(stream, JsonSerializerOptions);
    }

    public async Task SetObject<T>(string key, T value) where T : class
    {
        using var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, value, JsonSerializerOptions);
        stream.Position = 0;

        await _cache.SetAsync(key, stream.ToArray(), _distributedCacheEntryOptions);
    }
}