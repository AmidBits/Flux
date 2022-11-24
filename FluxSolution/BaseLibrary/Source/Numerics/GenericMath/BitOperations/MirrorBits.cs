namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Mirror (or reverses) the bits of a byte.</summary>
    public static byte MirrorBits(this byte value)
      => (byte)((value * (System.UInt128)0x0202020202 & (System.UInt128)0x010884422010) % 1023);
  }
}
