namespace Toggler.Control.Exceptions
{
  [System.Serializable]
  public class TogglerExceptionTogglerCreationFailed : System.Exception
  {
    public TogglerExceptionTogglerCreationFailed() { }
    public TogglerExceptionTogglerCreationFailed(string message) : base(message) { }
    public TogglerExceptionTogglerCreationFailed(string message, System.Exception inner) : base(message, inner) { }
    protected TogglerExceptionTogglerCreationFailed(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}