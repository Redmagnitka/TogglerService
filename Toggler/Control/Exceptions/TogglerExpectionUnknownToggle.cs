namespace Toggler.Control.Exceptions
{
  [System.Serializable]
  public class TogglerExpectionUnknownToggle : System.Exception
  {
    public TogglerExpectionUnknownToggle() { }
    public TogglerExpectionUnknownToggle(string message) : base(message) { }
    public TogglerExpectionUnknownToggle(string message, System.Exception inner) : base(message, inner) { }
    protected TogglerExpectionUnknownToggle(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}