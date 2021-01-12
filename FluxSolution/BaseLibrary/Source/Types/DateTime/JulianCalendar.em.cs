namespace Flux
{
  /// <seealso cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public static partial class SystemDateTimeEm
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
  public static class JulianDate
  {
    private static double DateToJD(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
      bool isJD = IsJulianDate(year, month, day);

      int M = month > 2 ? month : month + 12;
      int Y = month > 2 ? year : year - 1;
      double D = day + hour / 24.0 + minute / 1440.0 + (second + millisecond / 1000.0) / 86400.0;
      int B = isJD ? 0 : 2 - Y / 100 + Y / 100 / 4;

      return (int)(365.25 * (Y + 4716)) + (int)(30.6001 * (M + 1)) + D + B - 1524.5;
    }

    public static double From(int year, int month, int day, int hour, int minute, int second, int millisecond)
      => DateToJD(year, month, day, hour, minute, second, millisecond);

    public static double From(System.DateTime dateTime)
      => DateToJD(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);

    public static bool IsJulianDate(int year, int month, int day)
    {
      if (year < 1582) // Dates prior to 1582 are Julian dates.
        return true;
      else if (year > 1582) // Dates after 1582 are Gregorian dates.
        return false;
      else // If 1582, check before October 4 (Julian) or after October 15 (Gregorian)
      {
        if (month < 10) // Dates before October, 1582 are Julian dates.
          return true;
        else if (month > 10) // Dates after October, 1582 are Gregorian dates.
          return false;
        else
        {
          if (day < 5)
            return true;
          else if (day > 14)
            return false;
          else
            throw new System.InvalidOperationException($"This date is invalid.");
        }
      }
    }
  }

  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public static class JulianDayNumber
  {
    public static System.DayOfWeek ToDayOfWeek(int julianDayNumber)
      => (System.DayOfWeek)(((julianDayNumber % 7) + 1) % 7);
    /// <summary>Returns the JDN (integer value ) for the Gregorian date.</summary>
    /// <param name="julianDayNumber">The Julian day number (JDN) is the integer assigned to a whole solar day in the Julian day count starting from noon Universal time, with Julian day number 0 assigned to the day starting at noon on Monday, January 1, 4713 BC, proleptic Julian calendar (November 24, 4714 BC, in the proleptic Gregorian calendar), a date at which three multi-year cycles started (which are: Indiction, Solar, and Lunar cycles) and which preceded any dates in recorded history.[a] For example, the Julian day number for the day starting at 12:00 UT (noon) on January 1, 2000, was 2,451,545.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Proleptic_Gregorian_calendar"/>
    public static (int year, int month, int day) ToGregorianDate(int julianDayNumber)
    {
      var J = julianDayNumber;

      var y = 4716;
      var v = 3;
      var j = 1401;
      var u = 5;
      var m = 2;
      var s = 153;
      var n = 12;
      var w = 2;
      var r = 4;
      var B = 274277;
      var p = 1461;
      var C = -38;

      var f = J + j + (((4 * J + B) / 146097) * 3) / 4 + C;
      var e = r * f + v;
      var g = (e % p) / r;
      var h = u * g + w;

      var D = (h % s) / u + 1;
      var M = ((h / s + m) % n) + 1;
      var Y = (e / p) - y + (n + m - M) / n;

      return (Y, M, D);
    }

    /// <summary>Returns the Gregorian date for the JDN.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Proleptic_Julian_calendar"/>
    public static int FromGregorianDate(int year, int month, int day)
    //=> (1461 * (year + 4800 + (month - 14) / 12)) / 4 + (367 * (month - 2 - 12 * ((month - 14) / 12))) / 12 - (3 * ((year + 4900 + (month - 14) / 12) / 100)) / 4 + day - 32075;
    {
      var a = (14 - month) / 12;
      var y = year + 4800 - a;
      var m = month + 12 * a - 3;

      return day + (153 * m + 2) / 5 + 365 * y + y / 4 - y / 100 + y / 400 - 32045;
    }
  }
}
