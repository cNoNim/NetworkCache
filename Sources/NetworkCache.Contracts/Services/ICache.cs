using NFX.Glue;

namespace NetworkCache.Services
{
  [Glued]
  public interface ICache
  {
    object Echo(object data);

    object Get(string key, string table);
    bool Put(string key, object data, string table);
    bool Delete(string key, string table);
  }
}
