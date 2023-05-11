namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Gets the length, in bits, of the shortest two's-complement representation of <paramref name="value"/>. The bit-length also serves as the bit position of a power-of-2 <paramref name="value"/>.</summary>
    /// <remarks>This is using the .NET built-in functionality without modifications to the result.</remarks>
    public static int ShortestBitLength<TSelf>(this TSelf value) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.GetShortestBitLength();

    /// <summary>This version of bit-length is the same as <see cref="ShortestBitLength{TSelf}(TSelf)"/> for positive values, but for negative values it returns <see cref="GetBitCount{TSelf}(TSelf)"/>. This also serves as the bit position of a power-of-2 <paramref name="value"/>.</summary>
    public static int BitLengthN<TSelf>(this TSelf value) where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsNegative(value) ? GetBitCount(value) : value.GetShortestBitLength();

#else

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    public static int ShortestBitLength(this System.Numerics.BigInteger value) => (int)value.GetBitLength();

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    public static int ShortestBitLength(this int value) => unchecked(((uint)value).ShortestBitLength());

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    public static int ShortestBitLength(this long value) => unchecked(((ulong)value).ShortestBitLength());

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    [System.CLSCompliant(false)] public static int ShortestBitLength(this uint value) => 1 + System.Numerics.BitOperations.Log2(value);

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    [System.CLSCompliant(false)] public static int ShortestBitLength(this ulong value) => 1 + System.Numerics.BitOperations.Log2(value);

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns (<paramref name="value"/>.GetByteCount() * 8) instead.</summary>
    public static int BitLengthN(this System.Numerics.BigInteger value) => value < 0 ? value.GetByteCount() * 8 : (int)value.GetBitLength();

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns 32 instead.</summary>
    public static int BitLengthN(this int value) => value < 0 ? 32 : 1 + System.Numerics.BitOperations.Log2(unchecked((uint)value));

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns 64 instead.</summary>
    public static int BitLengthN(this long value) => value < 0 ? 64 : 1 + System.Numerics.BitOperations.Log2(unchecked((ulong)value));

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns 32 instead.</summary>
    [System.CLSCompliant(false)] public static int BitLengthN(this uint value) => 1 + System.Numerics.BitOperations.Log2(value);

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns 64 instead.</summary>
    [System.CLSCompliant(false)] public static int BitLengthN(this ulong value) => 1 + System.Numerics.BitOperations.Log2(value);

#endif
  }
}
