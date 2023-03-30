namespace Flux
{
  public static partial class GlobalizationExtensionMethods
  {
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, System.DateTime>> GetHolidays(this System.DateTime source, Globalization.EnUs.HolidayScope scope)
    {
      switch (scope)
      {
        case Globalization.EnUs.HolidayScope.Federal:
          yield return System.Collections.Generic.KeyValuePair.Create(@"New Year's Day", new System.DateTime(source.Year, 1, 1));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Birthday of Martin Luther King, Jr", new System.DateTime(source.Year, 1, 1).NextDayOfWeek(System.DayOfWeek.Monday, true).AddDays(14));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Washington's Birthday", new System.DateTime(source.Year, 2, 1).NextDayOfWeek(System.DayOfWeek.Monday, true).AddDays(14));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Memorial Day", new System.DateTime(source.Year, 5, 31).PreviousDayOfWeek(System.DayOfWeek.Monday, true));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Independence Day", new System.DateTime(source.Year, 7, 4));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Labor Day", new System.DateTime(source.Year, 9, 1).NextDayOfWeek(System.DayOfWeek.Monday, true));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Columbus Day", new System.DateTime(source.Year, 10, 1).NextDayOfWeek(System.DayOfWeek.Monday, true).AddDays(7));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Leif Erikson Day", new System.DateTime(source.Year, 10, 9));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Veterans Day", new System.DateTime(source.Year, 11, 11));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Thanksgiving Day", new System.DateTime(source.Year, 11, 1).NextDayOfWeek(System.DayOfWeek.Thursday, true).AddDays(21));
          yield return System.Collections.Generic.KeyValuePair.Create(@"Christmas Day", new System.DateTime(source.Year, 12, 25));
          yield break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(scope));
      }
    }
  }
}
