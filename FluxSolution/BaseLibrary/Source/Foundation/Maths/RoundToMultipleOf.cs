namespace Flux
{
  public static partial class Maths // Full (integer) rounding:
  {
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static decimal RoundToMultipleOf(decimal value, decimal interval, FullRoundingBehavior mode)
      => RoundTo(value / interval, mode) * interval;

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static double RoundToMultipleOf(double value, double interval, FullRoundingBehavior mode)
      => RoundTo(value / interval, mode) * interval;
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static float RoundToMultipleOf(float value, float interval, FullRoundingBehavior mode)
      => RoundTo(value / interval, mode) * interval;

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static int RoundToMultipleOf(int number, int interval, FullRoundingBehavior mode)
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
    public static long RoundToMultipleOf(long number, long interval, FullRoundingBehavior mode)
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
    public static decimal RoundToMultipleOf(decimal value, decimal interval, HalfRoundingBehavior mode)
    => RoundTo(value / interval, mode) * interval;

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static double RoundToMultipleOf(double value, double interval, HalfRoundingBehavior mode)
      => RoundTo(value / interval, mode) * interval;
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static float RoundToMultipleOf(float value, float interval, HalfRoundingBehavior mode)
      => RoundTo(value / interval, mode) * interval;
  }
}
