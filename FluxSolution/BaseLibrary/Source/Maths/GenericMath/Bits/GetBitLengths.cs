namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Gets the length, in bits, of the shortest two's-complement representation of <paramref name="value"/>. The bit-length also serves as the bit position of a power-of-2 <paramref name="value"/>.</summary>
    /// <remarks>This is using the .NET built-in functionality without modifications to the result.</remarks>
    public static int GetShortestBitLength<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.GetShortestBitLength();

    /// <summary>Gets the size, in bits, of the shortest two's-complement representation of <paramref name="value"/>, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TSelf"/>, based on byte-count (times 8). This bit-length also serves as the bit position of a power-of-2 <paramref name="value"/>.</summary>
    /// <remarks>Some datatypes, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies. This means that the bit-count also is dynamic.</remarks>
    public static int GetBitLength<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsNegative(value)
      ? value.GetByteCount() * 8 // When value is negative, return the bit-length based on the storage strategy (measured in bytes).
      : value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

#else

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    public static int GetShortestBitLength(this System.Numerics.BigInteger value) => (int)value.GetBitLength();

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    public static int GetShortestBitLength(this int value) => unchecked(((uint)value).GetShortestBitLength());

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    public static int GetShortestBitLength(this long value) => unchecked(((ulong)value).GetShortestBitLength());

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    [System.CLSCompliant(false)] public static int GetShortestBitLength(this uint value) => 1 + System.Numerics.BitOperations.Log2(value);

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    [System.CLSCompliant(false)] public static int GetShortestBitLength(this ulong value) => 1 + System.Numerics.BitOperations.Log2(value);

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns (<paramref name="value"/>.GetByteCount() * 8) instead.</summary>
    public static int GetBitLength(this System.Numerics.BigInteger value) => value < 0 ? value.GetByteCount() * 8 : (int)value.GetBitLength();

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns 32 instead.</summary>
    public static int GetBitLength(this int value) => value < 0 ? 32 : 1 + System.Numerics.BitOperations.Log2(unchecked((uint)value));

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns 64 instead.</summary>
    public static int GetBitLength(this long value) => value < 0 ? 64 : 1 + System.Numerics.BitOperations.Log2(unchecked((ulong)value));

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns 32 instead.</summary>
    [System.CLSCompliant(false)] public static int GetBitLength(this uint value) => 1 + System.Numerics.BitOperations.Log2(value);

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns 64 instead.</summary>
    [System.CLSCompliant(false)] public static int GetBitLength(this ulong value) => 1 + System.Numerics.BitOperations.Log2(value);

#endif
  }
}
