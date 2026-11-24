namespace Flux
{
  [System.CLSCompliant(false)]
  public static partial class UInt128Extensions
  {
    #region Pow10 table

    /// <summary>
    /// <para>Table of Pow10 values up to 10^38.</para>
    /// </summary>
    internal static readonly System.UInt128[] Pow10 = [.. Number.GeometricSequence(System.UInt128.One, NumberBaseConstants<System.UInt128>.Ten).TakeWhile(n => n <= System.UInt128.MaxValue)];

    #endregion

    extension(System.UInt128)
    {
      #region Try/FastPow10Magnitude

      /// <summary>
      /// <para>Fast-path power-of-10 from precomputed values. Supported exponent magnitudes are in the range [0, 38], corresponding to powers from 1 through 10^38.</para>
      /// </summary>
      /// <param name="exponent"></param>
      /// <returns>10^<paramref name="exponent"/></returns>
      public static System.UInt128 FastPow10Magnitude(int exponent)
      {
        var values = Pow10;

        var absExponent = int.Abs(exponent);

        if (absExponent >= values.Length) throw new System.ArgumentOutOfRangeException(nameof(exponent));

        return values[absExponent];
      }

      /// <summary>
      /// <para>Attempts fast-path power-of-10 from precomputed values. Supported exponent magnitudes are in the range [0, 38], corresponding to powers from 1 through 10^38.</para>
      /// </summary>
      /// <param name="exponent"></param>
      /// <param name="pow10">10^<paramref name="exponent"/></param>
      /// <returns>true if the value was computed successfully; otherwise, false.</returns>
      public static bool TryFastPow10Magnitude(int exponent, out System.UInt128 pow10)
      {
        var values = Pow10;

        var absExponent = int.Abs(exponent);

        if (absExponent >= values.Length)
        {
          pow10 = 0;
          return false;
        }

        pow10 = values[absExponent];
        return true;
      }

      #endregion

      #region ReverseBits..

      /// <summary>
      /// <para>Bit-reversal of a System.UInt128, i.e. trade place of bit 127 with bit 0 and bit 126 with bit 1 and so on.</para>
      /// </summary>
      public static System.UInt128 ReverseBits(System.UInt128 value)
      {
        ReverseBitsInPlace(ref value);

        return value;
      }

      /// <summary>
      /// <para>In-place (by ref) mirror the bits (bit-reversal of a System.UInt128, i.e. trade place of bit 127 with bit 0 and bit 126 with bit 1 and so on.</para>
      /// </summary>
      public static void ReverseBitsInPlace(ref System.UInt128 value)
      {
        var upper = (ulong)(value >> 64);
        var lower = (ulong)value;

        ulong.ReverseBitsInPlace(ref upper);
        ulong.ReverseBitsInPlace(ref lower);

        value = new System.UInt128(lower, upper); // Reverse upper and lower, because lower bits are now upper and upper is lower.
      }

      #endregion
    }
  }
}
