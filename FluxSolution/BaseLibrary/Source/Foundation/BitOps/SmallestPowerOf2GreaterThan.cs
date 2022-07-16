namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static System.Numerics.BigInteger SmallestPowerOf2GreaterThan(System.Numerics.BigInteger value)
      => FoldRight(value) + 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static int SmallestPowerOf2GreaterThan(int value)
      => FoldRight(value) + 1;
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static long SmallestPowerOf2GreaterThan(long value)
      => FoldRight(value) + 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static uint SmallestPowerOf2GreaterThan(uint value)
      => FoldRight(value) + 1;
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static ulong SmallestPowerOf2GreaterThan(ulong value)
      => FoldRight(value) + 1;
  }
}
