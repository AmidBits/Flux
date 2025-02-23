namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Swap adjacent 1-bits (single bits).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap1(this ulong value) => ((value & 0xaaaaaaaaaaaaaaaaUL) >> 0x01) | ((value & 0x5555555555555555UL) << 0x01);

    /// <summary>
    /// <para>Swap adjacent 2-bits (pairs).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap2(this ulong value) => ((value & 0xccccccccccccccccUL) >> 0x02) | ((value & 0x3333333333333333UL) << 0x02);

    /// <summary>
    /// <para>Swap adjacent 4-bits (nibbles).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap4(this ulong value) => ((value & 0xf0f0f0f0f0f0f0f0UL) >> 0x04) | ((value & 0x0f0f0f0f0f0f0f0fUL) << 0x04);

    /// <summary>
    /// <para>Swap adjacent 8-bits (bytes).</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap8(this ulong value) => ((value & 0xff00ff00ff00ff00UL) >> 0x08) | ((value & 0x00ff00ff00ff00ffUL) << 0x08);

    /// <summary>
    /// <para>Swap adjacent 16-bits.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap16(this ulong value) => ((value & 0xffff0000ffff0000UL) >> 0x10) | ((value & 0x0000ffff0000ffffUL) << 0x10);

    /// <summary>
    /// <para>Swap adjacent 32-bits.</para>
    /// </summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong BitSwap32(this ulong value) => ((value & 0xffffffff00000000UL) >> 0x20) | ((value & 0x00000000ffffffffUL) << 0x20);
  }
}
