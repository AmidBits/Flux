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
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int Log2(int value)
      => Log2(unchecked((uint)value));

    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it, then uses PopCount().</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int Log2(long value)
      => Log2(unchecked((ulong)value));

    /// <summary>Recursively "folds" the upper bits into the lower bits. The process yields a bit vector with the same most significant 1 as value, but all 1's below it, then uses PopCount().</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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

  public static class ILog2
  {
    public static readonly byte[] ByteTable = new byte[] { 0, 0, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };

    public static int ByByteTable(int value)
      => value switch
      {
        > 0x00FFFFFF => 0x18 + ByteTable[value >> 0x18],
        > 0x0000FFFF => 0x10 + ByteTable[value >> 0x10],
        > 0x000000FF => 0x08 + ByteTable[value >> 0x08],
        > 0x00000000 => 0x00 + ByteTable[value >> 0x00],
        _ => 0,
      };

    /// <summary>Contains the bit positions.</summary>
    public static readonly byte[] DeBruijnTable = { 0, 9, 1, 10, 13, 21, 2, 29, 11, 14, 16, 18, 22, 25, 3, 30, 8, 12, 20, 28, 15, 17, 24, 7, 19, 27, 23, 6, 26, 5, 4, 31 };

    public static int ByDeBruijnTable(int value)
    {
      value |= value >> 1;
      value |= value >> 2;
      value |= value >> 4;
      value |= value >> 8;
      value |= value >> 16;
      return DeBruijnTable[(uint)(value * 0x07C4ACDDU) >> 27];
    }
  }
}
