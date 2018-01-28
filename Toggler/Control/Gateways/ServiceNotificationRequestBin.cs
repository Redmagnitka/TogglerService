using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Toggler.Control.Contracts;

namespace Toggler.Control.Gateways
{
  public class ServiceNotificationRequestBin : IServiceNotification
  {

    private string _notifierService;
    public ServiceNotificationRequestBin()
    {
      string reqBin = Environment.GetEnvironmentVariable("TOGGLER_NOTIFIER");
      if (reqBin.Length == 0)
      {
        Console.WriteLine("ERR: Please edit '.env' file with valid requestb.in url!");
                _notifierService = "http://www.example.com";
      }
      else
      {
        this._notifierService = reqBin;
      }

    }

    public async Task<HttpResponseMessage> Notify(NotifyMessage msg)
    {
      var httpClient = new HttpClient();

      // requestb.in is the service to send notifications.
         //
      // add inspect https://requestb.in/xho8l4xh?inspect to check what was sent


      var myContent = JsonConvert.SerializeObject(msg);
      var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
      var byteContent = new ByteArrayContent(buffer);
      byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

      return await httpClient.PostAsync(new Uri(this._notifierService), byteContent);
    }
  }
}