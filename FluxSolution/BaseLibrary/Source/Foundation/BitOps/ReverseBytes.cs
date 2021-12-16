namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Computes the reverse bytes a value.</summary>
    public static int ReverseBytes(int value)
      => unchecked((int)ReverseBytes((uint)value));
    /// <summary>Computes the reverse bytes a value.</summary>
    public static long ReverseBytes(long value)
      => unchecked((long)ReverseBytes((ulong)value));

    /// <summary>Computes the reverse bytes a value.</summary>
    [System.CLSCompliant(false)]
    public static uint ReverseBytes(uint value)
      => (value & 0xFFU) << 24 | (value & 0xFF00U) << 8 | (value & 0xFF0000U) >> 8 | (value & 0xFF000000U) >> 24;
    /// <summary>Computes the reverse bytes a value.</summary>
    [System.CLSCompliant(false)]
    public static ulong ReverseBytes(ulong value)
      => (value & 0xFFUL) << 56 | (value & 0xFF00UL) << 48 | (value & 0xFF0000UL) >> 40 | (value & 0xFF000000UL) >> 32 | (value & 0xFF00000000UL) >> 32 | (value & 0xFF0000000000UL) >> 40 | (value & 0xFF000000000000UL) >> 48 | (value & 0xFF00000000000000UL) >> 56;
  }
}
