using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Toggler.Control.Contracts;
using Toggler.Control.Exceptions;
using Toggler.Control.Gateways;
using Toggler.Domain;
using Usecases.Interactor;
using Usecases.Interfaces;

namespace Toggler.Control
{
    public class CTLToggle : IEventReceiver
  {
    public static CTLToggle Instance
    {
      get
      {
        if (_instance == null) _instance = new CTLToggle();

        return _instance;
      }
    }
    public IServiceAuthentication authenticationService;
    public IServiceAuthorization authorizationService;
    
    private static CTLToggle _instance;
    private IServiceNotification _notificationService;
    private List<IEventEmiter> _observees = new List<IEventEmiter>();
    private UCIToggle _uciToggle;

    private CTLToggle()
    {
      this._notificationService = new ServiceNotificationRequestBin();
      this.authenticationService = new ServiceAuthenticationDummy();
      this.authorizationService = new ServiceAuthorizationDummy();
      UCIToggle uciT = new UCIToggle();

      this._uciToggle = uciT;

      // NOTE regist as uciT observer
      uciT.observers.Add(this);

      this._observees.Add(uciT);
    }

    public Toggle InsertToggle(string name, string user_token)
    {
      if (!this.authenticationService.isValidToken(user_token)) throw new TogglerExpectionNotValidToken();
      if (!this.authenticationService.isAdmin(user_token)) throw new TogglerExceptionNotAdmin();

      var data = new Dictionary<string, object>();
      data.Add("name", name);

      Toggle res = this._uciToggle.RegistToggle(data);

      if (res == null) throw new TogglerExceptionTogglerCreationFailed();

      return res;
    }

    public Toggle retrieveToggle(string name, string user_token, string appID, string appVersion)
    {
      Toggle res = null;

      if (!authenticationService.isValidToken(user_token)) throw new TogglerExpectionNotValidToken();

      res = _uciToggle.returnToggle(name);

      if (res == null) throw new TogglerExpectionUnknownToggle();

      // NOTE admin can access the toggle
      if (this.authenticationService.isAdmin(user_token)) return res;

      // NOTE other users need to send their id and version.
      if (appID == null || appVersion == null)
            {
                throw new TogglerExpectionInvalidAppIDVersion();
            }

            bool can_use = true;
      if (res != null && res.HasWL() && !res.isAppOnWL(new App(appID, appVersion)))
      {
        Console.WriteLine($"App {appID} is on WL for toggle {res.Name}");
        can_use = false;
      }

      if (res != null && res.HasBL() && res.isAppOnBL(new App(appID, appVersion)))
      {
        Console.WriteLine($"App {appID} is on BL for toggle {res.Name}");
        can_use = false;
      }

      if (!can_use) throw new TogglerExpectionNotAllowedToUseToggle();

      return res;
    }

    public Toggle AddAppToToggle(BWList.ListColor color, string toggleID, App app, string user_token)
    {
      Toggle res = null;

      if (!this.authenticationService.isValidToken(user_token)) throw new TogglerExceptionNotAdmin();

      Toggle t;
      try
      {
        t = this.retrieveToggle(toggleID, user_token, app.ID, app.Version);
      }
      catch (Exception) // NOTE catch em all
      {
        throw;
      }

      try
      {
        t.AddToList(color, app);
        res = this.UpdateToggle(t, user_token, $"Added app [{app.ID}] to  Toggle [{t.Name}] {color} list");
      }
      catch (Exception) 
      {
        t.RemoveFromBW(color, app);
        throw;
      }

      return res;
    }

    public Toggle UpdateToggle(Toggle toggle, string user_token, string msg)
    {
      if (!this.authenticationService.isValidToken(user_token)) throw new TogglerExpectionNotValidToken();
      if (!this.authenticationService.isAdmin(user_token)) throw new TogglerExceptionNotAdmin();
      Toggle res = this._uciToggle.UpdateToggle(toggle, msg);

      if (res == null) throw new TogglerExpectionUnknownToggle();

      return res;
    }

    public void roger(string observee_msg, Toggle toggle)
    {
      Console.WriteLine($"EVENT: {observee_msg}");

      NotifyMessage nm = new NotifyMessage(observee_msg, toggle);
      try
      {
        var task = this._notificationService.Notify(nm);
        task.Wait();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        Console.WriteLine($"INFO: proceed without internet connection! [{JsonConvert.SerializeObject(nm)}]");
      }

    }
  }
}