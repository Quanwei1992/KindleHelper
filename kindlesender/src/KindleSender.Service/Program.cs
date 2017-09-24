using System.ServiceProcess;

namespace KindleSender.Service
{
  public static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public static void Main()
    {
      var servicesToRun = new ServiceBase[] 
      { 
        new KindleSenderService()
      };

      ServiceBase.Run(servicesToRun);
    }
  }
}
