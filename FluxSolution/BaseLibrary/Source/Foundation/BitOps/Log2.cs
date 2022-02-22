namespace Flux
{
  public static partial class BitOps
  {
    // https://en.wikipedia.org/wiki/Binary_logarithm
    // http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogObvious
    // http://aggregate.org/MAGIC/#Log2%20of%20an%20Integer

    /// <summary>The log base 2 of an integer is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
    public static int Log2(System.Numerics.BigInteger value)
    {
      if (value > 255)
      {
        value.ToByteArrayEx(out var byteIndex, out var byteValue);

        return ILog2.ByteTable[byteValue] + byteIndex * 8;
      }
      else if (value > 0) return ILog2.ByteTable[(int)value];

      return 0;
    }

    /// <summary>The log base 2 of an integer is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
    public static int Log2(int value)
      => System.Numerics.BitOperations.Log2(unchecked((uint)value));

    /// <summary>The log base 2 of an integer is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
    public static int Log2(long value)
      => System.Numerics.BitOperations.Log2(unchecked((ulong)value));

    ///// <summary>The log base 2 of an integer is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
    //[System.CLSCompliant(false)]
    //public static int Log2(uint value)
    //  => System.Numerics.BitOperations.Log2(value); // PopCount(FoldRight(value) >> 1);
    ///// <summary>The log base 2 of an integer is the same as the position of the highest bit set (or most significant bit set, MSB).</summary>
    //[System.CLSCompliant(false)]
    //public static int Log2(ulong value)
    //  => System.Numerics.BitOperations.Log2(value); // PopCount(FoldRight(value) >> 1);
  }
}
