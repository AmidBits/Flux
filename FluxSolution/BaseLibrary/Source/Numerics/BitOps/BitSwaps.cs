namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Swap adjacent 1-bits (single bits).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap1(ulong value) => ((value & 0xaaaaaaaaaaaaaaaaUL) >> 1) | ((value & 0x5555555555555555UL) << 1);

    /// <summary>
    /// <para>Swap adjacent 2-bits (pairs).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap2(ulong value) => ((value & 0xccccccccccccccccUL) >> 2) | ((value & 0x3333333333333333UL) << 2);

    /// <summary>
    /// <para>Swap adjacent 4-bits (nibbles).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap4(ulong value) => ((value & 0xf0f0f0f0f0f0f0f0UL) >> 4) | ((value & 0x0f0f0f0f0f0f0f0fUL) << 4);

    /// <summary>
    /// <para>Swap adjacent 8-bits (Byte).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap8(ulong value) => ((value & 0xff00ff00ff00ff00UL) >> 8) | ((value & 0x00ff00ff00ff00ffUL) << 8);

    /// <summary>
    /// <para>Swap adjacent 16-bits (UInt16).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap16(ulong value) => ((value & 0xffff0000ffff0000UL) >> 16) | ((value & 0x0000ffff0000ffffUL) << 16);

    /// <summary>
    /// <para>Swap adjacent 32-bits (UInt32).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong BitSwap32(ulong value) => (value >> 32) | (value << 32); // For a 64-bit type (e.g. ulong/System.UInt64), there is no need to AND bits, we can simply swap the two halfs.
  }
}
