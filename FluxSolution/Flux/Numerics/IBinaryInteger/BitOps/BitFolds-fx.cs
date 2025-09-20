namespace Flux
{
  // <seealso cref="http://aggregate.org/MAGIC/"/>
  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class BitOps
  {
    extension<TInteger>(TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region BitFold

      /// <summary>
      /// <para>Recursively "folds" all 1-bits into upper (left) bits, ending with top (left) bits (from LS1B on) set to 1.</para>
      /// <para>The process yields a bit vector with the same least-significant-1-bit as <paramref name="value"/>, and all 1's above it.</para>
      /// </summary>
      /// <returns>All bits set from LS1B up, or -1 if the value is less than zero.</returns>
      public TInteger BitFoldLeft()
        => TInteger.IsZero(value)
        ? value
        : (value is System.Numerics.BigInteger ? TInteger.CreateChecked(value.GetBitCount()).CreateBitMaskRight() : ~TInteger.Zero) << int.CreateChecked(TInteger.TrailingZeroCount(value));
      //var tzc = value.GetTrailingZeroCount();
      //return BitFoldRight(value << value.GetLeadingZeroCount()) >> tzc << tzc;

      /// <summary>
      /// <para>Recursively "folds" all 1-bits into lower (right) bits, by taking the most-significant-1-bit (MS1B) and OR it with (MS1B - 1), ending with bottom (right) bits (from MS1B on) set to 1.</para>
      /// <para>The process yields a bit vector with the same most-significant-1-bit as <paramref name="value"/>, and all 1's below it.</para>
      /// </summary>
      /// <returns>All bits set from MS1B down, or -1 (all bits) if the value is less than zero.</returns>
      public TInteger BitFoldRight()
        => TInteger.IsZero(value)
        ? value
        : (((value.MostSignificant1Bit() - TInteger.One) << 1) | TInteger.One);

      #endregion

#if EXCLUDE_SCRATCH

      /// <summary>
      /// <para>This is the traditional SWAR algorithm that recursively "folds" the lower bits into the upper bits, i.e. folded left or towards the MSB.</para>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public TInteger ScratchBitFoldLeft()
      {
        // Or loop to accomodate dynamic data types, but works like the traditional unrolled SWAR below:
        for (var shift = GetBitCount(value) >> 1; shift > 0; shift >>= 1)
          value |= value << shift;

        // value |= (value << 64); // For a 128-bit type.
        // value |= (value << 32); // For a 64-bit type.
        // value |= (value << 16); // For a 32-bit type
        // value |= (value << 8);
        // value |= (value << 4);
        // value |= (value << 2);
        // value |= (value << 1);

        return value;
      }

      /// <summary>
      /// <para>This is the traditional SWAR algorithm that recursively "folds" the upper bits into the lower bits, i.e. folded right or towards the LSB.</para>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public TInteger ScratchBitFoldRight()
      {
        // Or loop to accomodate dynamic data types, but works like traditional unrolled SWAR below:
        for (var shift = GetBitCount(value); shift > 0; shift >>= 1)
          value |= value >>> shift; // Unsigned shift right.

        // value |= (value >> 64); // For a 128-bit type.
        // value |= (value >> 32); // For a 64-bit type.
        // value |= (value >> 16); // For a 32-bit type
        // value |= (value >> 8);
        // value |= (value >> 4);
        // value |= (value >> 2);
        // value |= (value >> 1);

        return value;
      }

#endif
    }
  }
}
