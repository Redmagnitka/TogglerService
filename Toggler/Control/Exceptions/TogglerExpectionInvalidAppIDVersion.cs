namespace Toggler.Control.Exceptions
{
  [System.Serializable]
  public class TogglerExpectionInvalidAppIDVersion : System.Exception
  {
    public TogglerExpectionInvalidAppIDVersion() { }
    public TogglerExpectionInvalidAppIDVersion(string message) : base(message) { }
    public TogglerExpectionInvalidAppIDVersion(string message, System.Exception inner) : base(message, inner) { }
    protected TogglerExpectionInvalidAppIDVersion(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}