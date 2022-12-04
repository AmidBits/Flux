namespace Flux
{
  /// <summary></summary>
  public class Rounding<TSelf>
    : INumberRoundable<TSelf, TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    private readonly RoundingMode m_mode;

    public Rounding(RoundingMode mode)
      => m_mode = mode;

    #region Static methods

    #region Halfway rounding functions
    /// <summary>Symmetric rounding: round half up, bias: away from zero.</summary>
    public static TSelf RoundHalfAwayFromZero(TSelf x)
      => TSelf.CopySign(RoundHalfToPositiveInfinity(TSelf.Abs(x)), x);

    /// <summary>Common rounding: round half, bias: even.</summary>
    public static TSelf RoundHalfToEven(TSelf x)
    {
      var half = TSelf.CreateChecked(0.5);

      var positiveInfinity = TSelf.Floor(x + half);

      return !TSelf.IsZero(positiveInfinity % TSelf.CreateChecked(2)) && x - TSelf.Floor(x) == half ? positiveInfinity - TSelf.One : positiveInfinity;
    }

    /// <summary>Common rounding: round half down, bias: negative infinity.</summary>
    public static TSelf RoundHalfToNegativeInfinity(TSelf x)
      => TSelf.Ceiling(x - TSelf.CreateChecked(0.5));

    /// <summary>Common rounding: round half, bias: even.</summary>
    public static TSelf RoundHalfToOdd(TSelf x)
    {
      var half = TSelf.CreateChecked(0.5);

      var positiveInfinity = TSelf.Floor(x + half);

      return TSelf.IsZero(positiveInfinity % TSelf.CreateChecked(2)) && x - TSelf.Floor(x) == half ? positiveInfinity - TSelf.One : positiveInfinity;
    }

    /// <summary>Common rounding: round half up, bias: positive infinity.</summary>
    public static TSelf RoundHalfToPositiveInfinity(TSelf x)
      => TSelf.Floor(x + TSelf.CreateChecked(0.5));

    /// <summary>Symmetric rounding: round half down, bias: towards zero.</summary>
    public static TSelf RoundHalfTowardZero(TSelf x)
      => TSelf.CopySign(RoundHalfToNegativeInfinity(TSelf.Abs(x)), x);
    #endregion Halfway rounding functions

    #region Direct (non-halfway) rounding functions
    /// <summary>Common rounding: round up, bias: positive infinity.</summary>
    public static TSelf RoundCeiling(TSelf x)
     => TSelf.Ceiling(x);

    /// <summary>Symmetric rounding: round up, bias: away from zero.</summary>
    public static TSelf RoundEnvelop(TSelf x)
      => TSelf.Sign(x) < 0 ? TSelf.Floor(x) : TSelf.Ceiling(x);

    /// <summary>Common rounding: round down, bias: negative infinity.</summary>
    public static TSelf RoundFloor(TSelf x)
      => TSelf.Floor(x);

    /// <summary>Symmetric rounding: round down, bias: towards zero.</summary>
    public static TSelf RoundTruncate(TSelf x)
      => TSelf.Truncate(x);
    #endregion Direct (non-halfway) rounding functions

    public static TSelf Round(TSelf x, RoundingMode mode)
    {
      return mode switch
      {
        RoundingMode.Envelop => RoundEnvelop(x),
        RoundingMode.Ceiling => RoundCeiling(x),
        RoundingMode.Floor => RoundFloor(x),
        RoundingMode.Truncate => RoundTruncate(x),
        _ => mode switch
        {
          RoundingMode.HalfAwayFromZero => RoundHalfAwayFromZero(x),
          RoundingMode.HalfTowardZero => RoundHalfTowardZero(x),
          RoundingMode.HalfToEven => TSelf.CreateChecked(2) is var two && TSelf.CreateChecked(0.5) is var half && TSelf.Floor(x + half) is var pi && !TSelf.IsZero(pi % two) && x - TSelf.Floor(x) == half ? pi - TSelf.One : pi,
          RoundingMode.HalfToNegativeInfinity => RoundHalfToNegativeInfinity(x),
          RoundingMode.HalfToOdd => TSelf.CreateChecked(2) is var two && TSelf.CreateChecked(0.5) is var half && TSelf.Floor(x + half) is var pi && TSelf.IsZero(pi % two) && x - TSelf.Floor(x) == half ? pi - TSelf.One : pi,
          RoundingMode.HalfToPositiveInfinity => RoundHalfToPositiveInfinity(x),
          _ => throw new System.ArgumentOutOfRangeException(mode.ToString()),
        }
      };
    }

    #endregion Static methods

    #region Implemented interfaces
    public TSelf RoundNumber(TSelf x)
      => Round(x, m_mode);
    #endregion Implemented interfaces
  }
}
