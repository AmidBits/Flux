namespace Flux
{
  /// <summary>Extension of PropertyChangedEventArgs to include Sender.</summary>
  public class PropertyChangedEventArgsX
    : System.ComponentModel.PropertyChangedEventArgs
  {
    public object Sender { get; private set; }

    public PropertyChangedEventArgsX(string propertyName, object sender)
      : base(propertyName)
    {
      Sender = sender;
    }
  }
}
