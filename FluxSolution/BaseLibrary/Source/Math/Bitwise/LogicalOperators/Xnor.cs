// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Math
  {
    // 1 Xnor 1 = 1
    // 1 Xnor 0 = 0
    // 0 Xnor 1 = 0
    // 0 Xnor 0 = 1

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.BigInteger Xnor(in System.Numerics.BigInteger a, in System.Numerics.BigInteger b)
      => ~(a ^ b);

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int Xnor(in int a, in int b)
      => ~(a ^ b);
    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static long Xnor(in long a, in long b)
      => ~(a ^ b);

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static uint Xnor(in uint a, in uint b)
      => ~(a ^ b);
    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong Xnor(in ulong a, in ulong b)
      => ~(a ^ b);
  }
}
