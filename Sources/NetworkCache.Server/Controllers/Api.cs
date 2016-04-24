using NetworkCache.Clients;
using NetworkCache.Pages;
using NFX;
using NFX.Environment;
using NFX.Serialization.JSON;
using NFX.Wave.MVC;

namespace NetworkCache.Controllers
{
  public class Api : Controller
  {
    [Config]
    private string cacheServiceNode = null;

    public Api()
    {
      ConfigAttribute.Apply(this, App.ConfigRoot);
    }

    [Action]
    public object Index() =>
      new Index();

    [Action]
    public object Echo(string message)
    {
      using (var client = new CacheClient(cacheServiceNode))
        return new JSONResult(client.Echo(new { message }), null);
    }

    [Action(name: "cache", order: 0, matchScript: "match{methods=GET}")]
    public object Get(string key)
    {
      using (var client = new CacheClient(cacheServiceNode))
      {
        var value = client.Get(key);
        if (value == null)
          return new Http404NotFound();
        return new JSONResult(value, null);
      }
    }

    [Action(name: "cache", order: 0, matchScript: "match{methods=PUT}")]
    public object Put(string key, object value)
    {
      using (var client = new CacheClient(cacheServiceNode))
        return new JSONResult(client.Put(key, value), null);
    }

    [Action(name: "cache", order: 0, matchScript: "match{methods=DELETE}")]
    public object Delete(string key)
    {
      using (var client = new CacheClient(cacheServiceNode))
        return new JSONResult(client.Delete(key), null);
    }
  }
}
