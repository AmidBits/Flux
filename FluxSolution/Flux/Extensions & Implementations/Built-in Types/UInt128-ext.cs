namespace Flux
{
  public static partial class UInt128Extensions
  {
    internal static readonly UInt128[] m_pow10 = [.. Number.GeometricSequence(System.UInt128.One, System.UInt128.CreateChecked(10)).Where(n => n <= System.UInt128.MaxValue)];

    extension(System.UInt128)
    {
      #region Pow10

      [System.CLSCompliant(false)]
      public static System.UInt128 Pow10(int exponent)
      {
        if ((uint)(exponent + 38) > 76) throw new System.ArgumentOutOfRangeException(nameof(exponent));

        return m_pow10[exponent];
      }

      #endregion

      #region ReverseBits..

      /// <summary>
      /// <para>Bit-reversal of a System.UInt128, i.e. trade place of bit 127 with bit 0 and bit 126 with bit 1 and so on.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static System.UInt128 ReverseBits(System.UInt128 value)
      {
        ReverseBitsInPlace(ref value);

        return value;
      }

      /// <summary>
      /// <para>In-place (by ref) mirror the bits (bit-reversal of a System.UInt128, i.e. trade place of bit 127 with bit 0 and bit 126 with bit 1 and so on.</para>
      /// </summary>
      [System.CLSCompliant(false)]
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
