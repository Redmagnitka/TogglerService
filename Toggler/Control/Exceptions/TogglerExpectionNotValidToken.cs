namespace Toggler.Control.Exceptions
{
  [System.Serializable]
  public class TogglerExpectionNotValidToken : System.Exception
  {
    public TogglerExpectionNotValidToken() { }
    public TogglerExpectionNotValidToken(string message) : base(message) { }
    public TogglerExpectionNotValidToken(string message, System.Exception inner) : base(message, inner) { }
    protected TogglerExpectionNotValidToken(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}