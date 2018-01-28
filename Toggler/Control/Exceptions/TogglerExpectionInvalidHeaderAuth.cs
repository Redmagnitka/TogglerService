namespace Toggler.Control.Exceptions
{
  [System.Serializable]
  public class TogglerExpectionInvalidHeaderAuth : System.Exception
  {
    public TogglerExpectionInvalidHeaderAuth() { }
    public TogglerExpectionInvalidHeaderAuth(string message) : base(message) { }
    public TogglerExpectionInvalidHeaderAuth(string message, System.Exception inner) : base(message, inner) { }
    protected TogglerExpectionInvalidHeaderAuth(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}