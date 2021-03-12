namespace Flux
{
  public static partial class BitOps
  {
    // https://en.wikipedia.org/wiki/Bit_numbering#Least_significant_bit
    // http://aggregate.org/MAGIC/

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit. Very fast.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.BigInteger LeastSignificant1Bit(System.Numerics.BigInteger value)
      => unchecked(value & -value);

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit. Very fast.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int LeastSignificant1Bit(int value)
      => unchecked(value & -value); // This is one situation where signed integers has less number of operations than unsigned integers.
    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit. Very fast.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static long LeastSignificant1Bit(long value)
      => unchecked(value & -value); // This is one situation where signed integers has less number of operations than unsigned integers.

    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit. Very fast.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    [System.CLSCompliant(false)]
    public static uint LeastSignificant1Bit(uint value)
      => unchecked(value & ((~value) + 1));
    /// <summary>Extracts the lowest numbered element of a bit set. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit. Very fast.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    [System.CLSCompliant(false)]
    public static ulong LeastSignificant1Bit(ulong value)
      => unchecked(value & ((~value) + 1));

    // https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit
    // http://aggregate.org/MAGIC/

    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set) by clearing the least significant 1 bit in each iteration of a loop.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.BigInteger MostSignificant1Bit(System.Numerics.BigInteger value)
      => System.Numerics.BigInteger.One << Log2(value);

    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int MostSignificant1Bit(int value)
      => value >= 0
      ? unchecked((int)MostSignificant1Bit((uint)value))
      : int.MinValue;
    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static long MostSignificant1Bit(long value)
      => value >= 0
      ? unchecked((long)MostSignificant1Bit((ulong)value))
      : long.MinValue;

    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    [System.CLSCompliant(false)]
    public static uint MostSignificant1Bit(uint value)
    {
#if NETCOREAPP
      if (System.Runtime.Intrinsics.X86.Lzcnt.IsSupported)
        return 1U << (LeadingZeroCount(value) ^ 31);
#endif

      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);
      return value & ~(value >> 1);
    }
    /// <summary>Extracts the most significant 1 bit (highest numbered element of a bit set).</summary>
    [System.CLSCompliant(false)]
    public static ulong MostSignificant1Bit(ulong value)
    {
#if NETCOREAPP
      if (System.Runtime.Intrinsics.X86.Lzcnt.X64.IsSupported)
        return 1UL << (LeadingZeroCount(value) ^ 63);
#endif

      value |= (value >> 1);
      value |= (value >> 2);
      value |= (value >> 4);
      value |= (value >> 8);
      value |= (value >> 16);
      value |= (value >> 32);
      return value & ~(value >> 1);
    }
  }
}
