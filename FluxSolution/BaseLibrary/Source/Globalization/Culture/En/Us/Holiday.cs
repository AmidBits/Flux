namespace Flux.Globalization.EnUs
{
  public static partial class Holiday
  {
    public enum Scope
    {
      Federal
    }

    public static System.Collections.Generic.IEnumerable<(string name, System.DateTime date)> Get(System.DateTime source, Scope scope)
    {
      switch (scope)
      {
        case Scope.Federal:
          yield return (@"New Year's Day", new System.DateTime(source.Year, 1, 1));
          yield return (@"Birthday of Martin Luther King, Jr", new System.DateTime(source.Year, 1, 1).Next(System.DayOfWeek.Monday, true).AddDays(14));
          yield return (@"Washington's Birthday", new System.DateTime(source.Year, 2, 1).Next(System.DayOfWeek.Monday, true).AddDays(14));
          yield return (@"Memorial Day", new System.DateTime(source.Year, 5, 31).Previous(System.DayOfWeek.Monday, true));
          yield return (@"Independence Day", new System.DateTime(source.Year, 7, 4));
          yield return (@"Labor Day", new System.DateTime(source.Year, 9, 1).Next(System.DayOfWeek.Monday, true));
          yield return (@"Columbus Day", new System.DateTime(source.Year, 10, 1).Next(System.DayOfWeek.Monday, true).AddDays(7));
          yield return (@"Leif Erikson Day", new System.DateTime(source.Year, 10, 9));
          yield return (@"Veterans Day", new System.DateTime(source.Year, 11, 11));
          yield return (@"Thanksgiving Day", new System.DateTime(source.Year, 11, 1).Next(System.DayOfWeek.Thursday, true).AddDays(21));
          yield return (@"Christmas Day", new System.DateTime(source.Year, 12, 25));
          yield break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(scope));
      }
    }
  }
}
