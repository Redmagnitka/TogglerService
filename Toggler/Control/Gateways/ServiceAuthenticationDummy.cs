using System.Collections.Generic;
using Toggler.Control.Contracts;
using System;
using Toggler.Control.Exceptions;

namespace Toggler.Control.Gateways
{
  public class ServiceAuthenticationDummy : IServiceAuthentication
  {
    private HashSet<string> valid_tokens = new HashSet<string> { "admin_token", "app_alpha_token", "app_bravo_token" };

    public bool isAdmin(string token)
    {
      bool res = false;

      if (token == "admin_token")
      {
        Console.WriteLine("ADMINISTRATOR USER");
        res = true;
      }
      else
      {
        Console.WriteLine("NON ADMINISTRATOR USER");
        res = false;
      }

      return res;
    }

    public string isSendingHeaderAuth(string headerAuth)
    {
      string token = this._extractToken(headerAuth);
      if (token == null) throw new TogglerExpectionInvalidHeaderAuth();
           
      return token;
    }

    public bool isValidToken(string token)
    {
      bool res;
      if (this.valid_tokens.Contains(token))
      {
        res = true;
      }
      else
      {
        res = false;
        Console.WriteLine("Invalid token [{0}]", token);
      }

      return res;
    }

    private string _extractToken(string headerAuth)
    {
      if (headerAuth == null) return null;

      string[] userNameToken = headerAuth.Split(' ');
      string token = userNameToken[1];

      if (token == null) return null;

      return token;
    }
  }
}