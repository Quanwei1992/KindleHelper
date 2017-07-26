using System.Diagnostics;
using System.ServiceProcess;
using log4net;

namespace KindleSender.Service
{
  public partial class KindleSenderService : ServiceBase
  {
    private static readonly ILog Log = LogManager.GetLogger(typeof(KindleSenderService));
  
    private IFolderWatcher _folderWatcher;

    public KindleSenderService()
    {
      Log.Info("Initializing Kindle Sender Service.");

      InitializeComponent();
      InitializeService();
    }

    protected override void OnStart(string[] args)
    {
      _folderWatcher.Start();

      Log.Info("Kindle Sender Service started.");
    }

    protected override void OnStop()
    {
      _folderWatcher.Stop();

      Log.Info("Kindle Sender Service stoped.");
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
