namespace Flux
{
  /// <summary>Julian Day struct</summary>
  /// <remarks>The Julian Day, Julian Day Number and Julian Date for this code, has nothing to do with the Julian Calendar. Functionality that compute on the Julian Calendar will have JulianCalendar in the name.</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public struct JulianDate
    : System.IComparable<JulianDate>, System.IEquatable<JulianDate>
  {
    public const double HoursPerDay = 24;
    public const double MinutesPerDay = 1440;
    public const double SecondsPerDay = 86400;

    public static readonly JulianDate Empty;

    private readonly double m_value;

    /// <summary>Create a Julian Date (JD) from the specified Julian Day Number (JDN).</summary>
    /// <param name="julianDayNumber"></param>
    public JulianDate(int julianDayNumber)
      => m_value = julianDayNumber;
    /// <summary>Create a Julian Date (JD) from the specified Julian Day Number (JDN) with optional fractions (time of day).</summary>
    public JulianDate(double julianDate)
      => m_value = julianDate;
    /// <summary>Create a Julian Date (JD) from the specified Julian Day Number (JDN), hour, minute and second.</summary>
    public JulianDate(int julianDayNumber, int hour, int minute, int second)
      => m_value = julianDayNumber + (hour - 12) / 24.0 + minute / 1440.0 + second / 86400.0;

    public bool IsEmpty
      => Equals(Empty);

    public int JulianDayNumber
      => (int)m_value;

    public double Value
      => m_value;

    public JulianDate AddDays(int days)
      => new JulianDate(m_value + days);
    public JulianDate AddHours(int hours)
      => new JulianDate(m_value + hours / HoursPerDay);
    public JulianDate AddMinutes(int minutes)
      => new JulianDate(m_value + minutes / MinutesPerDay);
    public JulianDate AddSeconds(int seconds)
      => new JulianDate(m_value + seconds / SecondsPerDay);

    public System.DayOfWeek GetDayOfWeek()
      => (System.DayOfWeek)((GetDayOfWeek((int)m_value) + 1) % 7);

    public int GetAstronomicalYearNumber()
      => IsProlepticJulianCalendar(m_value)
      ? ToProlepticJulianCalendarDate(JulianDayNumber).year
      : ToProlepticGregorianCalendarDate(JulianDayNumber).year;

    public string ToDateString()
    {
      if (IsProlepticJulianCalendar(m_value))
      {
        var (year, month, day) = ToProlepticJulianCalendarDate(JulianDayNumber);

        return $"{GetDayOfWeek()}, {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {day}, {GetAstronomicalYearNumber()} ({(year < 1 ? $"{System.Math.Abs(year) + 1} BC" : $"{year} AD")})";
      }
      else
      {
        var (year, month, day) = ToProlepticGregorianCalendarDate(JulianDayNumber);

        return $"{System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {day}, {GetAstronomicalYearNumber()}";
      }
    }

    public string ToGregorianCalendarDateString()
    {
      var (year, month, day) = ToProlepticGregorianCalendarDate(JulianDayNumber);

      return $"{System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {day}, {(year <= 0 ? System.Math.Abs(year) + 1 : year)} {(year > 0 ? "AD" : "BC")}";
    }

    public string ToJulianCalendarDateString()
    {
      var (year, month, day) = ToProlepticJulianCalendarDate(JulianDayNumber);

      return $"{GetDayOfWeek()}, {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {day}, {(year <= 0 ? System.Math.Abs(year) + 1 : year)} {(year > 0 ? "AD" : "BC")}";
    }

    public string ToTimeString()
    {
      var (hour, minute, second) = GetTimePart(m_value);

      return $"{hour:D2}:{minute:D2}:{second:D2}";
    }

    #region Static methods
    /// <summary>returns the Julian Day Number (JDN) from the specified year, month and day from the Gregorian Calendar. The algorithm is valid for all (possibly proleptic) Gregorian calendar dates after November 23, -4713 (per Wikipedia).</summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <returns>The Julian day number corresponding to the specified Gregorian calendar date.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Proleptic_Gregorian_calendar"/>
    public static JulianDate FromGregorianCalendarDate(int year, int month, int day)
      => new JulianDate((1461 * (year + 4800 + (month - 14) / 12)) / 4 + (367 * (month - 2 - 12 * ((month - 14) / 12))) / 12 - (3 * ((year + 4900 + (month - 14) / 12) / 100)) / 4 + day - 32075);
    //{
    //  var a = (14 - month) / 12;
    //  var y = year + 4800 - a;
    //  var m = month + 12 * a - 3;

    //  return new JulianDate(day + (153 * m + 2) / 5 + 365 * y + y / 4 - y / 100 + y / 400 - 32045);
    //}
    /// <summary>returns the Julian Day Number (JDN) from the specified year, month and day from the Julian Calendar.</summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <returns>The Julian day number corresponding to the specified Julian calendar date.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
    /// <see cref="https://en.wikipedia.org/wiki/Proleptic_Julian_calendar"/>
    public static JulianDate FromJulianCalendarDate(int year, int month, int day)
      => new JulianDate(367 * year - (7 * (year + 5001 + (month - 9) / 7)) / 4 + (275 * month) / 9 + day + 1729777);
    //public static double FromGregorianCalendarDate(int year, int month, double day)
    //{
    //  if (month == 1 || month == 2)
    //  {
    //    year -= 1;
    //    month += 12;
    //  }

    //  var A = year / 100;
    //  var B = 2 - A + (A / 4);

    //  return (int)(365.25 * (year + 4716)) + (int)(30.6001 * (month + 1)) + day + B - 1524.5;
    //}

    /// <summary>Returns a day of week value which spans 0 (Monday) through 6 (Sunday) from the specified Julian Day Number 0 (which was a Monday noon).</summary>
    /// <returns>0=Monday, 1=Tuesday, 2=Wednesday, 3=Thursday, 4=Friday, 5=Saturday, 6=Sunday.</returns>
    public static int GetDayOfWeek(int julianDayNumber)
      => julianDayNumber % 7;
    /// <summary>Returns the time part from the specified Julian Date.</summary>
    public static (int hour, int minute, int second) GetTimePart(double julianDate)
    {
      double time = julianDate % 1;

      time = (time >= 0.5 ? time - 0.5 : time + 0.5);
      time *= 86400;

      var hour = (int)System.Math.Round(time / 3600);
      time -= (hour * 3600);

      var minute = (int)System.Math.Round(time / 60);
      time -= (minute * 60);

      var second = (int)System.Math.Round(time);

      return (hour, minute, second);
    }

    /// <summary>Returns whether the Julian Day is considered in the Gregorian Calendar (non-proleptic), i.e. on or after Friday, October 15, 1582.</summary>
    public static bool IsNonProlepticGregorianCalendar(double julianDate)
      => julianDate >= 2299160.5;
    /// <summary>Returns whether the Julian Day is considered in the Gregorian Calendar (non-proleptic), i.e. on or after Friday, October 15, 1582.</summary>
    public static bool IsNonProlepticGregorianCalendar(int year, int month, int day)
      => year > 1582 || (year == 1582 && (month > 10 || (month == 10 && day >= 15)));

    /// <summary>Returns whether the Julian Day is considered in the (proleptic) Julian Calendar, i.e. on or before Thursday, October 4, 1582.</summary>
    public static bool IsProlepticJulianCalendar(double julianDate)
      => julianDate < 2299160.5;
    /// <summary>Returns whether the Julian Day is considered in the (proleptic) Julian Calendar, i.e. on or before Thursday, October 4, 1582.</summary>
    public static bool IsProlepticJulianCalendar(int year, int month, int day)
      => year < 1582 || (year == 1582 && (month < 10 || (month == 10 && day <= 4)));

    /// <summary>Returns whether the specified year, month and day is a value.</summary>
    public static bool IsValidDate(int year, int month, int day)
      => !(year == 1582 && month == 10 && day >= 5 && day <= 14); // Excludes the 10 day numbers that were jumped in the transition from the Gregorian Calendar to the Julian Calendar.

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
      //var B = 274277;
      var p = 1461;
      //var C = -38;

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
    #endregion Static methods

    #region Overloaded operators
    public static JulianDate operator +(JulianDate a, double b)
      => new JulianDate(a.m_value + b);
    public static JulianDate operator +(JulianDate a, System.TimeSpan b)
      => new JulianDate(a.m_value + b.Days + ((b.Hours - 12) / HoursPerDay) + (b.Minutes / MinutesPerDay) + (b.Seconds / SecondsPerDay));
    public static JulianDate operator -(JulianDate a, double b)
      => new JulianDate(a.m_value - b);

    public static bool operator <(JulianDate a, JulianDate b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(JulianDate a, JulianDate b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(JulianDate a, JulianDate b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(JulianDate a, JulianDate b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(JulianDate a, JulianDate b)
      => a.Equals(b);
    public static bool operator !=(JulianDate a, JulianDate b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(JulianDate other)
      => m_value < other.m_value ? -1 : m_value > other.m_value ? 1 : 0;

    // IEquatable
    public bool Equals(JulianDate other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is JulianDate o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string? ToString()
      => $"<{GetType().Name} {m_value} ({ToDateString()} {ToTimeString()})>";
    #endregion Object overrides
  }
}
