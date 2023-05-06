namespace Flux
{
  public static partial class Bits
  {

    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</summary>
    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void MirrorBits(ref this byte value) => value = (byte)((value * 0x0202020202UL & 0x010884422010UL) % 1023);

    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of an int, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</summary>
    public static void MirrorBits(ref this int value)
    {
      var ui32 = unchecked((uint)value);
      MirrorBits(ref ui32);
      value = unchecked((int)ui32);
    }

    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of a long, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</summary>
    public static void MirrorBits(ref this long x)
    {
      var ui64 = unchecked((ulong)x);
      MirrorBits(ref ui64);
      x = unchecked((long)ui64);
    }

    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of a uint, i.e. trade place of bit 31 with bit 0 and bit 30 with bit 1 and so on.</summary>
    [System.CLSCompliant(false)]
    public static void MirrorBits(ref this uint x)
    {
      x = ((x & 0xAAAAAAAA) >> 0x01) | ((x & 0x55555555) << 0x01);
      x = ((x & 0xCCCCCCCC) >> 0x02) | ((x & 0x33333333) << 0x02);
      x = ((x & 0xF0F0F0F0) >> 0x04) | ((x & 0x0F0F0F0F) << 0x04);
      x = ((x & 0xFF00FF00) >> 0x08) | ((x & 0x00FF00FF) << 0x08);
      x = ((x & 0xFFFF0000) >> 0x10) | ((x & 0x0000FFFF) << 0x10);
    }

    /// <summary>In-place (by ref) mirror the bits (bit-reversal) of a ulong, i.e. trade place of bit 63 with bit 0 and bit 62 with bit 1 and so on.</summary>
    [System.CLSCompliant(false)]
    public static void MirrorBits(ref this ulong x)
    {
      x = ((x & 0xAAAAAAAAAAAAAAAA) >> 0x01) | ((x & 0x5555555555555555) << 0x01);
      x = ((x & 0xCCCCCCCCCCCCCCCC) >> 0x02) | ((x & 0x3333333333333333) << 0x02);
      x = ((x & 0xF0F0F0F0F0F0F0F0) >> 0x04) | ((x & 0x0F0F0F0F0F0F0F0F) << 0x04);
      x = ((x & 0xFF00FF00FF00FF00) >> 0x08) | ((x & 0x00FF00FF00FF00FF) << 0x08);
      x = ((x & 0xFFFF0000FFFF0000) >> 0x10) | ((x & 0x0000FFFF0000FFFF) << 0x10);
      x = ((x & 0xFFFFFFFF00000000) >> 0x20) | ((x & 0x00000000FFFFFFFF) << 0x20);
    }
  }
}
