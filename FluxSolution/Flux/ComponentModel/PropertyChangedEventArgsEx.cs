namespace Flux.ComponentModel
{
  /// <summary>
  /// <para>Extension of PropertyChangedEventArgs to include Sender.</para>
  /// </summary>
  public sealed class PropertyChangedEventArgsEx(string? propertyName, object? sender)
    : System.ComponentModel.PropertyChangedEventArgs(propertyName)
  {
    public object? Sender { get; private set; } = sender;
  }
}
