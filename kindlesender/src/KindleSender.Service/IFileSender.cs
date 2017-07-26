using System.Threading.Tasks;

namespace KindleSender.Service
{
  public interface IFileSender
  {
    void Send(string filePath);
  }
}
