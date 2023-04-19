namespace Flux
{
#if NET7_0_OR_GREATER

  public static partial class NumericsExtensionMethods
  {
    public static TSelf Round<TSelf>(this TSelf source, RoundingMode mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => Rounding<TSelf>.Round(source, mode);
  }

  /// <summary></summary>
  public class Rounding<TSelf>
    : INumberRoundable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    #region Static methods

    #region Halfway rounding functions

    /// <summary>Symmetric rounding: round half up, bias: away from zero.</summary>
    public static TSelf RoundHalfAwayFromZero(TSelf x) => TSelf.CopySign(RoundHalfToPositiveInfinity(TSelf.Abs(x)), x);

    /// <summary>Common rounding: round half, bias: even.</summary>
    public static TSelf RoundHalfToEven(TSelf x) => TSelf.CreateChecked(0.5) is var half && TSelf.Floor(x + half) is var xh && TSelf.IsOddInteger(xh) && x - TSelf.Floor(x) == half ? xh - TSelf.One : xh;

    /// <summary>Common rounding: round half down, bias: negative infinity.</summary>
    public static TSelf RoundHalfToNegativeInfinity(TSelf x) => TSelf.Ceiling(x - TSelf.CreateChecked(0.5));

    /// <summary>Common rounding: round half, bias: even.</summary>
    public static TSelf RoundHalfToOdd(TSelf x) => TSelf.CreateChecked(0.5) is var half && TSelf.Floor(x + half) is var xh && TSelf.IsEvenInteger(xh) && x - TSelf.Floor(x) == half ? xh - TSelf.One : xh;

    /// <summary>Common rounding: round half up, bias: positive infinity.</summary>
    public static TSelf RoundHalfToPositiveInfinity(TSelf x) => TSelf.Floor(x + TSelf.CreateChecked(0.5));

    /// <summary>Symmetric rounding: round half down, bias: towards zero.</summary>
    public static TSelf RoundHalfTowardZero(TSelf x) => TSelf.CopySign(RoundHalfToNegativeInfinity(TSelf.Abs(x)), x);

    #endregion Halfway rounding functions

    #region Direct (non-halfway) rounding functions

    /// <summary>Common rounding: round up, bias: positive infinity.</summary>
    public static TSelf RoundCeiling(TSelf x) => TSelf.Ceiling(x);

    /// <summary>Symmetric rounding: round up, bias: away from zero.</summary>
    public static TSelf RoundEnvelop(TSelf x) => TSelf.IsNegative(x) ? TSelf.Floor(x) : TSelf.Ceiling(x);

    /// <summary>Common rounding: round down, bias: negative infinity.</summary>
    public static TSelf RoundFloor(TSelf x) => TSelf.Floor(x);

    /// <summary>Symmetric rounding: round down, bias: towards zero.</summary>
    public static TSelf RoundTruncate(TSelf x) => TSelf.Truncate(x);

    #endregion Direct (non-halfway) rounding functions

    public static TSelf Round(TSelf x, RoundingMode mode)
      => mode switch // First, handle the direct rounding strategies.
      {
        RoundingMode.Envelop => RoundEnvelop(x),
        RoundingMode.Ceiling => RoundCeiling(x),
        RoundingMode.Floor => RoundFloor(x),
        RoundingMode.Truncate => RoundTruncate(x),
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

    #endregion Static methods

    #region Implemented interfaces
    public TSelf RoundNumber(TSelf x, RoundingMode mode) => Round(x, mode);
    #endregion Implemented interfaces
  }

#else

  public static partial class NumericsExtensionMethods
  {
    public static double Round(this double source, RoundingMode mode)
      => Rounding.Round(source, mode);
  }

  /// <summary></summary>
  public class Rounding
    : INumberRoundable
  {
    #region Static methods

    #region Halfway rounding functions

    /// <summary>Symmetric rounding: round half up, bias: away from zero.</summary>
    public static double RoundHalfAwayFromZero(double x) => System.Math.CopySign(RoundHalfToPositiveInfinity(System.Math.Abs(x)), x);

    /// <summary>Common rounding: round half, bias: even.</summary>
    public static double RoundHalfToEven(double x) => 0.5 is var half && System.Math.Floor(x + half) is var xh && (System.Convert.ToInt32(xh) & 1) == 1 && x - System.Math.Floor(x) == half ? xh - 1 : xh;

    /// <summary>Common rounding: round half down, bias: negative infinity.</summary>
    public static double RoundHalfToNegativeInfinity(double x) => System.Math.Ceiling(x - 0.5);

    /// <summary>Common rounding: round half, bias: even.</summary>
    public static double RoundHalfToOdd(double x) => 0.5 is var half && System.Math.Floor(x + half) is var xh && (System.Convert.ToInt32(xh) & 1) == 0 && x - System.Math.Floor(x) == half ? xh - 1 : xh;

    /// <summary>Common rounding: round half up, bias: positive infinity.</summary>
    public static double RoundHalfToPositiveInfinity(double x) => System.Math.Floor(x + 0.5);

    /// <summary>Symmetric rounding: round half down, bias: towards zero.</summary>
    public static double RoundHalfTowardZero(double x) => System.Math.CopySign(RoundHalfToNegativeInfinity(System.Math.Abs(x)), x);

    #endregion Halfway rounding functions

    #region Direct (non-halfway) rounding functions

    /// <summary>Common rounding: round up, bias: positive infinity.</summary>
    public static double RoundCeiling(double x) => System.Math.Ceiling(x);

    /// <summary>Symmetric rounding: round up, bias: away from zero.</summary>
    public static double RoundEnvelop(double x) => x < 0 ? System.Math.Floor(x) : System.Math.Ceiling(x);

    /// <summary>Common rounding: round down, bias: negative infinity.</summary>
    public static double RoundFloor(double x) => System.Math.Floor(x);

    /// <summary>Symmetric rounding: round down, bias: towards zero.</summary>
    public static double RoundTruncate(double x) => System.Math.Truncate(x);

    #endregion Direct (non-halfway) rounding functions

    public static double Round(double x, RoundingMode mode)
      => mode switch // First, handle the direct rounding strategies.
      {
        RoundingMode.Envelop => RoundEnvelop(x),
        RoundingMode.Ceiling => RoundCeiling(x),
        RoundingMode.Floor => RoundFloor(x),
        RoundingMode.Truncate => RoundTruncate(x),
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

    #endregion Static methods

    #region Implemented interfaces
    public double RoundNumber(double x, RoundingMode mode) => Round(x, mode);
    #endregion Implemented interfaces
  }

#endif
}
