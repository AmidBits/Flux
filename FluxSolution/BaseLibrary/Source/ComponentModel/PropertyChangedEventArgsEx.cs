namespace Flux
{
  /// <summary>Extension of PropertyChangedEventArgs to include Sender.</summary>
  public sealed class PropertyChangedEventArgsEx
    : System.ComponentModel.PropertyChangedEventArgs
  {
    public object? Sender { get; private set; }

    public PropertyChangedEventArgsEx(string? propertyName, object? sender) : base(propertyName) => Sender = sender;
  }
}
