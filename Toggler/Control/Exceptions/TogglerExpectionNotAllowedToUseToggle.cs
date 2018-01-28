namespace Toggler.Control.Exceptions
{
  [System.Serializable]
  public class TogglerExpectionNotAllowedToUseToggle : System.Exception
  {
    public TogglerExpectionNotAllowedToUseToggle() { }
    public TogglerExpectionNotAllowedToUseToggle(string message) : base(message) { }
    public TogglerExpectionNotAllowedToUseToggle(string message, System.Exception inner) : base(message, inner) { }
    protected TogglerExpectionNotAllowedToUseToggle(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}