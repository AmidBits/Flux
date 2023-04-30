namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    public static TSelf Round<TSelf>(this TSelf source, RoundingMode mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => mode switch // First, handle the direct rounding strategies.
      {
        RoundingMode.AllAwayFromZero => RoundAllAwayFromZero(source),
        RoundingMode.AllToPositiveInfinity => RoundAllToPositiveInfinity(source),
        RoundingMode.AllToNegativeInfinity => RoundAllToNegativeInfinity(source),
        RoundingMode.AllTowardZero => RoundAllTowardZero(source),
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
    /// <remarks>Equivalent to the opposite effect of the Truncate() function (also <see cref="RoundAllTowardZero{TSelf}(TSelf)"/>).</remarks>
    public static TSelf RoundAllAwayFromZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.IsNegative(x) ? TSelf.Floor(x) : TSelf.Ceiling(x);

    /// <summary>Symmetric rounding: round down, bias: towards zero.</summary>
    /// <remarks>Equivalent to the Truncate() function.</remarks>
    public static TSelf RoundAllTowardZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Truncate(x);

    /// <summary>Common rounding: round down, bias: negative infinity.</summary>
    /// <remarks>Equivalent to the Floor() function.</remarks>
    public static TSelf RoundAllToNegativeInfinity<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(x);

    /// <summary>Common rounding: round up, bias: positive infinity.</summary>
    /// <remarks>Equivalent to the ceil()/Ceiling() function.</remarks>
    public static TSelf RoundAllToPositiveInfinity<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(x);

    #endregion // Unconditional rounding functions

#else

    public static double Round(this double x, RoundingMode mode)
    => mode switch // First, handle the direct rounding strategies.
    {
      RoundingMode.AllAwayFromZero => RoundAllAwayFromZero(x),
      RoundingMode.AllToPositiveInfinity => RoundAllToPositiveInfinity(x),
      RoundingMode.AllToNegativeInfinity => RoundAllToNegativeInfinity(x),
      RoundingMode.AllTowardZero => RoundAllTowardZero(x),
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
    public static double RoundAllAwayFromZero(this double x) => x < 0 ? System.Math.Floor(x) : System.Math.Ceiling(x);

    /// <summary>Symmetric rounding: round down, bias: towards zero.</summary>
    public static double RoundAllTowardZero(this double x) => System.Math.Truncate(x);

    /// <summary>Common rounding: round down, bias: negative infinity.</summary>
    public static double RoundAllToNegativeInfinity(this double x) => System.Math.Floor(x);

    /// <summary>Common rounding: round up, bias: positive infinity.</summary>
    public static double RoundAllToPositiveInfinity(this double x) => System.Math.Ceiling(x);

    #endregion Direct (non-halfway) rounding functions

#endif
  }
}
