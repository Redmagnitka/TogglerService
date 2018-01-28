namespace Toggler.Control.Exceptions
{
  [System.Serializable]
  public class TogglerExceptionNotAdmin : System.Exception
  {
    public TogglerExceptionNotAdmin() { }
    public TogglerExceptionNotAdmin(string message) : base(message) { }
    public TogglerExceptionNotAdmin(string message, System.Exception inner) : base(message, inner) { }
    protected TogglerExceptionNotAdmin(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}