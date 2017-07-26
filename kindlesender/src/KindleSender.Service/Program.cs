using System.ServiceProcess;
using log4net;
using log4net.Config;

namespace KindleSender.Service
{
  public static class Program
  {
    private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public static void Main()
    {
      XmlConfigurator.Configure();

      var servicesToRun = new ServiceBase[] 
      { 
        new KindleSenderService()
      };

      ServiceBase.Run(servicesToRun);
    }
  }
}
