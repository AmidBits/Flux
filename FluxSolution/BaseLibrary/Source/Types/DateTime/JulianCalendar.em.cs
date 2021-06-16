namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.DateTime FromJulianDate(this double julianDate)
      => System.DateTime.FromOADate(julianDate - 2415018.5);
    public static System.DateTime FromModifiedJulianDate(this double modifiedJulianDate)
      => FromJulianDate(modifiedJulianDate + 2400000.5);
    public static System.DateTime FromReducedJulianDate(this double reducedJulianDate)
      => FromJulianDate(reducedJulianDate + 2400000);
    public static System.DateTime FromTruncatedJulianDate(this double truncatedJulianDate)
      => FromJulianDate(truncatedJulianDate + 2440000.5);

    public static double ToJulianDate(this System.DateTime date)
      => date.ToOADate() + 2415018.5;
    public static double ToModifiedJulianDate(this System.DateTime date)
      => date.ToJulianDate() - 2400000.5;
    public static double ToReducedJulianDate(this System.DateTime date)
      => date.ToJulianDate() - 2400000;
    public static double ToTruncatedJulianDate(this System.DateTime date)
      => System.Math.Floor(date.ToJulianDate() - 2440000.5);
  }
  //https://ia802807.us.archive.org/20/items/astronomicalalgorithmsjeanmeeus1991/Astronomical%20Algorithms-%20Jean%20Meeus%20%281991%29.pdf
  //public static class JulianDate
  //{
  //  private static double DateToJD(int year, int month, int day, int hour, int minute, int second, int millisecond)
  //  {
  //    bool isJD = IsJulianDate(year, month, day);

  //    int M = month > 2 ? month : month + 12;
  //    int Y = month > 2 ? year : year - 1;
  //    double D = day + hour / 24.0 + minute / 1440.0 + (second + millisecond / 1000.0) / 86400.0;
  //    int B = isJD ? 0 : 2 - Y / 100 + Y / 100 / 4;

  //    return (int)(365.25 * (Y + 4716)) + (int)(30.6001 * (M + 1)) + D + B - 1524.5;
  //  }

  //  public static double From(int year, int month, int day, int hour, int minute, int second, int millisecond)
  //    => DateToJD(year, month, day, hour, minute, second, millisecond);

  //  public static double From(System.DateTime dateTime)
  //    => DateToJD(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);

  //  public static bool IsJulianDate(int year, int month, int day)
  //  {
  //    if (year < 1582) // Dates prior to 1582 are Julian dates.
  //      return true;
  //    else if (year > 1582) // Dates after 1582 are Gregorian dates.
  //      return false;
  //    else // If 1582, check before October 4 (Julian) or after October 15 (Gregorian)
  //    {
  //      if (month < 10) // Dates before October, 1582 are Julian dates.
  //        return true;
  //      else if (month > 10) // Dates after October, 1582 are Gregorian dates.
  //        return false;
  //      else
  //      {
  //        if (day < 5)
  //          return true;
  //        else if (day > 14)
  //          return false;
  //        else
  //          throw new System.InvalidOperationException($"This date is invalid.");
  //      }
  //    }
  //  }
  //}
}
