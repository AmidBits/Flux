namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static System.Numerics.BigInteger RoundToNearestPowerOf2(this System.Numerics.BigInteger value)
      => FoldRight(value - 1) + 1 is var gte && gte >> 1 is var lte && (gte - value) > (value - lte) ? lte : gte;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static int RoundToNearestPowerOf2(this int value)
      => FoldRight(value - 1) + 1 is var gte && gte >> 1 is var lte && (gte - value) > (value - lte) ? lte : gte;
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static long RoundToNearestPowerOf2(this long value)
      => FoldRight(value - 1) + 1 is var gte && gte >> 1 is var lte && (gte - value) > (value - lte) ? lte : gte;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static uint RoundToNearestPowerOf2(this uint value)
      => FoldRight(value - 1) + 1 is var gte && gte >> 1 is var lte && (gte - value) > (value - lte) ? lte : gte;
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static ulong RoundToNearestPowerOf2(this ulong value)
      => FoldRight(value - 1) + 1 is var gte && gte >> 1 is var lte && (gte - value) > (value - lte) ? lte : gte;
  }
}
