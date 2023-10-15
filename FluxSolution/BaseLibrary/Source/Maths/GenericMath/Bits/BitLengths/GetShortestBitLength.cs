namespace Flux
{
  //  // <seealso cref="http://aggregate.org/MAGIC/"/>
  //  // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>

  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>using the built-in <see cref="System.Numerics.IBinaryInteger{TSelf}.GetShortestBitLength()"/>.</summary>
    public static int GetShortestBitLength<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => value.GetShortestBitLength();

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

#endif
  }
}
