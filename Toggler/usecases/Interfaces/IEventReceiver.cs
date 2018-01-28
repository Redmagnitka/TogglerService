using Toggler.Domain;

namespace Usecases.Interfaces
{
  public interface IEventReceiver
  {
    void roger(string msg, Toggle toggle);
  }
}