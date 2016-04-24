using NetworkCache.Services;

namespace NetworkCache
{
  public static class ICacheExtensions
  {
    public static object Get(this ICache cache, string key) => cache.Get(key, null);
    public static bool Put(this ICache cache, string key, object data) => cache.Put(key, data, null);
    public static bool Delete(this ICache cache, string key) => cache.Delete(key, null);
  }
}
