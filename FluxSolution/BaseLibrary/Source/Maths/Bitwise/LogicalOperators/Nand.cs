// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetKernighan"/>

namespace Flux
{
  public static partial class Maths
  {
    // 1 Nand 1 = 0
    // 1 Nand 0 = 1
    // 0 Nand 1 = 1
    // 0 Nand 0 = 1

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.BigInteger Nand(in System.Numerics.BigInteger a, in System.Numerics.BigInteger b)
      => ~(a & b);

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int Nand(in int a, in int b)
      => ~(a & b);
    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static long Nand(in long a, in long b)
      => ~(a & b);

    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static uint Nand(in uint a, in uint b)
      => ~(a & b);
    /// <summary>Performs a logical AND on the two numbers.</summary>
    [System.CLSCompliant(false)]
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static ulong Nand(in ulong a, in ulong b)
      => ~(a & b);
  }
}
