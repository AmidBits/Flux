namespace Flux
{
  public static partial class Fx
  {
    public static void LocateNearestDayOfWeek(this System.DateTime source, System.DayOfWeek dayOfWeek, bool proper, out System.DateTime nearestPrevious, out System.DateTime nearestNext)
    {
      nearestPrevious = PreviousDayOfWeek(source, dayOfWeek, proper);
      nearestNext = NextDayOfWeek(source, dayOfWeek, proper);
    }
    public static System.DateTime NearestDayOfWeek(this System.DateTime source, System.DayOfWeek dayOfWeek, bool proper, RoundingMode mode, out System.DateTime nearestPrevious, out System.DateTime nearestNext)
    {
      LocateNearestDayOfWeek(source, dayOfWeek, proper, out nearestPrevious, out nearestNext);

      return new System.DateTime(System.Convert.ToInt64(((double)(source.Ticks)).RoundToBoundaries(mode, (double)nearestPrevious.Ticks, (double)nearestNext.Ticks)));
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

    /// <summary>Yields the <see cref="System.DateTime"/> of the next specified <paramref name="dayOfWeek"/> relative to the <paramref name="source"/>. Use <paramref name="proper"/> to include/exclude <paramref name="source"/> as the next <see cref="System.DayOfWeek"/>.</summary>
    public static System.DateTime NextDayOfWeek(this System.DateTime source, System.DayOfWeek dayOfWeek, bool proper)
      => source.DayOfWeek == dayOfWeek && proper ? source.AddDays(7) : source.AddDays(unchecked((int)dayOfWeek - (int)source.DayOfWeek + 7) % 7);

    /// <summary>Yields the <see cref="System.DateTime"/> of the previous specified <paramref name="dayOfWeek"/> relative to the <paramref name="source"/>. Use <paramref name="proper"/> to include/exclude <paramref name="source"/> as the previous <see cref="System.DayOfWeek"/>.</summary>
    public static System.DateTime PreviousDayOfWeek(this System.DateTime source, System.DayOfWeek dayOfWeek, bool proper)
      => source.DayOfWeek == dayOfWeek && proper ? source.AddDays(-7) : source.AddDays(unchecked((int)dayOfWeek - (int)source.DayOfWeek - 7) % 7);
  }
}
