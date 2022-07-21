namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Find the next smaller power of 2 that is less than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always less than value, othewise it could be less than or equal to.</param>
    /// <returns>The smaller power of 2 that is less than (or equal to, depending on the proper flag).</returns>
    public static System.Numerics.BigInteger SmallerPowerOf2(this System.Numerics.BigInteger value, bool proper)
      => IsPowerOf2(value) ? (proper ? value >> 1 : value) : FoldRight(value - 1) + 1 >> 1;

    /// <summary>Find the next smaller power of 2 that is less than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always less than value, othewise it could be less than or equal to.</param>
    /// <returns>The smaller power of 2 that is less than (or equal to, depending on the proper flag).</returns>
    public static int SmallerPowerOf2(this int value, bool proper)
      => IsPowerOf2(value) ? (proper ? value >> 1 : value) : FoldRight(value - 1) + 1 >> 1;

    /// <summary>Find the next smaller power of 2 that is less than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always less than value, othewise it could be less than or equal to.</param>
    /// <returns>The smaller power of 2 that is less than (or equal to, depending on the proper flag).</returns>
    public static long SmallerPowerOf2(this long value, bool proper)
      => IsPowerOf2(value) ? (proper ? value >> 1 : value) : FoldRight(value - 1) + 1 >> 1;

    /// <summary>Find the next smaller power of 2 that is less than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always less than value, othewise it could be less than or equal to.</param>
    /// <returns>The smaller power of 2 that is less than (or equal to, depending on the proper flag).</returns>
    [System.CLSCompliant(false)]
    public static uint SmallerPowerOf2(this uint value, bool proper)
      => IsPowerOf2(value) ? (proper ? value >> 1 : value) : FoldRight(value - 1) + 1 >> 1;

    /// <summary>Find the next smaller power of 2 that is less than (or equal to, depending on the proper flag).</summary>
    /// <param name="value">The reference value.</param>
    /// <param name="proper">If true, then the result is always less than value, othewise it could be less than or equal to.</param>
    /// <returns>The smaller power of 2 that is less than (or equal to, depending on the proper flag).</returns>
    [System.CLSCompliant(false)]
    public static ulong SmallerPowerOf2(this ulong value, bool proper)
      => IsPowerOf2(value) ? (proper ? value >> 1 : value) : FoldRight(value - 1) + 1 >> 1;
  }
}
