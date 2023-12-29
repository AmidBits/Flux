namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER
    //public static int ILog2<TSelf>(this TSelf value) where TSelf : System.Numerics.IBinaryNumber<TSelf>
    //  =>TSelf. int.CreateChecked(TSelf.Log2(value));

    /// <summary>Computes the ceiling integer-log-2 (a.k.a. ceiling-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index + 1 of a power-of-2 <paramref name="value"/>.</summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The ceiling integer log2 of <paramref name="value"/>.</returns>
    public static TSelf IntegerLog2Ceiling<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.Log2(value) is var log2 && TSelf.IsPow2(value) ? log2 : log2 + TSelf.One;

    /// <summary>Computes the floor integer-log-2 (a.k.a. floor-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index of a power-of-2 <paramref name="value"/>.</summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The floor integer-log-2 of <paramref name="value"/>.</returns>
    /// <remarks>The ceiling log2 of <paramref name="value"/> = (x > 1 ? GetIntegerLog2Floor(x - 1) + 1 : 0).</remarks>
    public static TSelf GetIntegerLog2Floor<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.Log2(value);

    public static (TSelf Ilog2TowardZero, TSelf Ilog2AwayFromZero) IntegerLog2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      TSelf ilog2TowardZero, ilog2AwayFromZero;

      if (TSelf.IsNegative(value))
      {
        (ilog2TowardZero, ilog2AwayFromZero) = IntegerLog2(TSelf.Abs(value));

        return (-ilog2TowardZero, -ilog2AwayFromZero);
      }

      ilog2TowardZero = value.GetIntegerLog2Floor();
      ilog2AwayFromZero = TSelf.IsPow2(value) ? ilog2TowardZero : ilog2TowardZero + TSelf.One;

      return (ilog2TowardZero, ilog2AwayFromZero);
    }

#else

    /// <summary>Computes the integer log2 (a.k.a. floor log2) of <paramref name="value"/>.</summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The integer log2 of <paramref name="value"/>.</returns>
    /// <remarks>The ceiling log2 of <paramref name="value"/> = (x > 1 ? IntegerLog2(x - 1) + 1 : 0).</remarks>
    public static int IntegerLog2(this System.Numerics.BigInteger value) => (int)System.Numerics.BigInteger.Log(value, 2);

    /// <summary>Computes the integer log2 (a.k.a. floor log2) of <paramref name="value"/>.</summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The integer log2 of <paramref name="value"/>.</returns>
    /// <remarks>The ceiling log2 of <paramref name="value"/> = (x > 1 ? IntegerLog2(x - 1) + 1 : 0).</remarks>
    public static int IntegerLog2(this int value) => value >= 0 ? System.Numerics.BitOperations.Log2(unchecked((uint)value)) : throw new System.ArgumentOutOfRangeException(nameof(value));

    /// <summary>Computes the integer log2 (a.k.a. floor log2) of <paramref name="value"/>.</summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The integer log2 of <paramref name="value"/>.</returns>
    /// <remarks>The ceiling log2 of <paramref name="value"/> = (x > 1 ? IntegerLog2(x - 1) + 1 : 0).</remarks>
    public static int IntegerLog2(this long value) => value >= 0 ? System.Numerics.BitOperations.Log2(unchecked((ulong)value)) : throw new System.ArgumentOutOfRangeException(nameof(value));

#endif
  }
}
