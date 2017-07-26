namespace KindleSender.Service
{
  public interface IConfiguration
  {
    string FolderPath { get; set; }

    string FileFilter { get; }
    string KindleMail { get; set; }
    int SmtpPort { get; set; }
    string SmtpServer { get; set; }
    string SmtpUserName { get; set; }
    string SmtpPassword { get; set; }

    void Load();
  }
}
