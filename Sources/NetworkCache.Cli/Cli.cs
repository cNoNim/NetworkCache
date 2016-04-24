using NetworkCache.Clients;
using NFX;
using NFX.ApplicationModel;
using NFX.Environment;
using NFX.IO;
using System;
using System.Diagnostics;

namespace NetworkCache
{
  class Cli : IConfigurable
  {
    [Config]
    private string cacheServiceNode = null;

    public void Configure(IConfigSectionNode node) =>
      ConfigAttribute.Apply(this, node);

    public void Run(string[] args)
    {
      using (var client = new CacheClient(cacheServiceNode))
      {
        client.Put("message", "hello");
        Console.WriteLine(client.Get("message"));
      }
    }

    [STAThread]
    static int Main(string[] args)
    {
      try
      {
        using (var application = new ServiceBaseApplication(args, null))
        {
          var shell = new Cli();
          var watch = Stopwatch.StartNew();
          shell.Configure(App.ConfigRoot);
          shell.Run(args);
          watch.Stop();
          ConsoleUtils.Info("time: " + watch.Elapsed);
          Console.Write("Press [ENTER] to exit...");
          Console.ReadLine();
        }
        return 0;
      }
      catch (Exception ex)
      {
        ConsoleUtils.Error(ex.ToMessageWithType());
        return -1;
      }
    }
  }
}
