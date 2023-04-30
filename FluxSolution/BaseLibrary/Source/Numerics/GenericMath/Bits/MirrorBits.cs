namespace Flux
{
  public static partial class Bits
  {

    /// <summary>Mirror (or reverses) the bits of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</summary>
    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static byte MirrorBits(this byte value) => (byte)((value * 0x0202020202UL & 0x010884422010UL) % 1023);

    //#if NET7_0_OR_GREATER

    //#else

    public static int MirrorBits(this int value) => unchecked((int)((uint)value).MirrorBits());

    public static long MirrorBits(this long value) => unchecked((long)((ulong)value).MirrorBits());

    [System.CLSCompliant(false)]
    public static uint MirrorBits(this uint value)
    {
      value = ((value & 0xFFFF0000) >> 0x10) | ((value & 0x0000FFFF) << 0x10);
      value = ((value & 0xFF00FF00) >> 0x08) | ((value & 0x00FF00FF) << 0x08);
      value = ((value & 0xF0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F) << 0x04);
      value = ((value & 0xCCCCCCCC) >> 0x02) | ((value & 0x33333333) << 0x02);
      value = ((value & 0xAAAAAAAA) >> 0x01) | ((value & 0x55555555) << 0x01);

      return value;
    }

    [System.CLSCompliant(false)]
    public static ulong MirrorBits(this ulong value)
    {
      value = ((value & 0xFFFFFFFF00000000) >> 0x20) | ((value & 0x00000000FFFFFFFF) << 0x20);
      value = ((value & 0xFFFF0000FFFF0000) >> 0x10) | ((value & 0x0000FFFF0000FFFF) << 0x10);
      value = ((value & 0xFF00FF00FF00FF00) >> 0x08) | ((value & 0x00FF00FF00FF00FF) << 0x08);
      value = ((value & 0xF0F0F0F0F0F0F0F0) >> 0x04) | ((value & 0x0F0F0F0F0F0F0F0F) << 0x04);
      value = ((value & 0xCCCCCCCCCCCCCCCC) >> 0x02) | ((value & 0x3333333333333333) << 0x02);
      value = ((value & 0xAAAAAAAAAAAAAAAA) >> 0x01) | ((value & 0x5555555555555555) << 0x01);

      return value;
    }

    //#endif
  }
}
