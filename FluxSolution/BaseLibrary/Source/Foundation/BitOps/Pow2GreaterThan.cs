namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static System.Numerics.BigInteger Pow2GreaterThan(System.Numerics.BigInteger value)
      => FoldRight(value) + 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static int Pow2GreaterThan(int value)
      => FoldRight(value) + 1;
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static long Pow2GreaterThan(long value)
      => FoldRight(value) + 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static uint Pow2GreaterThan(uint value)
      => FoldRight(value) + 1;
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static ulong Pow2GreaterThan(ulong value)
      => FoldRight(value) + 1;
  }
}
