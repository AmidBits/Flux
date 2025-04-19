namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Interleave bits of byte <paramref name="x"/> and byte <paramref name="y"/>, so that all of the bits of <paramref name="x"/> are in the even positions and <paramref name="y"/> in the odd, resulting in a Morton Number.</para>
    /// </summary>
    /// <see href="http://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
    public static int MortonNumber(this byte x, byte y)
      => unchecked((int)(((x * 0x0101010101010101UL & 0x8040201008040201UL) * 0x0102040810204081UL >> 49) & 0x5555 | ((y * 0x0101010101010101UL & 0x8040201008040201UL) * 0x0102040810204081UL >> 48) & 0xAAAA));
  }
}
