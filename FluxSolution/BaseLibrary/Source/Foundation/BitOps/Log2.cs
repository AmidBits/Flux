//namespace Flux
//{
//  // Log2 for int/long, uint/ulong is kept for backwards compatibility, use System.Numerics.BitOperations.Log2 when available.

//  public static partial class BitOps
//  {
//    // https://en.wikipedia.org/wiki/Binary_logarithm
//    // http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious
//    // http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer

//    /// <summary>The log base 2 of an integer is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
//    public static int Log2(System.Numerics.BigInteger value)
//      => value > 255 && value.ToByteArrayEx(out var byteIndex, out var byteValue) is var _
//      ? System.Numerics.BitOperations.Log2(byteValue) + byteIndex * 8
//      : value > 0
//      ? System.Numerics.BitOperations.Log2((uint)value)
//      : 0;

//    /// <summary>The log base 2 of an integer is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
//    public static int Log2(int value)
//      => System.Numerics.BitOperations.Log2(unchecked((uint)value));

//    /// <summary>The log base 2 of an integer is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
//    public static int Log2(long value)
//      => System.Numerics.BitOperations.Log2(unchecked((ulong)value));

//    ///// <summary>The log base 2 of an integer is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
//    //[System.CLSCompliant(false)]
//    //public static int Log2(uint value)
//    //  => PopCount(FoldRight(value) >> 1);
//    ///// <summary>The log base 2 of an integer is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
//    //[System.CLSCompliant(false)]
//    //public static int Log2(ulong value)
//    //  => PopCount(FoldRight(value) >> 1);
//  }
//}
