using loglibrary;
using System.Diagnostics;
using System.ServiceProcess;


namespace KindleSender.Service
{
  public partial class KindleSenderService : ServiceBase
  {
  
    private IFolderWatcher _folderWatcher;

    public KindleSenderService()
    {
      LogHelper.Info("Initializing Kindle Sender Service.");
      InitializeComponent();
      InitializeService();
      LogHelper.Flush();
    }

    protected override void OnStart(string[] args)
    {
      _folderWatcher.Start();
      LogHelper.Info("Kindle Sender Service started.");
      LogHelper.Flush();
    }

    protected override void OnStop()
    {
      _folderWatcher.Stop();
      LogHelper.Info("Kindle Sender Service stoped.");
      LogHelper.Flush();
    }

    private void InitializeService()
    {
      var configuration = new Configuration();
      configuration.Load();

      var fileSender = new FileSender(configuration);

      _folderWatcher = new FolderWatcher(configuration, fileSender);
    }
  }
}
