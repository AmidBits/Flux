namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static System.Numerics.BigInteger LargestPowerOf2LessThan(this System.Numerics.BigInteger value)
      => FoldRight(value - 1) + 1 >> 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static int LargestPowerOf2LessThan(this int value)
      => FoldRight(value - 1) + 1 >> 1;
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static long LargestPowerOf2LessThan(this long value)
      => FoldRight(value - 1) + 1 >> 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static uint LargestPowerOf2LessThan(this uint value)
      => FoldRight(value - 1) + 1 >> 1;
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static ulong LargestPowerOf2LessThan(this ulong value)
      => FoldRight(value - 1) + 1 >> 1;
  }
}
