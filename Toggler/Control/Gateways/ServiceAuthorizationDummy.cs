using System.Collections.Generic;
using Toggler.Control.Contracts;
using System;
using Toggler.Control.Exceptions;

namespace Toggler.Control.Gateways
{
  public class ServiceAuthorizationDummy : IServiceAuthorization
  {
    public Dictionary<string, string> isSendingHeaderFFIDVersion(string FFIDVersion)
    {
      Dictionary<string, string> res = new Dictionary<string, string>();
      if (FFIDVersion.Length < 2) return null;
      
      string[] IDVersion = FFIDVersion.Split(' ');

      if (IDVersion.Length < 2) return null;

      res.Add("id", IDVersion[0]);
      res.Add("version", IDVersion[1]);

      return res;
    }
  }
}