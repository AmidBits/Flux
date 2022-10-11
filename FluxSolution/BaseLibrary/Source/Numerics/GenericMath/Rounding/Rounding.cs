#if NET7_0_OR_GREATER
namespace Flux
{
  public class Rounding<TSelf>
    : INumberRoundable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    public static Rounding<TSelf> HalfwayToEven => new(RoundingMode.HalfwayToEven);
    public static Rounding<TSelf> HalfwayToAwayFromZero => new(RoundingMode.HalfwayAwayFromZero);
    public static Rounding<TSelf> HalfwayTowardZero => new(RoundingMode.HalfwayTowardZero);
    public static Rounding<TSelf> HalfwayToNegativeInfinity => new(RoundingMode.HalfwayToNegativeInfinity);
    public static Rounding<TSelf> HalfwayToPositiveInfinity => new(RoundingMode.HalfwayToPositiveInfinity);
    public static Rounding<TSelf> HalfwayToOdd => new(RoundingMode.HalfwayToOdd);

    public static Rounding<TSelf> Envelop => new(RoundingMode.Envelop);
    public static Rounding<TSelf> Truncate => new(RoundingMode.Truncate);
    public static Rounding<TSelf> Ceiling => new(RoundingMode.Ceiling);
    public static Rounding<TSelf> Floor => new(RoundingMode.Floor);

    private readonly RoundingMode m_mode;

    public Rounding(RoundingMode mode)
      => m_mode = mode;

#region Static methods

    // Halfway Rounding Extension Methods
    /// <summary>PREVIEW! Common rounding: round half down, bias: negative infinity.</summary>
    public static TSelf HalfwayRoundDown(TSelf x)
      => TSelf.Ceiling(x - (TSelf.One.Div2()));

    /// <summary>PREVIEW! Symmetric rounding: round half down, bias: towards zero.</summary>
    public static TSelf HalfwayRoundDownZero(TSelf x)
      => TSelf.CopySign(HalfwayRoundDown(TSelf.Abs(x)), x);

    /// <summary>PREVIEW! Common rounding: round half up, bias: positive infinity.</summary>
    public static TSelf HalfwayRoundUp(TSelf x)
      => TSelf.Floor(x + (TSelf.One.Div2()));

    /// <summary>PREVIEW! Symmetric rounding: round half up, bias: away from zero.</summary>
    public static TSelf HalfwayRoundUpZero(TSelf x)
      => TSelf.CopySign(HalfwayRoundUp(TSelf.Abs(x)), x);

    // Integer Rounding Extension Methods
    /// <summary>PREVIEW! Symmetric rounding: round up, bias: away from zero.</summary>
    public static TSelf IntegerRoundEnvelop(TSelf x)
      => TSelf.Sign(x) < 0 ? TSelf.Floor(x) : TSelf.Ceiling(x);

    /// <summary>PREVIEW! Common rounding: round up, bias: positive infinity.</summary>
    public static TSelf IntegerRoundCeiling(TSelf x)
     => TSelf.Ceiling(x);

    /// <summary>PREVIEW! Common rounding: round down, bias: negative infinity.</summary>
    public static TSelf IntegerRoundFloor(TSelf x)
      => TSelf.Floor(x);

    /// <summary>PREVIEW! Symmetric rounding: round down, bias: towards zero.</summary>
    public static TSelf IntegerRoundTruncate(TSelf x)
      => TSelf.Truncate(x);

#endregion Static methods

#region Implemented interfaces
    public TSelf RoundNumber(TSelf x)
    {
      var two = TSelf.CreateChecked(2);
      var halfOfOne = TSelf.CreateChecked(0.5);

      return m_mode switch
      {
        RoundingMode.HalfwayToEven => TSelf.Floor(x + halfOfOne) is var pi && !TSelf.IsZero(pi % two) && x - TSelf.Floor(x) == halfOfOne ? pi - TSelf.One : pi,
        RoundingMode.HalfwayAwayFromZero => HalfwayRoundUpZero(x),
        RoundingMode.HalfwayTowardZero => HalfwayRoundDownZero(x),
        RoundingMode.HalfwayToNegativeInfinity => HalfwayRoundDown(x),
        RoundingMode.HalfwayToPositiveInfinity => HalfwayRoundUp(x),
        RoundingMode.HalfwayToOdd => TSelf.Floor(x + halfOfOne) is var pi && TSelf.IsZero(pi % two) && x - TSelf.Floor(x) == halfOfOne ? pi - TSelf.One : pi,
        RoundingMode.Envelop => IntegerRoundEnvelop(x),
        RoundingMode.Ceiling => IntegerRoundCeiling(x),
        RoundingMode.Floor => IntegerRoundFloor(x),
        RoundingMode.Truncate => IntegerRoundTruncate(x),
        _ => throw new System.ArgumentOutOfRangeException(m_mode.ToString()),
      };
    }
#endregion Implemented interfaces
  }
}
#endif
