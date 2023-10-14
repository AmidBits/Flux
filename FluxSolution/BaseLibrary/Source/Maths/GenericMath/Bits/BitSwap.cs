namespace Flux
{
  public static partial class Bits
  {
    /// <summary></summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap1(ulong value) => ((value & 0xaaaaaaaaaaaaaaaaUL) >> 1) | ((value & 0x5555555555555555UL) << 1);

    /// <summary></summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap2(ulong value) => ((value & 0xccccccccccccccccUL) >> 2) | ((value & 0x3333333333333333UL) << 2);

    /// <summary></summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap4(ulong value) => ((value & 0xf0f0f0f0f0f0f0f0UL) >> 4) | ((value & 0x0f0f0f0f0f0f0f0fUL) << 4);

    /// <summary></summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap8(ulong value) => ((value & 0xff00ff00ff00ff00UL) >> 8) | ((value & 0x00ff00ff00ff00ffUL) << 8);

    /// <summary></summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap16(ulong value) => ((value & 0xffff0000ffff0000UL) >> 16) | ((value & 0x0000ffff0000ffffUL) << 16);

    /// <summary></summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap32(ulong value) => (value >> 32) | (value << 32); // No need to AND bits, we can simply swap the two halfs for a UInt64.
  }
}
