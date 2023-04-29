namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    public static TSelf Round<TSelf>(this TSelf source, RoundingMode mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => mode switch // First, handle the direct rounding strategies.
      {
        RoundingMode.Envelop => RoundingEnvelop(source),
        RoundingMode.Ceiling => RoundingCeiling(source),
        RoundingMode.Floor => RoundingFloor(source),
        RoundingMode.Truncate => RoundingTruncate(source),
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

    /// <summary>Symmetric rounding: round half up, bias: away from zero.</summary>
    public static TSelf RoundHalfAwayFromZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CopySign(RoundHalfToPositiveInfinity(TSelf.Abs(x)), x);

    /// <summary>Common rounding: round half, bias: even.</summary>
    public static TSelf RoundHalfToEven<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CreateChecked(0.5) is var half && TSelf.Floor(x + half) is var xh && TSelf.IsOddInteger(xh) && x - TSelf.Floor(x) == half ? xh - TSelf.One : xh;

    /// <summary>Common rounding: round half down, bias: negative infinity.</summary>
    public static TSelf RoundHalfToNegativeInfinity<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(x - TSelf.CreateChecked(0.5));

    /// <summary>Common rounding: round half, bias: even.</summary>
    public static TSelf RoundHalfToOdd<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CreateChecked(0.5) is var half && TSelf.Floor(x + half) is var xh && TSelf.IsEvenInteger(xh) && x - TSelf.Floor(x) == half ? xh - TSelf.One : xh;

    /// <summary>Common rounding: round half up, bias: positive infinity.</summary>
    public static TSelf RoundHalfToPositiveInfinity<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(x + TSelf.CreateChecked(0.5));

    /// <summary>Symmetric rounding: round half down, bias: towards zero.</summary>
    public static TSelf RoundHalfTowardZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CopySign(RoundHalfToNegativeInfinity(TSelf.Abs(x)), x);

    #endregion Halfway rounding functions

    #region Direct (non-halfway) rounding functions

    /// <summary>Common rounding: round up, bias: positive infinity.</summary>
    public static TSelf RoundingCeiling<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(x);

    /// <summary>Symmetric rounding: round up, bias: away from zero.</summary>
    /// <remarks>This has the opposite effect of <see cref="RoundingTruncate{TSelf}(TSelf)"/>.</remarks>
    public static TSelf RoundingEnvelop<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.IsNegative(x) ? TSelf.Floor(x) : TSelf.Ceiling(x);

    /// <summary>Common rounding: round down, bias: negative infinity.</summary>
    public static TSelf RoundingFloor<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(x);

    /// <summary>Symmetric rounding: round down, bias: towards zero.</summary>
    public static TSelf RoundingTruncate<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Truncate(x);

    #endregion Direct (non-halfway) rounding functions

#else

    public static double Round(this double x, RoundingMode mode)
    => mode switch // First, handle the direct rounding strategies.
    {
      RoundingMode.Envelop => RoundingEnvelop(x),
      RoundingMode.Ceiling => RoundingCeiling(x),
      RoundingMode.Floor => RoundingFloor(x),
      RoundingMode.Truncate => RoundingTruncate(x),
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

    /// <summary>Symmetric rounding: round half up, bias: away from zero.</summary>
    public static double RoundHalfAwayFromZero(this double x) => System.Math.CopySign(RoundHalfToPositiveInfinity(System.Math.Abs(x)), x);

    /// <summary>Common rounding: round half, bias: even.</summary>
    public static double RoundHalfToEven(this double x) => 0.5 is var half && System.Math.Floor(x + half) is var xh && (System.Convert.ToInt32(xh) & 1) == 1 && x - System.Math.Floor(x) == half ? xh - 1 : xh;

    /// <summary>Common rounding: round half down, bias: negative infinity.</summary>
    public static double RoundHalfToNegativeInfinity(this double x) => System.Math.Ceiling(x - 0.5);

    /// <summary>Common rounding: round half, bias: even.</summary>
    public static double RoundHalfToOdd(this double x) => 0.5 is var half && System.Math.Floor(x + half) is var xh && (System.Convert.ToInt32(xh) & 1) == 0 && x - System.Math.Floor(x) == half ? xh - 1 : xh;

    /// <summary>Common rounding: round half up, bias: positive infinity.</summary>
    public static double RoundHalfToPositiveInfinity(this double x) => System.Math.Floor(x + 0.5);

    /// <summary>Symmetric rounding: round half down, bias: towards zero.</summary>
    public static double RoundHalfTowardZero(this double x) => System.Math.CopySign(RoundHalfToNegativeInfinity(System.Math.Abs(x)), x);

    #endregion Halfway rounding functions

    #region Direct (non-halfway) rounding functions

    /// <summary>Common rounding: round up, bias: positive infinity.</summary>
    public static double RoundingCeiling(this double x) => System.Math.Ceiling(x);

    /// <summary>Symmetric rounding: round up, bias: away from zero.</summary>
    public static double RoundingEnvelop(this double x) => x < 0 ? System.Math.Floor(x) : System.Math.Ceiling(x);

    /// <summary>Common rounding: round down, bias: negative infinity.</summary>
    public static double RoundingFloor(this double x) => System.Math.Floor(x);

    /// <summary>Symmetric rounding: round down, bias: towards zero.</summary>
    public static double RoundingTruncate(this double x) => System.Math.Truncate(x);

    #endregion Direct (non-halfway) rounding functions

#endif
  }
}
