using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Toggler.Domain;

namespace Toggler.Control.Contracts
{
  public interface IServiceNotification
  {
    Task<HttpResponseMessage> Notify(NotifyMessage msg);
  }

  [DataContract]
  public class NotifyMessage
  {
    [DataMember]
    public string eventMsg;
    [DataMember]
    public Toggle feature;
    public NotifyMessage(string msg, Toggle toggle)
    {
      this.eventMsg = msg;
      this.feature = toggle;
    }
  }
}