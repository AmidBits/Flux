namespace Flux
{
  public static partial class UInt16Extensions
  {
    /// <summary>
    /// <para>The largest prime number that fits in an <see cref="System.UInt16"/>.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public const ushort MaxPrimeNumber = 65521;

    extension(System.UInt16)
    {
      #region ReverseBits..

      /// <summary>
      /// <para>Bit-reversal of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static ushort ReverseBits(ushort value)
      {
        ReverseBitsInPlace(ref value);

        return value;
      }

      /// <summary>
      /// <para>In-place (by ref) mirror the bits (bit-reversal of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static void ReverseBitsInPlace(ref ushort value)
        => value = (ushort)(((((((byte)(value & 0xFF)) * 0x0202020202UL) & 0x010884422010UL) % 1023) << 8) | ((((((byte)((value >> 8) & 0xFF)) * 0x0202020202UL) & 0x010884422010UL) % 1023)));

      #endregion
    }
  }
}
