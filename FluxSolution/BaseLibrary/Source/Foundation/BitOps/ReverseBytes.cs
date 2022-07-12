namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Computes the reverse bytes of a value.</summary>
    public static int ReverseBytes(int value)
      => unchecked((int)ReverseBytes((uint)value));
    /// <summary>Computes the reverse bytes of a value.</summary>
    public static long ReverseBytes(long value)
      => unchecked((long)ReverseBytes((ulong)value));

    /// <summary>Computes the reverse bytes of a value.</summary>
    [System.CLSCompliant(false)]
    public static uint ReverseBytes(uint value)
      => (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8
       | (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
    /// <summary>Computes the reverse bytes of a value.</summary>
    [System.CLSCompliant(false)]
    public static ulong ReverseBytes(ulong value)
      => (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 | (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8
       | (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 | (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
  }
}
