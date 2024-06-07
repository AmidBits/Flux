namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Computes the ceiling integer-log-2 (a.k.a. ceiling-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index + 1 of a power-of-2 <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The ceiling integer log2 of <paramref name="value"/>.</returns>
    public static TSelf IntegerLog2AwayFromZero<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(value) ? value : TSelf.CopySign(TSelf.Abs(value) is var v && TSelf.IsPow2(v) ? TSelf.Log2(v) : TSelf.Log2(v) + TSelf.One, value);

    /// <summary>
    /// <para>Computes the floor integer-log-2 (a.k.a. floor-log-2) of <paramref name="value"/>. The log-2 also serves as the bit (0-based) index of a power-of-2 <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value">The value of which to find the log.</param>
    /// <returns>The floor integer-log-2 of <paramref name="value"/>.</returns>
    /// <remarks>The ceiling log2 of <paramref name="value"/>: <code>(<paramref name="value"/> > 1 ? IntegerLog2Floor(<paramref name="value"/> - 1) + 1 : 0)</code></remarks>
    public static TSelf IntegerLog2TowardZero<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsZero(value) ? value : TSelf.CopySign(TSelf.Log2(TSelf.Abs(value)), value);

#if INCLUDE_SWAR

    public static TSelf SwarIntegerLog2<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.PopCount(value.SwarFoldRight() >> 1);

#endif

  }
}
