using System;
using System.Configuration;
using loglibrary;

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
    public override void Load()
    {
        LogHelper.Info("Loading configuration ... ");

      try
      {
        FolderPath = ConfigurationManager.AppSettings["FolderPath"];
        FileFilter = ConfigurationManager.AppSettings["FileFilter"];
        KindleMail = ConfigurationManager.AppSettings["KindleMail"];

        SmtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
        SmtpServer = ConfigurationManager.AppSettings["SmtpServer"];
        SmtpUserName = ConfigurationManager.AppSettings["SmtpUserName"];
        SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];

        LogHelper.Info("Configuration loaded.");
      }
      catch (Exception e)
      {
          LogHelper.Error(e.Message);
      }
      finally
      {
          LogHelper.Flush();
      }
    }
    }
}
