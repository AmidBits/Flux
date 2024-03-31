namespace Flux
{
  /// <summary>An interface representing a quantifiable value.</summary>
  /// <typeparam name="TValue">The value type.</typeparam>
  /// <remarks>
  /// <para>If use of <see cref="System.IConvertible"/> is desirable, use <see cref="IValueQuantifiable{TValue}.Value"/> as a parameter for such functionality.</para>
  /// </remarks>
  public interface IValueQuantifiable<TValue>
    : System.IFormattable
    where TValue : struct, System.IEquatable<TValue>
  {
    /// <summary>
    /// <para>The value of the quantity.</para>
    /// </summary>
    TValue Value { get; }
  }
}
