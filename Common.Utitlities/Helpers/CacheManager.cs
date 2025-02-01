using Common.Utitlities.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace Common.Utitlities.Helpers
{
    //This is a generic in-memory cache manager used to manage small resources
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;

        public CacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void SetCacheItem<T>(string key, T value)
        {
            _cache.Set(key, value);
        }

        public T GetCacheItem<T>(string key)
        {
            return _cache.Get<T>(key);
        }
    }
}
