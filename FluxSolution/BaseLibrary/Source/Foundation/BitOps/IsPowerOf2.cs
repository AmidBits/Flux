namespace Flux
{
  public static partial class BitOps
  {
    // <seealso cref="http://aggregate.org/MAGIC/"/>
    // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

    /// <summary>Determines if the number is a power of 2. A non-negative binary integer value x is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</summary>
    /// <remarks>The implementation is extremely fast for huge BigInteger values.</remarks>
    public static bool IsPowerOf2(System.Numerics.BigInteger value)
      => value > 0 && (value & (value - 1)) == 0;

    /// <summary>Determines if the number is a power of 2. A non-negative binary integer value x is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</summary>
    /// <remarks>The implementation is extremely fast.</remarks>
    public static bool IsPowerOf2(int value)
      => value > 0 && (value & (value - 1)) == 0;
    /// <summary>Determines if the number is a power of 2. A non-negative binary integer value x is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</summary>
    /// <remarks>The implementation is extremely fast.</remarks>
    public static bool IsPowerOf2(long value)
      => value > 0 && (value & (value - 1)) == 0;

    /// <summary>Determines if the number is a power of 2. A non-negative binary integer value x is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</summary>
    /// <remarks>The implementation is extremely fast.</remarks>
    [System.CLSCompliant(false)]
    public static bool IsPowerOf2(uint value)
      => value != 0 && (value & (value - 1)) == 0;
    /// <summary>Determines if the number is a power of 2. A non-negative binary integer value x is a power of 2 iff (x&(x-1)) is 0 using 2's complement arithmetic.</summary>
    /// <remarks>The implementation is extremely fast.</remarks>
    [System.CLSCompliant(false)]
    public static bool IsPowerOf2(ulong value)
      => value != 0 && (value & (value - 1)) == 0;
  }
}
