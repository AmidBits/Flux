namespace Flux
{
  public static partial class UInt128Extensions
  {
    extension(System.UInt128)
    {
      /// <summary>
      /// <para>The largest prime number that fits in the type.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static System.UInt128 MaxPrimeNumber => new(0xFFFFFFFFFFFFFFFFul, 0xFFFFFFFFFFFFFF53ul);

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
