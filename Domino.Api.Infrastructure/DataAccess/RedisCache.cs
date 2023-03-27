using Domino.Api.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Domino.Api.Infrastructure.DataAccess;

public class RedisCache : IRedisCache
{
    private readonly IDistributedCache _cache;
    private readonly IConfiguration _configuration;
    public readonly int _expirationMinutes;
    private readonly DistributedCacheEntryOptions _defaultCacheOptions;
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public RedisCache(IConfiguration configuration, IDistributedCache cache)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _cache = cache;
        _expirationMinutes = int.Parse(_configuration["RedisExpirationTime"]!);
        _defaultCacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_expirationMinutes)
        };
        _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }

    public async Task<T?> Get<T>(string key) where T : class
    {
        string? cachedResponse = await _cache.GetStringAsync(key);

        return cachedResponse == null ? null : JsonConvert.DeserializeObject<T>(cachedResponse, _jsonSerializerSettings);
    }

    public async Task Set<T>(string key, T value) where T : class
    {
        string response = JsonConvert.SerializeObject(value, _jsonSerializerSettings);

        await _cache.SetStringAsync(key, response, _defaultCacheOptions);
    }
}