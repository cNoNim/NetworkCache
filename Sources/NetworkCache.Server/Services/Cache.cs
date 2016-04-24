using NFX;
using NFX.ApplicationModel.Pile;
using NFX.Environment;

namespace NetworkCache.Services
{
  public class Cache : DisposableObject, ICache
  {
    [Config]
    private string defaultTable = "<default>";

    LocalCache cache = new LocalCache();

    public Cache()
    {
      cache.Pile = new DefaultPile(cache);
      ConfigAttribute.Apply(this, App.ConfigRoot);
      cache.Configure(App.ConfigRoot);
      cache.Start();
    }

    public object Echo(object data) =>
      data;

    public object Get(string key, string table) =>
      cache.GetOrCreateTable<string>(table ?? defaultTable).Get(key);

    public bool Put(string key, object data, string table) =>
      cache.GetOrCreateTable<string>(table ?? defaultTable).Put(key, data) != PutResult.Collision;

    public bool Delete(string key, string table) =>
      cache.GetOrCreateTable<string>(table ?? defaultTable).Remove(key);
  }
}
