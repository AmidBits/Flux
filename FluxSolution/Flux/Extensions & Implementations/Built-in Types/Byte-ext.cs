namespace Flux
{
  public static partial class ByteExtensions
  {
    /// <summary>
    /// <para>The largest prime number that fits in a <see cref="System.Byte"/>.</para>
    /// </summary>
    public const byte MaxPrimeNumber = 251;

    extension(System.Byte)
    {

      #region ReverseBits..

      /// <summary>
      /// <para>Bit-reversal of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</para>
      /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
      /// </summary>
      public static byte ReverseBits(byte value)
      {
        ReverseBitsInPlace(ref value);

        return value;
      }

      /// <summary>
      /// <para>In-place (by ref) mirror the bits (bit-reversal) of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</para>
      /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
      /// </summary>
      public static void ReverseBitsInPlace(ref byte value)
        => value = (byte)(((value * 0x0202020202UL) & 0x010884422010UL) % 1023);

      #endregion

      #region ToUriPercentEncoding

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static string ToUriPercentEncoding(byte value)
        => $"%{value:X2}";

      #endregion
    }
  }
}
