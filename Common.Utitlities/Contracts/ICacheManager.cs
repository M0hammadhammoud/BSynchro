namespace Common.Utitlities.Contracts
{
    public interface ICacheManager
    {
        T GetCacheItem<T>(string key);
        void SetCacheItem<T>(string key, T value);
    }
}
