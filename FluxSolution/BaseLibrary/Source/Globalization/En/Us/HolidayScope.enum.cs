namespace Flux
{
  public static partial class ExtensionMethodsEnUs
  {
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, System.DateTime>> GetHolidays(this Globalization.EnUs.HolidayScope source, System.DateTime timestamp)
    {
      switch (source)
      {
        case Globalization.EnUs.HolidayScope.Federal:
          yield return System.Collections.Generic.KeyValuePair.Create(@"New Year's Day", new System.DateTime(timestamp.Year, 1, 1));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Birthday of Martin Luther King, Jr", new System.DateTime(timestamp.Year, 1, 1).NextDayOfWeek(System.DayOfWeek.Monday, true).AddDays(14));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Washington's Birthday", new System.DateTime(timestamp.Year, 2, 1).NextDayOfWeek(System.DayOfWeek.Monday, true).AddDays(14));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Memorial Day", new System.DateTime(timestamp.Year, 5, 31).PreviousDayOfWeek(System.DayOfWeek.Monday, true));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Independence Day", new System.DateTime(timestamp.Year, 7, 4));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Labor Day", new System.DateTime(timestamp.Year, 9, 1).NextDayOfWeek(System.DayOfWeek.Monday, true));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Columbus Day", new System.DateTime(timestamp.Year, 10, 1).NextDayOfWeek(System.DayOfWeek.Monday, true).AddDays(7));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Leif Erikson Day", new System.DateTime(timestamp.Year, 10, 9));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Veterans Day", new System.DateTime(timestamp.Year, 11, 11));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Thanksgiving Day", new System.DateTime(timestamp.Year, 11, 1).NextDayOfWeek(System.DayOfWeek.Thursday, true).AddDays(21));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Christmas Day", new System.DateTime(timestamp.Year, 12, 25));
          yield break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(source));
      }
    }
  }

  namespace Globalization.EnUs
  {
    public enum HolidayScope
    {
      Federal
    }
  }
}
