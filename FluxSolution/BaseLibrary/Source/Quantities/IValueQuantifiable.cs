namespace Flux
{
  namespace Quantities
  {
    /// <summary>
    /// <para>This is for general quantities (where the concept of units are not needed).</para>
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <remarks>
    /// <para>If use of <see cref="System.IConvertible"/> is desirable, use <see cref="Value"/> for such functionality.</para>
    /// </remarks>
    public interface IValueQuantifiable<TValue>
      : System.IFormattable
    where TValue : System.Numerics.INumber<TValue>
    {
      /// <summary>
      /// <para>The value of the quantity.</para>
      /// </summary>
      TValue Value { get; }
    }
  }
}
