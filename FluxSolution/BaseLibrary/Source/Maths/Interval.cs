namespace Flux
{
  /// <summary>Represents the available interval rounding behaviors.</summary>
  public enum IntervalRoundingBehavior
  {
    /// <summary>Rounds an interval further from zero.</summary>
    AwayFromZero = RoundingBehavior.RoundAwayFromZero,
    /// <summary>Rounds an interval to the next lower integer.</summary>
    Ceiling = RoundingBehavior.RoundCeiling,
    /// <summary>Rounds an interval to the next greater integer.</summary>
    Floor = RoundingBehavior.RoundFloor,
    /// <summary>Rounds every number with a fraction part to the surrounding integer that is closer to zero.</summary>
    TowardZero = RoundingBehavior.RoundTowardZero
  }

  public static partial class Maths
  {
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static decimal ToInterval(decimal value, decimal interval, RoundingBehavior mode)
      => RoundToNearest(value / interval, mode) * interval;
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static double ToInterval(double value, double interval, RoundingBehavior mode)
      => RoundToNearest(value / interval, mode) * interval;


    /// <summary></summary>
    public static int ToInterval(int value, int multiple, IntervalRoundingBehavior mode)
    {
      var quotient = System.Math.DivRem(value, multiple, out var remainder);

      var rounded = quotient * multiple;

      if (remainder == 0)
        return rounded;
      else
        return mode switch
        {
          IntervalRoundingBehavior.AwayFromZero => rounded < 0 ? rounded - multiple : rounded + multiple,
          IntervalRoundingBehavior.Ceiling => rounded > 0 ? rounded + multiple : rounded,
          IntervalRoundingBehavior.Floor => rounded < 0 ? rounded - multiple : rounded,
          IntervalRoundingBehavior.TowardZero => rounded,
          _ => throw new System.ArgumentOutOfRangeException(nameof(mode))
        };
    }
    /// <summary></summary>
    public static long ToInterval(long value, long multiple, IntervalRoundingBehavior mode)
    {
      var quotient = System.Math.DivRem(value, multiple, out var remainder);

      var rounded = quotient * multiple;

      if (remainder == 0)
        return rounded;
      else
        return mode switch
        {
          IntervalRoundingBehavior.AwayFromZero => rounded < 0 ? rounded - multiple : rounded + multiple,
          IntervalRoundingBehavior.Ceiling => rounded > 0 ? rounded + multiple : rounded,
          IntervalRoundingBehavior.Floor => rounded < 0 ? rounded - multiple : rounded,
          IntervalRoundingBehavior.TowardZero => rounded,
          _ => throw new System.ArgumentOutOfRangeException(nameof(mode))
        };
    }
  }
}
