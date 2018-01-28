using Toggler.Domain;

namespace Usecases.Interfaces
{
  public interface IEventEmiter
  {
    void NotifyObservers(string msg, Toggle t);
  }
}