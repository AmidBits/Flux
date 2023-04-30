namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
    /// See <see cref="ReverseBits{TSelf}(TSelf)"/> for bit reversal.
    /// </summary>
    public static TSelf ReverseBytes<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var bytes = new byte[value.GetByteCount()]; // Retrieve the byte size of the number, which will be the basis for the byte reversal.
      value.WriteLittleEndian(bytes);
      System.Array.Reverse(bytes, 0, bytes.Length); // Reverse all bytes.

      return TSelf.ReadLittleEndian(bytes, typeof(System.Numerics.IUnsignedNumber<>).IsSupertypeOf(value.GetType()));
    }

#else

    /// <summary>
    /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
    /// See <see cref="ReverseBits{TSelf}(TSelf)"/> for bit reversal.
    /// </summary>
    public static int ReverseBytes(this int value) => unchecked((int)((uint)value).ReverseBytes());

    /// <summary>
    /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
    /// See <see cref="ReverseBits{TSelf}(TSelf)"/> for bit reversal.
    /// </summary>
    public static long ReverseBytes(this long value) => unchecked((long)((ulong)value).ReverseBytes());

    /// <summary>
    /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
    /// See <see cref="ReverseBits{TSelf}(TSelf)"/> for bit reversal.
    /// </summary>
    [System.CLSCompliant(false)]
    public static uint ReverseBytes(this uint value)
      => ((value << 24) & 0xFF000000) | ((value << 8) & 0x00FF0000)
      | ((value >> 8) & 0x0000FF00) | ((value >> 24) & 0x000000FF);

    /// <summary>
    /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
    /// See <see cref="ReverseBits{TSelf}(TSelf)"/> for bit reversal.
    /// </summary>
    [System.CLSCompliant(false)]
    public static ulong ReverseBytes(this ulong value)
      => ((value << 56) & 0xFF000000_00000000) | ((value << 32) & 0x00FF0000_00000000) | ((value << 16) & 0x0000FF00_00000000) | ((value << 8) & 0x000000FF_00000000)
      | ((value >> 8) & 0x00000000_FF000000) | ((value >> 16) & 0x00000000_00FF0000) | ((value >> 32) & 0x00000000_0000FF00) | ((value >> 56) & 0x00000000_000000FF);

#endif
  }
}
