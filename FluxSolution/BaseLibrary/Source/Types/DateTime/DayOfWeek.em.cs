namespace Flux
{
  public static partial class ExtensionMethodsDateTime
  {
    public static void LocateNearestDayOfWeek(this System.DateTime source, System.DayOfWeek dayOfWeek, bool proper, out System.DateTime nearestPrevious, out System.DateTime nearestNext)
    {
      nearestPrevious = PreviousDayOfWeek(source, dayOfWeek, proper);
      nearestNext = NextDayOfWeek(source, dayOfWeek, proper);
    }
    public static System.DateTime NearestDayOfWeek(this System.DateTime source, System.DayOfWeek dayOfWeek, bool proper, RoundingMode mode, out System.DateTime nearestPrevious, out System.DateTime nearestNext)
    {
      LocateNearestDayOfWeek(source, dayOfWeek, proper, out nearestPrevious, out nearestNext);

      return new System.DateTime(System.Convert.ToInt64(source.Ticks.RoundToBoundary(mode, nearestPrevious.Ticks, nearestNext.Ticks)));
    }


    /// <summary>Determines the closest DayOfWeek date before and after the source.</summary>
    public static System.DateTime Closest(this System.DateTime source, System.DayOfWeek dayOfWeek, out System.DateTime secondClosest)
    {
      var next = NextDayOfWeek(source, dayOfWeek, false);
      var previous = PreviousDayOfWeek(source, dayOfWeek, false);

      if (next.Subtract(source) < source.Subtract(previous))
      {
        secondClosest = previous;
        return next;
      }
      else
      {
        secondClosest = next;
        return previous;
      }
    }

    /// <summary>Yields the date of the next specified dayOfWeek relative to the source. In order to return same date for same dayOfWeek, subtract 1 day from the source before calling this function.</summary>
    public static System.DateTime NextDayOfWeek(this System.DateTime source, System.DayOfWeek dayOfWeek, bool excludeSource)
      => source.DayOfWeek == dayOfWeek && excludeSource ? source.AddDays(7) : source.AddDays(unchecked((int)dayOfWeek - (int)source.DayOfWeek + 7) % 7);

    /// <summary>Yields the date of the previous specified dayOfWeek relative to the source. In order to return same date for same dayOfWeek, add 1 day to the source before calling this function.</summary>
    public static System.DateTime PreviousDayOfWeek(this System.DateTime source, System.DayOfWeek dayOfWeek, bool excludeSource)
      => source.DayOfWeek == dayOfWeek && excludeSource ? source.AddDays(-7) : source.AddDays(unchecked((int)dayOfWeek - (int)source.DayOfWeek - 7) % 7);
  }
}
