namespace Flux
{
  public static partial class XtensionByte
  {
    extension(System.Byte)
    {
      /// <summary>
      /// <para>The largest prime number that fits in the type.</para>
      /// </summary>
      public static byte MaxPrimeNumber => 251;
    }

    /// <summary>
    /// <para>In-place (by ref) mirror the bits (bit-reversal) of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</para>
    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
    /// </summary>
    public static void ReverseBitsInPlace(ref this byte value)
      => value = (byte)(((value * 0x0202020202UL) & 0x010884422010UL) % 1023);
  }
}
