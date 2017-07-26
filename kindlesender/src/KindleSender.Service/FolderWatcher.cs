using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using log4net;

namespace KindleSender.Service
{
  public class FolderWatcher : IFolderWatcher, IDisposable
  {
    private static readonly ILog Log = LogManager.GetLogger(typeof(FolderWatcher));

    private readonly IConfiguration _configuration;

    private readonly IFileSender _fileSender;

    private FileSystemWatcher _fileSystemWatcher;

    public FolderWatcher(IConfiguration configuration, IFileSender fileSender)
    {
      if (configuration == null)
      {
        throw new ArgumentNullException("configuration");
      }

      if (fileSender == null)
      {
        throw new ArgumentNullException("fileSender");
      }

      _configuration = configuration;
      _fileSender = fileSender;

      InitilaizeFileSystemWatcher();
    }

    public void Start()
    {
      _fileSystemWatcher.EnableRaisingEvents = true;
    }
    public void Stop()
    {
      _fileSystemWatcher.EnableRaisingEvents = false;
    }

    private void InitilaizeFileSystemWatcher()
    {
      if (!Directory.Exists(_configuration.FolderPath))
      {
        Directory.CreateDirectory(_configuration.FolderPath);
        Log.Info("Created directory: " + _configuration.FolderPath);
      }

      _fileSystemWatcher = new FileSystemWatcher
      {
        Path = _configuration.FolderPath,
        Filter = _configuration.FileFilter,
        EnableRaisingEvents = false
      };

      _fileSystemWatcher.Created += OnFileCreated;

      Log.Info("Watching folder: " + _fileSystemWatcher.Path);
    }

    private void OnFileCreated(object sender, FileSystemEventArgs e)
    {
      Log.Info("New file found: " + e.FullPath);
      _fileSender.Send(e.FullPath);
    }

    public void Dispose()
    {
      _fileSystemWatcher.Created -= OnFileCreated;
      _fileSystemWatcher.Dispose();
    }


  }
}
