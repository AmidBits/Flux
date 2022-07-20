namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static System.Numerics.BigInteger LargerPowerOf2(System.Numerics.BigInteger value, bool proper)
      => IsPowerOf2(value) ? (proper ? value << 1 : value) : FoldRight(value) + 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static int LargerPowerOf2(int value, bool proper)
      => IsPowerOf2(value) ? (proper ? value << 1 : value) : FoldRight(value) + 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static long LargerPowerOf2(long value, bool proper)
      => IsPowerOf2(value) ? (proper ? value << 1 : value) : FoldRight(value) + 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static uint LargerPowerOf2(uint value, bool proper)
      => IsPowerOf2(value) ? (proper ? value << 1 : value) : FoldRight(value) + 1;

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static ulong LargerPowerOf2(ulong value, bool proper)
      => IsPowerOf2(value) ? (proper ? value << 1 : value) : FoldRight(value) + 1;
  }
}
