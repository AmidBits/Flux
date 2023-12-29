namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    public static TSelf Round<TSelf>(this TSelf source, RoundingMode mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => mode switch // First, handle the direct rounding strategies.
      {
        RoundingMode.AwayFromZero => RoundAwayFromZero(source),
        RoundingMode.TowardZero => RoundTowardZero(source),
        RoundingMode.ToPositiveInfinity => RoundToPositiveInfinity(source),
        RoundingMode.ToNegativeInfinity => RoundToNegativeInfinity(source),
        //RoundingMode.ToPowOf2AwayFromZero => TSelf.CreateChecked(RoundToPowOf2AwayFromZero(source)),
        //RoundingMode.ToPowOf2TowardZero => TSelf.CreateChecked(RoundToPowOf2TowardZero(source)),
        _ => mode switch  // Second, handle the halfway rounding strategies.
        {
          RoundingMode.HalfAwayFromZero => RoundHalfAwayFromZero(source),
          RoundingMode.HalfTowardZero => RoundHalfTowardZero(source),
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
    /// <remarks><see cref="RoundingMode.HalfTowardZero"/></remarks>
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

    #endregion // Unconditional rounding functions

#else

    public static double Round(this double x, RoundingMode mode)
    => mode switch // First, handle the direct rounding strategies.
    {
      RoundingMode.AwayFromZero => RoundAwayFromZero(x),
      RoundingMode.TowardZero => RoundTowardZero(x),
      RoundingMode.ToPositiveInfinity => RoundToPositiveInfinity(x),
      RoundingMode.ToNegativeInfinity => RoundToNegativeInfinity(x),
      //RoundingMode.ToPowOf2AwayFromZero => (double)(RoundToPowOf2AwayFromZero(source)),
      //RoundingMode.ToPowOf2TowardZero => (double)(RoundToPowOf2TowardZero(source)),
      _ => mode switch  // Second, handle the halfway rounding strategies.
      {
        RoundingMode.HalfAwayFromZero => RoundHalfAwayFromZero(x),
        RoundingMode.HalfTowardZero => RoundHalfTowardZero(x),
        RoundingMode.HalfToEven => RoundHalfToEven(x),
        RoundingMode.HalfToNegativeInfinity => RoundHalfToNegativeInfinity(x),
        RoundingMode.HalfToOdd => RoundHalfToOdd(x),
        RoundingMode.HalfToPositiveInfinity => RoundHalfToPositiveInfinity(x),
        _ => throw new System.ArgumentOutOfRangeException(mode.ToString()),
      }
    };

    #region Halfway rounding functions

    /// <summary>Common rounding: round half, bias: even.</summary>
    public static double RoundHalfToOdd(this double x) => 0.5 is var half && System.Math.Floor(x + half) is var xh && (System.Convert.ToInt32(xh) & 1) == 0 && x - System.Math.Floor(x) == half ? xh - 1 : xh;

    /// <summary>Common rounding: round half, bias: even.</summary>
    public static double RoundHalfToEven(this double x) => 0.5 is var half && System.Math.Floor(x + half) is var xh && (System.Convert.ToInt32(xh) & 1) == 1 && x - System.Math.Floor(x) == half ? xh - 1 : xh;

    /// <summary>Symmetric rounding: round half up, bias: away from zero.</summary>
    public static double RoundHalfAwayFromZero(this double x) => System.Math.CopySign(RoundHalfToPositiveInfinity(System.Math.Abs(x)), x);

    /// <summary>Symmetric rounding: round half down, bias: towards zero.</summary>
    public static double RoundHalfTowardZero(this double x) => System.Math.CopySign(RoundHalfToNegativeInfinity(System.Math.Abs(x)), x);

    /// <summary>Common rounding: round half down, bias: negative infinity.</summary>
    public static double RoundHalfToNegativeInfinity(this double x) => System.Math.Ceiling(x - 0.5);

    /// <summary>Common rounding: round half up, bias: positive infinity.</summary>
    public static double RoundHalfToPositiveInfinity(this double x) => System.Math.Floor(x + 0.5);

    #endregion Halfway rounding functions

    #region Direct (non-halfway) rounding functions

    /// <summary>Symmetric rounding: round up, bias: away from zero.</summary>
    public static double RoundAwayFromZero(this double x) => x < 0 ? System.Math.Floor(x) : System.Math.Ceiling(x);

    /// <summary>Symmetric rounding: round down, bias: towards zero.</summary>
    public static double RoundTowardZero(this double x) => System.Math.Truncate(x);

    /// <summary>Common rounding: round down, bias: negative infinity.</summary>
    public static double RoundToNegativeInfinity(this double x) => System.Math.Floor(x);

    /// <summary>Common rounding: round up, bias: positive infinity.</summary>
    public static double RoundToPositiveInfinity(this double x) => System.Math.Ceiling(x);

    /// <summary>Get the power-of-2 nearest to value, away from zero (AFZ).</summary>
    /// <param name="value">The value for which the nearest power-of-2 away from zero will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the power-of-2 will always be away from zero and never equal to value.</param>
    public static System.Numerics.BigInteger RoundToPowOf2AwayFromZero(this double value)
      => RoundToPowOf2TowardZero(value) is var ms1b && (double)(ms1b) < value ? (ms1b == 0 ? 1 : ms1b << 1) : ms1b;

    /// <summary>Get the power-of-2 nearest to value, toward zero (TZ).</summary>
    /// <param name="value">The value for which the nearest power-of-2 towards zero will be found.</param>
    /// <param name="proper">If true, ensure the power-of-2 are not equal to value, i.e. the power-of-2 will always be toward zero and never equal to value.</param>
    public static System.Numerics.BigInteger RoundToPowOf2TowardZero(this double value)
      => new System.Numerics.BigInteger((value < 0 ? throw new System.ArgumentOutOfRangeException(nameof(value)) : value).TruncMod(1, out var _)).MostSignificant1Bit();

    #endregion Direct (non-halfway) rounding functions

#endif
  }
}
