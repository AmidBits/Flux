// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Maths
  {
    // 1 And 1 = 1
    // 1 And 0 = 0
    // 0 And 1 = 0
    // 0 And 0 = 0

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.BigInteger And(in System.Numerics.BigInteger a, in System.Numerics.BigInteger b)
      => a & b;

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int And(in int a, in int b)
      => a & b;
    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static long And(in long a, in long b)
      => a & b;

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static uint And(in uint a, in uint b)
      => a & b;
    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong And(in ulong a, in ulong b)
      => a & b;
  }
}
