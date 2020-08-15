using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace Cache.InMemory
{
    public class InMemoryCache : ICache
    {
        private const double DefaultSlidingExpirationInMinutes = 5.0;
        private readonly IMemoryCache _memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<TObject> GetAsync<TObject>(string key, CancellationToken token = default)
        {
            return Task.Run(() =>
            {
                _memoryCache.TryGetValue(key, out var value);
                return (TObject) value;
            }, token);
        }

        public Task<bool> SetAsync(string key, object value, TimeSpan? expiry = null, CancellationToken token = default)
        {
            return Task.Run(() =>
            {
                try
                {
                    _memoryCache.CreateEntry(key)
                        .SetOptions(new MemoryCacheEntryOptions()
                            .SetPriority(CacheItemPriority.NeverRemove)
                            .SetSlidingExpiration(expiry ?? TimeSpan.FromMinutes(DefaultSlidingExpirationInMinutes)))
                        .SetValue(value);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }, token);
        }
    }
}