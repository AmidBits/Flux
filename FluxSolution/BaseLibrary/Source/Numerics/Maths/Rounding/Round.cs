namespace Flux
{
  public static partial class Maths
  {
    public static TSelf Round<TSelf>(this TSelf source, RoundingMode mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => mode switch // First, handle the direct rounding strategies.
      {
        RoundingMode.AwayFromZero => RoundAwayFromZero(source),
        RoundingMode.TowardsZero => RoundTowardZero(source),
        RoundingMode.ToPositiveInfinity => RoundToPositiveInfinity(source),
        RoundingMode.ToNegativeInfinity => RoundToNegativeInfinity(source),
        //RoundingMode.ToPowOf2AwayFromZero => TSelf.CreateChecked(RoundToPowOf2AwayFromZero(source)),
        //RoundingMode.ToPowOf2TowardZero => TSelf.CreateChecked(RoundToPowOf2TowardZero(source)),
        _ => mode switch  // Second, handle the halfway rounding strategies.
        {
          RoundingMode.HalfAwayFromZero => RoundHalfAwayFromZero(source),
          RoundingMode.HalfTowardsZero => RoundHalfTowardZero(source),
          RoundingMode.HalfToEven => RoundHalfToEven(source),
          RoundingMode.HalfToNegativeInfinity => RoundHalfToNegativeInfinity(source),
          RoundingMode.HalfToOdd => RoundHalfToOdd(source),
          RoundingMode.HalfToPositiveInfinity => RoundHalfToPositiveInfinity(source),
          _ => throw new System.ArgumentOutOfRangeException(mode.ToString()),
        }
      };

    #region Halfway rounding functions

    /// <summary>Common rounding: round half, bias: odd.</summary>
    /// <remarks><see cref="RoundingMode.HalfToOdd"/></remarks>
    public static TSelf RoundHalfToOdd<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CreateChecked(0.5) is var half && TSelf.Floor(x + half) is var xh && TSelf.IsEvenInteger(xh) && x - TSelf.Floor(x) == half ? xh - TSelf.One : xh;

    /// <summary>Common rounding: round half, bias: even.</summary>
    /// <remarks><see cref="RoundingMode.HalfToEven"/></remarks>
    public static TSelf RoundHalfToEven<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CreateChecked(0.5) is var half && TSelf.Floor(x + half) is var xh && TSelf.IsOddInteger(xh) && x - TSelf.Floor(x) == half ? xh - TSelf.One : xh;

    /// <summary>Symmetric rounding: round half up, bias: away from zero.</summary>
    /// <remarks><see cref="RoundingMode.HalfAwayFromZero"/></remarks>
    public static TSelf RoundHalfAwayFromZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CopySign(RoundHalfToPositiveInfinity(TSelf.Abs(x)), x);

    /// <summary>Symmetric rounding: round half down, bias: towards zero.</summary>
    /// <remarks><see cref="RoundingMode.HalfTowardsZero"/></remarks>
    public static TSelf RoundHalfTowardZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CopySign(RoundHalfToNegativeInfinity(TSelf.Abs(x)), x);

    /// <summary>Common rounding: round half down, bias: negative infinity.</summary>
    /// <remarks><see cref="RoundingMode.HalfToNegativeInfinity"/></remarks>
    public static TSelf RoundHalfToNegativeInfinity<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(x - TSelf.CreateChecked(0.5));

    /// <summary>Common rounding: round half up, bias: positive infinity.</summary>
    /// <remarks><see cref="RoundingMode.HalfToPositiveInfinity"/></remarks>
    public static TSelf RoundHalfToPositiveInfinity<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(x + TSelf.CreateChecked(0.5));

    #endregion Halfway rounding functions

    #region Unconditional rounding functions

    /// <summary>Symmetric rounding: round up, bias: away from zero.</summary>
    /// <remarks>Equivalent to the opposite effect of the Truncate() function (also <see cref="RoundTowardZero{TSelf}(TSelf)"/>).</remarks>
    public static TSelf RoundAwayFromZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.IsNegative(x) ? TSelf.Floor(x) : TSelf.Ceiling(x);

    /// <summary>Symmetric rounding: round down, bias: towards zero.</summary>
    /// <remarks>Equivalent to the Truncate() function.</remarks>
    public static TSelf RoundTowardZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Truncate(x);

    /// <summary>Common rounding: round down, bias: negative infinity.</summary>
    /// <remarks>Equivalent to the Floor() function.</remarks>
    public static TSelf RoundToNegativeInfinity<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(x);

    /// <summary>Common rounding: round up, bias: positive infinity.</summary>
    /// <remarks>Equivalent to the ceil()/Ceiling() function.</remarks>
    public static TSelf RoundToPositiveInfinity<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(x);

    public static TSelf RoundToMultipleOfAwayFromZero<TSelf>(this TSelf value, TSelf multiple, bool unequal)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CopySign(multiple, value) is var msv && value - (value % multiple) is var motz && (motz != value || unequal) ? motz + msv : motz;

    public static TSelf RoundToMultipleOfTowardZero<TSelf>(this TSelf value, TSelf multiple, bool unequal)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => value - (value % multiple) is var motz && unequal && motz == value ? motz - TSelf.CopySign(multiple, value) : motz;

    public static TSelf RoundToPow2AwayFromZero<TSelf>(this TSelf x, bool unequal)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CreateChecked(System.Numerics.BigInteger.CreateChecked(x).Pow2AwayFromZero(unequal || !TSelf.IsInteger(x)));

    public static TSelf RoundToPow2TowardZero<TSelf>(this TSelf x, bool unequal)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CreateChecked(System.Numerics.BigInteger.CreateChecked(x).Pow2TowardZero(unequal && TSelf.IsInteger(x)));

    public static TSelf RoundToPowOfAwayFromZero<TSelf, TRadix>(this TSelf x, TRadix radix, bool unequal)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TSelf.CreateChecked(Quantities.Radix.PowOfAwayFromZero(System.Numerics.BigInteger.CreateChecked(x), System.Numerics.BigInteger.CreateChecked(radix), unequal || !TSelf.IsInteger(x)));

    public static TSelf RoundToPowOfTowardZero<TSelf, TRadix>(this TSelf x, TRadix radix, bool unequal)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TSelf.CreateChecked(Quantities.Radix.PowOfTowardZero(System.Numerics.BigInteger.CreateChecked(x), System.Numerics.BigInteger.CreateChecked(radix), unequal && TSelf.IsInteger(x)));

    #endregion // Unconditional rounding functions
  }
}
