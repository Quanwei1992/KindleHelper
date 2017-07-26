using System;
using System.Configuration;
using log4net;

namespace KindleSender.Service
{
    public abstract class Base
    {
    public abstract void Load();
        
    public string FolderPath { get; set; }

    public string FileFilter { get; set; }

    public string KindleMail { get; set; }

    public int SmtpPort { get; set; }

    public string SmtpServer { get; set; }

    public string SmtpUserName { get; set; }

    public string SmtpPassword { get; set; }

    }
  public class Configuration : Base,IConfiguration
  {
    private static readonly ILog Log = LogManager.GetLogger(typeof(FolderWatcher));

    //public string FolderPath { get; set; }

    //public string FileFilter { get; set; }

    //public string KindleMail { get; set; }

    //public int SmtpPort { get; set; }

    //public string SmtpServer { get; set; }

    //public string SmtpUserName { get; set; }

    //public string SmtpPassword { get; set; }


    public override void Load()
    {
      Log.Info("Loading configuration ... ");

      try
      {
        FolderPath = ConfigurationManager.AppSettings["FolderPath"];
        FileFilter = ConfigurationManager.AppSettings["FileFilter"];
        KindleMail = ConfigurationManager.AppSettings["KindleMail"];

        SmtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
        SmtpServer = ConfigurationManager.AppSettings["SmtpServer"];
        SmtpUserName = ConfigurationManager.AppSettings["SmtpUserName"];
        SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];

        Log.Info("Configuration loaded.");
      }
      catch (Exception e)
      {
        Log.Error(e.Message);
      }
    }
    }
}
