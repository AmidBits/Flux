namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static System.Numerics.BigInteger SmallerPowerOf2(this System.Numerics.BigInteger value, bool proper)
      => IsPowerOf2(value) ? (proper ? value >> 1 : value) : FoldRight(value - 1) + 1 >> 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static int SmallerPowerOf2(this int value, bool proper)
      => IsPowerOf2(value) ? (proper ? value >> 1 : value) : FoldRight(value - 1) + 1 >> 1;
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static long SmallerPowerOf2(this long value, bool proper)
      => IsPowerOf2(value) ? (proper ? value >> 1 : value) : FoldRight(value - 1) + 1 >> 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static uint SmallerPowerOf2(this uint value, bool proper)
      => IsPowerOf2(value) ? (proper ? value >> 1 : value) : FoldRight(value - 1) + 1 >> 1;
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static ulong SmallerPowerOf2(this ulong value, bool proper)
      => IsPowerOf2(value) ? (proper ? value >> 1 : value) : FoldRight(value - 1) + 1 >> 1;
  }
}
