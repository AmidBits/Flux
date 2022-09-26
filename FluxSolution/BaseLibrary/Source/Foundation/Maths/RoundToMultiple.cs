namespace Flux
{
  public static partial class Maths // Full (integer) rounding:
  {
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static decimal RoundToMultiple(decimal value, decimal interval, FullRoundingBehavior mode)
      => Round(value / interval, mode) * interval;

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static double RoundToMultiple(double value, double interval, FullRoundingBehavior mode)
      => Round(value / interval, mode) * interval;

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static int RoundToMultiple(int number, int interval, FullRoundingBehavior mode)
    {
      if (number % interval is var remainder && remainder == 0)
        return number;

      var roundedTowardZero = number - remainder;

      return mode switch
      {
        FullRoundingBehavior.AwayFromZero => number < 0 ? roundedTowardZero - interval : roundedTowardZero + interval,
        FullRoundingBehavior.Ceiling => number < 0 ? roundedTowardZero : roundedTowardZero + interval,
        FullRoundingBehavior.Floor => number < 0 ? roundedTowardZero - interval : roundedTowardZero,
        FullRoundingBehavior.TowardZero => roundedTowardZero,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static long RoundToMultiple(long number, long interval, FullRoundingBehavior mode)
    {
      if (number % interval is var remainder && remainder == 0)
        return number;

      var roundedTowardZero = number - remainder;

      return mode switch
      {
        FullRoundingBehavior.AwayFromZero => number < 0 ? roundedTowardZero - interval : roundedTowardZero + interval,
        FullRoundingBehavior.Ceiling => number < 0 ? roundedTowardZero : roundedTowardZero + interval,
        FullRoundingBehavior.Floor => number < 0 ? roundedTowardZero - interval : roundedTowardZero,
        FullRoundingBehavior.TowardZero => roundedTowardZero,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }
  }

  public static partial class Maths // Half (midpoint) rounding:
  {
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static decimal RoundToMultiple(decimal value, decimal interval, HalfRoundingBehavior mode)
    => Round(value / interval, mode) * interval;

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static double RoundToMultiple(double value, double interval, HalfRoundingBehavior mode)
      => Round(value / interval, mode) * interval;
  }
}
