namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static System.Numerics.BigInteger Pow2LessThan(System.Numerics.BigInteger value)
      => FoldRight(value - 1) + 1 >> 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static int Pow2LessThan(int value)
      => FoldRight(value - 1) + 1 >> 1;
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static long Pow2LessThan(long value)
      => FoldRight(value - 1) + 1 >> 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static uint Pow2LessThan(uint value)
      => FoldRight(value - 1) + 1 >> 1;
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static ulong Pow2LessThan(ulong value)
      => FoldRight(value - 1) + 1 >> 1;
  }
}
