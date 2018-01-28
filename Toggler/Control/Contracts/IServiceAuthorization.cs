using System.Collections.Generic;

namespace Toggler.Control.Contracts
{
    public interface IServiceAuthorization
  {
    Dictionary<string, string> isSendingHeaderFFIDVersion(string FFIDVersion);
  }
}