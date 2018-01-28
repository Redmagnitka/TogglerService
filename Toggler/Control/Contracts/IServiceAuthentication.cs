namespace Toggler.Control.Contracts
{
    public interface IServiceAuthentication
  {
    bool isValidToken(string token);
    bool isAdmin(string token);
    string isSendingHeaderAuth(string auth);
  }
}