namespace Flux
{
  public static partial class Maths
  {
    // Round to interval with full rounding behavior.

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static decimal RoundToInterval(decimal value, decimal interval, FullRoundingBehavior mode)
      => RoundToNearest(value / interval, mode) * interval;

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static double RoundToInterval(double value, double interval, FullRoundingBehavior mode)
      => RoundToNearest(value / interval, mode) * interval;
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static float RoundToInterval(float value, float interval, FullRoundingBehavior mode)
      => RoundToNearest(value / interval, mode) * interval;

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static int RoundToInterval(int number, int interval, FullRoundingBehavior mode)
    {
      if (number % interval is var remainder && remainder == 0)
        return number;

      var roundedTowardZero = number - remainder;

      switch (mode)
      {
        case FullRoundingBehavior.AwayFromZero:
          return number < 0 ? roundedTowardZero - interval : roundedTowardZero + interval;
        case FullRoundingBehavior.Ceiling:
          return number < 0 ? roundedTowardZero : roundedTowardZero + interval;
        case FullRoundingBehavior.Floor:
          return number < 0 ? roundedTowardZero - interval : roundedTowardZero;
        case FullRoundingBehavior.TowardZero:
          return roundedTowardZero;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(mode));
      }
    }
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static long RoundToInterval(long number, long interval, FullRoundingBehavior mode)
    {
      if (number % interval is var remainder && remainder == 0)
        return number;

      var roundedTowardZero = number - remainder;

      switch (mode)
      {
        case FullRoundingBehavior.AwayFromZero:
          return number < 0 ? roundedTowardZero - interval : roundedTowardZero + interval;
        case FullRoundingBehavior.Ceiling:
          return number < 0 ? roundedTowardZero : roundedTowardZero + interval;
        case FullRoundingBehavior.Floor:
          return number < 0 ? roundedTowardZero - interval : roundedTowardZero;
        case FullRoundingBehavior.TowardZero:
          return roundedTowardZero;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(mode));
      }
    }

    // Round to interval with half rounding behavior.

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static decimal RoundToInterval(decimal value, decimal interval, HalfRoundingBehavior mode)
      => RoundToNearest(value / interval, mode) * interval;

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static double RoundToInterval(double value, double interval, HalfRoundingBehavior mode)
      => RoundToNearest(value / interval, mode) * interval;
    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when equally distant between two intervals.</summary>
    public static float RoundToInterval(float value, float interval, HalfRoundingBehavior mode)
      => RoundToNearest(value / interval, mode) * interval;
  }
}
