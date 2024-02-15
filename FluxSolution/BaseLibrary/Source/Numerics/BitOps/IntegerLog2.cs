namespace Flux
{
  public static partial class BitOps
  {
    //public static int ILog2<TSelf>(this TSelf value) where TSelf : System.Numerics.IBinaryNumber<TSelf>
    //  =>TSelf. int.CreateChecked(TSelf.Log2(value));

    /// <summary>
    /// <para>Computes the ceiling integer-log-2 (a.k.a. ceiling-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index + 1 of a power-of-2 <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The ceiling integer log2 of <paramref name="value"/>.</returns>
    public static TSelf IntegerLog2Ceiling<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.Log2(value) is var log2floor && TSelf.IsPow2(value) ? log2floor : log2floor + TSelf.One;

    /// <summary>
    /// <para>Computes the floor integer-log-2 (a.k.a. floor-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index of a power-of-2 <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The floor integer-log-2 of <paramref name="value"/>.</returns>
    /// <remarks>The ceiling log2 of <paramref name="value"/>: <code>(<paramref name="value"/> > 1 ? IntegerLog2Floor(<paramref name="value"/> - 1) + 1 : 0)</code></remarks>
    public static TSelf IntegerLog2Floor<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.Log2(value);

    public static (TSelf ilog2TowardZero, TSelf ilog2AwayFromZero) IntegerLog2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      TSelf ilog2TowardZero, ilog2AwayFromZero;

      if (TSelf.IsNegative(value))
      {
        (ilog2TowardZero, ilog2AwayFromZero) = IntegerLog2(TSelf.Abs(value));

        return (-ilog2TowardZero, -ilog2AwayFromZero);
      }

      ilog2TowardZero = TSelf.Log2(value);
      ilog2AwayFromZero = TSelf.IsPow2(value) ? ilog2TowardZero : ilog2TowardZero + TSelf.One;

      return (ilog2TowardZero, ilog2AwayFromZero);
    }
  }
}
