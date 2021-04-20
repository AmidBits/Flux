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

    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it, then uses PopCount().</summary>
    public static int Log2(int value)
      => Log2(unchecked((uint)value));

    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it, then uses PopCount().</summary>
    public static int Log2(long value)
      => Log2(unchecked((ulong)value));

    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it, then uses PopCount().</summary>
    [System.CLSCompliant(false)]
    public static int Log2(uint value)
    {
      if (value == 0) // Ensure undefined returns zero.
        return 0;

#if NETCOREAPP
      if (System.Runtime.Intrinsics.X86.Lzcnt.IsSupported)
        return 31 - (int)System.Runtime.Intrinsics.X86.Lzcnt.LeadingZeroCount(value);
#endif

      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);
      return PopCount(value >> 1);
    }
    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it, then uses PopCount().</summary>
    [System.CLSCompliant(false)]
    public static int Log2(ulong value)
    {
      if (value == 0) // Ensure undefined returns zero.
        return 0;

#if NETCOREAPP
      if (System.Runtime.Intrinsics.X86.Lzcnt.X64.IsSupported)
        return 63 - (int)System.Runtime.Intrinsics.X86.Lzcnt.X64.LeadingZeroCount(value);
      if (System.Runtime.Intrinsics.X86.Lzcnt.IsSupported)
        return value <= uint.MaxValue ? Log2((uint)value) : 32 + Log2((uint)(value >> 32));
#endif

      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);
      value |= (value >> 32);
      return PopCount(value >> 1);
    }
  }
}
