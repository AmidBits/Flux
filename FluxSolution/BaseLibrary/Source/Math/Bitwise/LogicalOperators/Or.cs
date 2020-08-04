// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Math
  {
    // 1 Or 1 = 1
    // 1 Or 0 = 1
    // 0 Or 1 = 1
    // 0 Or 0 = 0

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.BigInteger Or(in System.Numerics.BigInteger a, in System.Numerics.BigInteger b)
      => a | b;

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int Or(in int a, in int b)
      => a | b;
    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static long Or(in long a, in long b)
      => a | b;

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static uint Or(in uint a, in uint b)
      => a | b;
    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong Or(in ulong a, in ulong b)
      => a | b;
  }
}
