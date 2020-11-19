namespace Flux
{
  public static partial class BitOps
  {
    // <seealso cref="http://aggregate.org/MAGIC/"/>
    // <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

    /// <summary>Computes the next power of 2 greater than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the result will always be greater than value. If false, it could be greater than or equal to value.</param>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.BigInteger NextPowerOf2(System.Numerics.BigInteger value)
      => PowerOf2(value) << 1;

    /// <summary>Computes the next power of 2 greater than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the result will always be greater than value. If false, it could be greater than or equal to value.</param>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int NextPowerOf2(int value)
      => PowerOf2(value) << 1;
    /// <summary>Computes the next power of 2 greater than or optionally equal to the specified number.</summary>
    /// <param name="strictlyLessThan">If true, the result will always be greater than value. If false, it could be greater than or equal to value.</param>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static long NextPowerOf2(long value)
      => PowerOf2(value) << 1;
  }
}
