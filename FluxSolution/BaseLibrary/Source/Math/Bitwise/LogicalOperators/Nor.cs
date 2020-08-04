// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Math
  {
    // 1 Nor 1 = 0
    // 1 Nor 0 = 0
    // 0 Nor 1 = 0
    // 0 Nor 0 = 1

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.BigInteger Nor(in System.Numerics.BigInteger a, in System.Numerics.BigInteger b)
      => ~(a | b);

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int Nor(in int a, in int b)
      => ~(a | b);
    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static long Nor(in long a, in long b)
      => ~(a | b);

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static uint Nor(in uint a, in uint b)
      => ~(a | b);
    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong Nor(in ulong a, in ulong b)
      => ~(a | b);
  }
}
