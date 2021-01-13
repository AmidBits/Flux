namespace Flux
{
  /// <summary>Julian Day struct</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public struct JulianDay
    : System.IComparable<JulianDay>, System.IEquatable<JulianDay>
  {
    public static readonly JulianDay Empty;
    public bool IsEmpty => Equals(Empty);

    private double m_value;
    public double Value
      => m_value;

    public JulianDay(double julianDayNumber)
      => m_value = julianDayNumber;

    public JulianDay AddDays(int days)
      => new JulianDay(m_value + days);
    public JulianDay AddHours(int hours)
      => new JulianDay(m_value + hours / 24.0);
    public JulianDay AddMinutes(int minutes)
      => new JulianDay(m_value + minutes / 1440.0);
    public JulianDay AddSeconds(int seconds)
      => new JulianDay(m_value + seconds / 86400.0);

    public System.DayOfWeek ToDayOfWeek()
      => (System.DayOfWeek)ToDayOfWeekUS((int)m_value);

    public string ToProlepticGregorianDateString()
    {
      var (year, month, day) = ToProlepticGregorianCalendarDate((int)m_value);

      var (hour, minute, second) = ToTime(m_value);

      return $"{System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {day}, {AstronomicalYearToString(year)} {hour:D2}:{minute:D2}:{second:D2}";
    }

    public string ToProlepticJulianDateString()
    {
      var (year, month, day) = ToProlepticJulianCalendarDate((int)m_value);

      var (hour, minute, second) = ToTime(m_value);

      return $"{(System.DayOfWeek)ToDayOfWeekUS((int)m_value)}, {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {day}, {AstronomicalYearToString(year)} {hour:D2}:{minute:D2}:{second:D2}";
    }

    public static string AstronomicalYearToString(int astronomicalYear, string zeroOrLessSuffix = @"BC", string oneOrGreaterSuffix = @"AD")
      => astronomicalYear <= 0 ? $"{System.Math.Abs(astronomicalYear) + 1} {zeroOrLessSuffix}" : $"{astronomicalYear} {oneOrGreaterSuffix}";

    #region Static members

    /// <summary>returns the Julian Day Number (JDN) from the specified year, month and day from the Gregorian Calendar. The algorithm is valid for all (possibly proleptic) Gregorian calendar dates after November 23, -4713 (per Wikipedia).</summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <returns>The Julian day number corresponding to the specified Gregorian calendar date.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Proleptic_Gregorian_calendar"/>
    public static int NumberFromGregorianDate(int year, int month, int day)
      => (1461 * (year + 4800 + (month - 14) / 12)) / 4 + (367 * (month - 2 - 12 * ((month - 14) / 12))) / 12 - (3 * ((year + 4900 + (month - 14) / 12) / 100)) / 4 + day - 32075;

    /// <summary>returns the Julian Day Number (JDN) from the specified year, month and day from the Julian Calendar.</summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <returns>The Julian day number corresponding to the specified Julian calendar date.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
    /// <see cref="https://en.wikipedia.org/wiki/Proleptic_Julian_calendar"/>
    public static int NumberFromJulianDate(int year, int month, int day)
      => 367 * year - (7 * (year + 5001 + (month - 9) / 7)) / 4 + (275 * month) / 9 + day + 1729777;

    public static double JulianDateToJulianDay(int year, int month, double day)
    {
      if (month == 1 || month == 2)
      {
        year -= 1;
        month += 12;
      }

      return ((int)(365.25 * (year + 4716)) + (int)(30.6001 * (month + 1)) + day - 1524.5);
    }
    public static double GregorianDateToJulianDay(int year, int month, double day)
    {
      if (month == 1 || month == 2)
      {
        year -= 1;
        month += 12;
      }

      var A = year / 100;
      var B = 2 - A + (A / 4);

      return ((int)(365.25 * (year + 4716)) + (int)(30.6001 * (month + 1)) + day + B - 1524.5);
    }

    public static JulianDay Add(JulianDay jd, double b)
      => new JulianDay(jd.m_value + b);
    public static JulianDay Add(JulianDay jd, System.TimeSpan b)
      => AddTimeSpan(jd, b.Days, b.Hours, b.Minutes, b.Seconds);
    public static JulianDay AddTimeSpan(JulianDay jd, int days, int hours, int minutes, int seconds)
      => new JulianDay(jd.m_value + days + ((hours - 12) / 24.0) + (minutes / 1440.0) + (seconds / 86400.0));

    public static bool InJulianCalendar(int year, int month, int day)
      => year < 1582 || (year == 1582 && (month < 10 || (month == 10 && day <= 4)));
    public static bool InGregorianCalendar(int year, int month, int day)
      => year > 1582 || (year == 1582 && (month > 10 || (month == 10 && day >= 15)));

    /// <summary>Returns the US day of week which is from 0 (Sunday) through 6 (Saturday).</summary>
    public static int ToDayOfWeekUS(int julianDayNumber)
      => (julianDayNumber + 1) % 7;
    /// <summary>Returns the ISO day of week which is from 1 (Monday) through 7 (Sunday).</summary>
    public static int ToDayOfWeekISO(int julianDayNumber)
      => (julianDayNumber % 7) + 1;
    /// <summary>Returns the proleptic Julian date corresponding to the specified Julian Day Number (JDN).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Proleptic_Gregorian_calendar"/>
    public static (int year, int month, int day) ToProlepticJulianCalendarDate(int julianDayNumber)
    {
      if (julianDayNumber < 0) throw new System.ArgumentOutOfRangeException(nameof(julianDayNumber));

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

      var f = J + j;
      var e = r * f + v;
      var g = (e % p) / r;
      var h = u * g + w;

      var D = (h % s) / u + 1;
      var M = ((h / s + m) % n) + 1;
      var Y = (e / p) - y + (n + m - M) / n;

      return (Y, M, D);
    }
    /// <summary>Returns the proleptic Gregorian date corresponding to the specified Julian Day Number (JDN).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Proleptic_Gregorian_calendar"/>
    public static (int year, int month, int day) ToProlepticGregorianCalendarDate(int julianDayNumber)
    {
      if (julianDayNumber < 0) throw new System.ArgumentOutOfRangeException(nameof(julianDayNumber));

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
    public static (int hour, int minute, int second) ToTime(double julianDay)
    {
      var time = julianDay % 1;

      time = (time > 0.5 ? time - 0.5 : time + 0.5) * 86400;
      var hour = (int)(time / 3600);
      time -= (hour * 3600.0);
      var minute = (int)(time / 60);
      time -= (minute * 60.0);
      var second = (int)time;

      return (hour, minute, second);
    }
    public static JulianDay Subtract(JulianDay a, double b)
      => new JulianDay(a.m_value - b);

    // Operators
    public static JulianDay operator +(JulianDay a, double b)
      => Add(a, b);
    public static JulianDay operator +(JulianDay a, System.TimeSpan b)
      => Add(a, b);
    public static JulianDay operator -(JulianDay a, double b)
      => Subtract(a, b);

    public static bool operator <(JulianDay a, JulianDay b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(JulianDay a, JulianDay b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(JulianDay a, JulianDay b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(JulianDay a, JulianDay b)
      => a.CompareTo(b) <= 0;
    public static bool operator ==(JulianDay a, JulianDay b)
      => a.Equals(b);
    public static bool operator !=(JulianDay a, JulianDay b)
      => !a.Equals(b);

    #endregion Static members

    // IComparable
    public int CompareTo(JulianDay other)
      => m_value < other.m_value ? -1 : m_value > other.m_value ? 1 : 0;

    // IEquatable
    public bool Equals(JulianDay other)
      => m_value == other.m_value;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is JulianDay o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string? ToString()
      => $"<{nameof(JulianDay)} {m_value}>";
  }
}
