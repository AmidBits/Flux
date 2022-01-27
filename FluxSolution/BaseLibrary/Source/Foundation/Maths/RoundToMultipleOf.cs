namespace Flux
{
  public static partial class Maths // Full (integer) rounding:
  {
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static decimal RoundToMultipleOf(decimal value, decimal interval, FullRounding mode)
      => Round(value / interval, mode) * interval;

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static double RoundToMultipleOf(double value, double interval, FullRounding mode)
      => Round(value / interval, mode) * interval;

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static int RoundToMultipleOf(int number, int interval, FullRounding mode)
    {
      if (number % interval is var remainder && remainder == 0)
        return number;

      var roundedTowardZero = number - remainder;

      return mode switch
      {
        FullRounding.AwayFromZero => number < 0 ? roundedTowardZero - interval : roundedTowardZero + interval,
        FullRounding.Ceiling => number < 0 ? roundedTowardZero : roundedTowardZero + interval,
        FullRounding.Floor => number < 0 ? roundedTowardZero - interval : roundedTowardZero,
        FullRounding.TowardZero => roundedTowardZero,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static long RoundToMultipleOf(long number, long interval, FullRounding mode)
    {
      if (number % interval is var remainder && remainder == 0)
        return number;

      var roundedTowardZero = number - remainder;

      return mode switch
      {
        FullRounding.AwayFromZero => number < 0 ? roundedTowardZero - interval : roundedTowardZero + interval,
        FullRounding.Ceiling => number < 0 ? roundedTowardZero : roundedTowardZero + interval,
        FullRounding.Floor => number < 0 ? roundedTowardZero - interval : roundedTowardZero,
        FullRounding.TowardZero => roundedTowardZero,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }
  }

  public static partial class Maths // Half (midpoint) rounding:
  {
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static decimal RoundToMultipleOf(decimal value, decimal interval, HalfRounding mode)
    => Round(value / interval, mode) * interval;

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static double RoundToMultipleOf(double value, double interval, HalfRounding mode)
      => Round(value / interval, mode) * interval;
  }
}
