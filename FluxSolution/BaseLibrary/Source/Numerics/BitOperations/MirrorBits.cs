namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Mirror (or reverses) the bits of a byte, i.e. trade place of bit 7 with bit 0 and bit 6 with bit 1 and so on.</summary>
    /// <see href="http://www.inwap.com/pdp10/hbaker/hakmem/hakmem.html"/>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static byte MirrorBits(this byte value) => (byte)((value * 0x0202020202UL & 0x010884422010UL) % 1023);
  }
}
