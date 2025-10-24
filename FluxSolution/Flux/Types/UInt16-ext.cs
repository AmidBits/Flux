namespace Flux
{
  public static partial class XtensionUInt16
  {
    extension(System.UInt16)
    {
      /// <summary>
      /// <para>The largest prime number that fits in the type.</para>
      /// </summary>
      [System.CLSCompliant(false)]
      public static ushort LargestPrimeNumber => 65521;
    }

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal of a ushort, i.e. trade place of bit 15 with bit 0 and bit 14 with bit 1 and so on.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static void ReverseBitsInPlace(ref this ushort value)
      => value = (ushort)(((((((byte)(value & 0xFF)) * 0x0202020202UL) & 0x010884422010UL) % 1023) << 8) | ((((((byte)((value >> 8) & 0xFF)) * 0x0202020202UL) & 0x010884422010UL) % 1023)));

  }
}
