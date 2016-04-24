using NFX;
using NFX.ApplicationModel;
using NFX.IO;
using NFX.Wave;
using System;

namespace NetworkCache
{
  class Server
  {
    void Run(string[] args)
    {
      using (var wave = new WaveServer())
      {
        wave.Configure(null);
        wave.Start();
        ConsoleUtils.Info("Wave server prefixes:");
        foreach (var prefix in wave.Prefixes)
          ConsoleUtils.Info("  " + prefix);
        ConsoleUtils.Info("Glue servers:");
        foreach (var service in App.Glue.Servers)
          ConsoleUtils.Info("  " + service);
        Console.Write("Press [ENTER] to exit...");
        Console.ReadLine();
      }
    }

    static int Main(string[] args)
    {
      try
      {
        using (var application = new ServiceBaseApplication(args, null))
          new Server().Run(args);
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
