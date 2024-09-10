namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    /// <summary>
    /// <para>Recursively "folds" all 1-bits into upper (left) 0-bits, ending with top (left) bits (from LS1B on) set to 1.</para>
    /// <para>The process yields a bit vector with the same least-significant-1-bit as <paramref name="value"/>, and all 1's above it.</para>
    /// </summary>
    /// <returns>All bits set from LSB up, or -1 if the value is less than zero.</returns>
    public static TValue BitFoldLeft<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => TValue.IsZero(value)
      ? value
      : (value is System.Numerics.BigInteger ? Numerics.GenericMath.BitMaskRight(TValue.CreateChecked(value.GetBitCount())) : ~TValue.Zero) << value.GetTrailingZeroCount();
    //var tzc = value.GetTrailingZeroCount();
    //return BitFoldRight(value << value.GetLeadingZeroCount()) >> tzc << tzc;

    /// <summary>
    /// <para>Recursively "folds" all 1-bits into lower (right) 0-bits, by taking the most-significant-1-bits (MS1B) and OR it with (MS1B - 1), ending with bottom (right) bits (from MS1B on) set to 1.</para>
    /// <para>The process yields a bit vector with the same most-significant-1-bit as <paramref name="value"/>, and all 1's below it.</para>
    /// </summary>
    /// <returns>All bits set from MSB down, or -1 (all bits) if the value is less than zero.</returns>
    public static TValue BitFoldRight<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => TValue.IsZero(value)
      ? value
      : (((value.MostSignificant1Bit() - TValue.One) << 1) | TValue.One);

#if INCLUDE_SWAR

    /// <summary>
    /// <para>This is the traditional SWAR algorithm that recursively "folds" the upper bits into the lower bits.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TValue SwarFoldLeft<TValue>(this TValue source)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      // Loop to accomodate dynamic data types, but works like traditional unrolled 32-bit SWAR:
      //source |= (source << 16);
      //source |= (source << 8);
      //source |= (source << 4);
      //source |= (source << 2);
      //source |= (source << 1);

      for (var shift = GetBitCount(source); shift > 0; shift >>= 1)
        source |= source << shift;

      return source;
    }

    /// <summary>
    /// <para>This is the traditional SWAR algorithm that recursively "folds" the upper bits into the lower bits.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TValue SwarFoldRight<TValue>(this TValue source)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      // Loop to accomodate dynamic data types, but works like traditional unrolled 32-bit SWAR:
      //source |= (source >> 16);
      //source |= (source >> 8);
      //source |= (source >> 4);
      //source |= (source >> 2);
      //source |= (source >> 1);

      for (var shift = GetBitCount(source); shift > 0; shift >>= 1)
        source |= source >>> shift; // Unsigned shift right.

      return source;
    }

#endif

  }
}
