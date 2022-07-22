namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Find the next larger power of 2 that is greater than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always greater than value, othewise it could be greater than or equal to.</param>
    /// <returns>The larger power of 2 that is greater than (or equal to, depending on the proper flag).</returns>
    public static System.Numerics.BigInteger RoundUpToPowerOf2(System.Numerics.BigInteger value, bool proper)
      => IsPowerOf2(value) ? (proper ? value << 1 : value) : FoldRight(value - 1) + 1;

    /// <summary>Find the next larger power of 2 that is greater than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always greater than value, othewise it could be greater than or equal to.</param>
    /// <returns>The larger power of 2 that is greater than (or equal to, depending on the proper flag).</returns>
    public static int RoundUpToPowerOf2(int value, bool proper)
      => IsPowerOf2(value) ? (proper ? value << 1 : value) : FoldRight(value - 1) + 1;

    /// <summary>Find the next larger power of 2 that is greater than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always greater than value, othewise it could be greater than or equal to.</param>
    /// <returns>The larger power of 2 that is greater than (or equal to, depending on the proper flag).</returns>
    public static long RoundUpToPowerOf2(long value, bool proper)
      => IsPowerOf2(value) ? (proper ? value << 1 : value) : FoldRight(value - 1) + 1;

    /// <summary>Find the next larger power of 2 that is greater than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always greater than value, othewise it could be greater than or equal to.</param>
    /// <returns>The larger power of 2 that is greater than (or equal to, depending on the proper flag).</returns>
    [System.CLSCompliant(false)]
    public static uint RoundUpToPowerOf2(uint value, bool proper)
      => IsPowerOf2(value) ? (proper ? value << 1 : value) : FoldRight(value - 1) + 1;

    /// <summary>Find the next larger power of 2 that is greater than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always greater than value, othewise it could be greater than or equal to.</param>
    /// <returns>The larger power of 2 that is greater than (or equal to, depending on the proper flag).</returns>
    [System.CLSCompliant(false)]
    public static ulong RoundUpToPowerOf2(ulong value, bool proper)
      => IsPowerOf2(value) ? (proper ? value << 1 : value) : FoldRight(value - 1) + 1;
  }
}
