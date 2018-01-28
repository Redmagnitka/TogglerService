using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using Toggler.Control;
using Toggler.Control.Exceptions;
using Toggler.Domain;

namespace web.Controllers
{

    [Route("api/[controller]")]
  public class TogglesController : Controller
  {
    private CTLToggle t_ctl;

    public TogglesController() => t_ctl = CTLToggle.Instance;

    // curl -v -X PUT 'http://127.0.0.1:5000/api/toggles/feat-alpha' -H'Content-Type: application/json' -H'Authorization: JPN mytoken' -H'Content-Length: 1'
    [HttpPut("{featureID}")]
    public JsonResult insertToggle(string featureID)
    {
      JsonResult res = Json("{}");
      this.Response.StatusCode = 201;

      string token;
      try
      {
        StringValues auth;
        this.Request.Headers.TryGetValue("Authorization", out auth);
        token = this.t_ctl.authenticationService.isSendingHeaderAuth(auth.First());
      }
      catch (TogglerExpectionInvalidHeaderAuth)
      {
        this.Response.StatusCode = 401;
        return res;
      }

      try
      {
        Toggle t = this.t_ctl.InsertToggle(featureID, token);
        res = Json(t);
      }
      catch (TogglerExceptionNotAdmin)
      {
        this.Response.StatusCode = 403;
      }
      catch (TogglerExpectionNotValidToken)
      {
        this.Response.StatusCode = 401;
      }
      catch (TogglerExceptionTogglerCreationFailed)
      {
        this.Response.StatusCode = 409;
      }

      return res;
    }

    // curl -v -X GET 'http://127.0.0.1:5000/api/toggles/feat-alpha' -H'Content-Type: application/json' -H'Authorization: JPN mytoken'
    [HttpGet("{featureID}")]
    public JsonResult retrieveToggle(string featureID)
    {
      JsonResult res = Json("{}");
      this.Response.StatusCode = 200;

      string token;
      try
      {
        StringValues auth;
        this.Request.Headers.TryGetValue("Authorization", out auth);
        token = this.t_ctl.authenticationService.isSendingHeaderAuth(auth.First());
      }
      catch (TogglerExpectionInvalidHeaderAuth)
      {
        this.Response.StatusCode = 401;
        return res;
      }

      Dictionary<string, string> appIDVersion;
      try
      {
        StringValues appVersion;
        this.Request.Headers.TryGetValue("FF-ID-Version", out appVersion);

        appIDVersion = this.t_ctl.authorizationService.isSendingHeaderFFIDVersion(appVersion.First());
      }
      catch (System.Exception)
      {
        this.Response.StatusCode = 400;
        return res;
      }

      try
      {
        Toggle toggle = this.t_ctl.retrieveToggle(featureID, token, appIDVersion["id"], appIDVersion["version"]);
        res = Json(toggle);
      }
      catch (TogglerExpectionNotValidToken)
      {
        this.Response.StatusCode = 401;
      }
      catch (TogglerExpectionUnknownToggle)
      {
        this.Response.StatusCode = 404;
      }
      catch (TogglerExpectionInvalidAppIDVersion)
      {
        this.Response.StatusCode = 400;
      }
      catch (TogglerExpectionNotAllowedToUseToggle)
      {
        this.Response.StatusCode = 403;
      }

      return res;
    }

    // curl -v -X PATCH 'http://127.0.0.1:5000/api/toggles/feat-alpha' -H'Content-Type: application/json' -H'Authorization: JPN mytoken' -d'{"isON": false}'
    [HttpPatch("{featureID}")]
    public JsonResult switchONOFF([FromBody]Toggle toggle, string featureID)
    {
   
      JsonResult res = Json("{}");
      this.Response.StatusCode = 200;

      string token;
      try
      {
        StringValues auth;
        this.Request.Headers.TryGetValue("Authorization", out auth);
        token = this.t_ctl.authenticationService.isSendingHeaderAuth(auth.First());
      }
      catch (TogglerExpectionInvalidHeaderAuth)
      {
        this.Response.StatusCode = 401;
        return res;
      }

      if (toggle == null)
      {
        this.Response.StatusCode = 400;

        return res;
      }

      // XXX
      // Eventhough you cannot create a Toggle wihout name, "Serialization" can!
      // Hence to assures that payload is compliant before reaching methods that
      // assume that a Toggle must have name
      //
      toggle.Name = featureID;

      try
      {
        Toggle u = this.t_ctl.UpdateToggle(toggle, token, $"toggle {toggle.Name} was changed from isON to [{toggle.isON}]");

        res = Json(u);
      }
      catch (TogglerExpectionNotValidToken)
      {
        this.Response.StatusCode = 401;
      }
      catch (TogglerExpectionUnknownToggle)
      {
        this.Response.StatusCode = 404;
      }
      catch (TogglerExceptionNotAdmin)
      {
        this.Response.StatusCode = 403;
      }

      return res;
    }

    [HttpGet("{featureID}/bl")]
    public JsonResult getBLForToggle(string featureID)
    {
      JsonResult res = Json("{}");
      this.Response.StatusCode = 200;

      // Restricted: Only admin can do it!
      Console.WriteLine("Request blacklist for feature [{0}]", featureID);

      return res;
    }

    [HttpPut("{featureID}/wl")]
    public JsonResult addAppToToggleWL(string featureID, [FromBody]App app)
    {
      JsonResult res = Json("{}");
      this.Response.StatusCode = 200;

      string token;
      try
      {
        StringValues auth;
        this.Request.Headers.TryGetValue("Authorization", out auth);
        token = this.t_ctl.authenticationService.isSendingHeaderAuth(auth.First());
      }
      catch (TogglerExpectionInvalidHeaderAuth)
      {
        this.Response.StatusCode = 401;
        return res;
      }


      if (app == null)
      {
        Console.WriteLine("Error: You must send a request body!");
        this.Response.StatusCode = 400;

        return res;
      }

      try
      {
        Toggle toggle = this.t_ctl.AddAppToToggle(BWList.ListColor.WHITE, featureID, app, token);
        res = Json(toggle);
      }
      catch (TogglerExpectionNotValidToken)
      {
        this.Response.StatusCode = 401;
      }
      catch (TogglerExpectionUnknownToggle)
      {
        this.Response.StatusCode = 404;
      }
      catch (TogglerExceptionNotAdmin)
      {
        this.Response.StatusCode = 403;
      }

      return res;
    }

    [HttpPut("{featureID}/bl")]
    public JsonResult addAppToToggleBL(string featureID, [FromBody]App app)
    {
      JsonResult res = Json("{}");
      this.Response.StatusCode = 200;

      string token;
      try
      {
        StringValues auth;
        this.Request.Headers.TryGetValue("Authorization", out auth);
        token = this.t_ctl.authenticationService.isSendingHeaderAuth(auth.First());
      }
      catch (TogglerExpectionInvalidHeaderAuth)
      {
        this.Response.StatusCode = 401;
        return res;
      }

      if (app == null)
      {
        Console.WriteLine("Err: You must send a request body!");
        this.Response.StatusCode = 400;
        return res;
      }

      Console.WriteLine("Add App [{0}].[{1}] to the blacklist: [{2}]", app.ID, app.Version, featureID);

      try
      {
        Toggle toggle = this.t_ctl.AddAppToToggle(BWList.ListColor.BLACK, featureID, app, token);
        res = Json(toggle);
      }
      catch (TogglerExpectionNotValidToken)
      {
        this.Response.StatusCode = 401;
      }
      catch (TogglerExpectionUnknownToggle)
      {
        this.Response.StatusCode = 404;
      }
      catch (TogglerExceptionNotAdmin)
      {
        this.Response.StatusCode = 403;
      }

      return res;
    }

    [HttpDelete("{featureID}/bl")]
    public JsonResult removeAllAppsFromToggleBL(string featureID)
    {
      JsonResult res = Json("{}");
      this.Response.StatusCode = 200;

      // TODO endpoint DELETE /toggles/:id/bl
      Console.WriteLine("Remove all elements from the blacklist: [{0}]", featureID);

      return res;
    }

    [HttpDelete("{featureID}/bl/{appID}")]
    public JsonResult removeSingleAppFromToggleBL(string featureID)
    {
      JsonResult res = Json("{}");
      this.Response.StatusCode = 200;

      // TODO endpoint DELETE /toggles/:id/bl/:app_id
      Console.WriteLine("Remove an App from blacklist: [{0}]", featureID);

      return res;
    }

   

  }
}