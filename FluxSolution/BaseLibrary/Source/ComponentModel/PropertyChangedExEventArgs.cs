namespace Flux
{
  /// <summary>Extension of PropertyChangedEventArgs to include Sender.</summary>
  public class PropertyChangedExEventArgs
    : System.ComponentModel.PropertyChangedEventArgs
  {
    public object Sender { get; private set; }

    public PropertyChangedExEventArgs(string propertyName, object sender)
      : base(propertyName)
    {
      Sender = sender;
    }
  }
}
