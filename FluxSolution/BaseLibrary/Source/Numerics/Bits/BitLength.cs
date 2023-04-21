namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Gets the length, in bits, of the shortest two's-complement representation of the current value.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    public static int BitLength<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.GetShortestBitLength();

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where it returns (<paramref name="value"/>.GetByteCount() * 8) instead.</summary>
    public static int BitLengthN<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => TSelf.IsNegative(value) ? (value.GetByteCount() * 8) : value.GetShortestBitLength();

#else

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    public static int BitLength(this System.Numerics.BigInteger value) => (int)value.GetBitLength();

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    public static int BitLength(this int value) => value < 0 ? BitLength(System.Math.Abs(value)) : 1 + System.Numerics.BitOperations.Log2(unchecked((uint)value));

    /// <summary>Returns the number of bits in the minimal two's-complement representation of the number.</summary>
    /// <remarks>This is using the .NET built-in functionality, unmodified.</remarks>
    public static int BitLength(this long value) => value < 0 ? BitLength(System.Math.Abs(value)) : 1 + System.Numerics.BitOperations.Log2(unchecked((ulong)value));

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns (<paramref name="value"/>.GetByteCount() * 8) instead.</summary>
    public static int BitLengthN(this System.Numerics.BigInteger value) => value < 0 ? value.GetByteCount() * 8 : (int)value.GetBitLength();

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns 32 instead.</summary>
    public static int BitLengthN(this int value) => value < 0 ? 32 : 1 + System.Numerics.BitOperations.Log2(unchecked((uint)value));

    /// <summary>This version of bit-length is the same as <see cref="BitLength{TSelf}(TSelf)"/> except for negative values, where this returns 64 instead.</summary>
    public static int BitLengthN(this long value) => value < 0 ? 64 : 1 + System.Numerics.BitOperations.Log2(unchecked((ulong)value));

#endif
  }
}
