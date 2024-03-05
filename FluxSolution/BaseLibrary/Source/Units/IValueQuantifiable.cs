namespace Flux
{
  /// <summary>An interface representing a quantifiable value.</summary>
  /// <typeparam name="TValue">The value type.</typeparam>
  public interface IValueQuantifiable<TValue>
    : System.IFormattable
    where TValue : struct, System.IEquatable<TValue>
  {
    /// <summary>The value of the quantity.</summary>
    TValue Value { get; }
  }
}
