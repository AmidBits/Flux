namespace Flux
{
  public static partial class DateTimes
  {
    /// <summary>Determines the two closest DayOfWeek dates before and after the source.</summary>
    public static (System.DateTime closest, System.DateTime secondClosest) DayOfWeekClosest(this System.DateTime source, System.DayOfWeek dayOfWeek, bool unequal)
    {
      var last = DayOfWeekLast(source, dayOfWeek, unequal);
      var next = DayOfWeekNext(source, dayOfWeek, unequal);

      return next.Subtract(source) < source.Subtract(last) ? (next, last) : (last, next);
    }

    /// <summary>Yields the <see cref="System.DateTime"/> of the previous specified <paramref name="dayOfWeek"/> relative to the <paramref name="source"/>. Use <paramref name="unequal"/> to (false = include, true = exclude) <paramref name="source"/> as a result for the past <see cref="System.DayOfWeek"/>.</summary>
    public static System.DateTime DayOfWeekLast(this System.DateTime source, System.DayOfWeek dayOfWeek, bool unequal)
      => source.DayOfWeek == dayOfWeek && unequal
      ? source.AddDays(-7)
      : source.AddDays(unchecked((int)dayOfWeek - (int)source.DayOfWeek - 7) % 7);

    /// <summary>Yields the <see cref="System.DateTime"/> of the next specified <paramref name="dayOfWeek"/> relative to the <paramref name="source"/>. Use <paramref name="unequal"/> to include/exclude <paramref name="source"/> as a result for the future <see cref="System.DayOfWeek"/>.</summary>
    public static System.DateTime DayOfWeekNext(this System.DateTime source, System.DayOfWeek dayOfWeek, bool unequal)
      => source.DayOfWeek == dayOfWeek && unequal
      ? source.AddDays(7)
      : source.AddDays(unchecked((int)dayOfWeek - (int)source.DayOfWeek + 7) % 7);
  }
}
