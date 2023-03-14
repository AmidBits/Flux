namespace Flux
{
  public static partial class ExtensionMethodsDateTime
  {
    /// <summary>Yields a sequence of dates between the source and the specified number of days.</summary>
    /// <param name="source">Start date.</param>
    /// <param name="numberOfDays">The number of days to include. Can be negative.</param>
    public static System.Collections.Generic.IEnumerable<System.DateTime> GetDatesFor(this System.DateTime source, int numberOfDays)
    {
      yield return source;

      for (int sign = numberOfDays >= 0 ? 1 : -1, index = sign; index != numberOfDays; index += sign)
        yield return source.AddDays(index);
    }

    /// <summary>Yields the dates in the month of the source.</summary>
    public static System.Collections.Generic.IEnumerable<System.DateTime> GetDatesInMonth(this System.DateTime source)
      => source.FirstDayOfMonth().GetDatesFor(System.DateTime.DaysInMonth(source.Year, source.Month));

    /// <summary>Yields the dates in the current calendar quarter of the source.</summary>
    public static System.Collections.Generic.IEnumerable<System.DateTime> GetDatesInQuarter(this System.DateTime source)
      => FirstDayOfQuarter(source).GetDatesTo(LastDayOfQuarter(source), true);

    /// <summary>Yields the dates in the week of the source.</summary>
    public static System.Collections.Generic.IEnumerable<System.DateTime> GetDatesInWeek(this System.DateTime source)
      => source.FirstDayOfWeek().GetDatesFor(7);

    /// <summary>Yields the dates in the year of the source.</summary>
    public static System.Collections.Generic.IEnumerable<System.DateTime> GetDatesInYear(this System.DateTime source)
      => source.FirstDayOfYear().GetDatesFor(source.DaysInYear());

    /// <summary>Yields a sequence of dates between the source and the specified target, which can be included or not.</summary>
    public static System.Collections.Generic.IEnumerable<System.DateTime> GetDatesTo(this System.DateTime source, System.DateTime target, bool includeTarget)
      => source.GetDatesFor((target - source).Days is var numberOfDays && includeTarget ? numberOfDays + System.Math.Sign(numberOfDays) : numberOfDays);
  }
}
