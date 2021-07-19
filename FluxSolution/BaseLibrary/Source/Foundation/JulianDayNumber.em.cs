namespace Flux
{
  /// <summary>Julian Day Number struct</summary>
  /// <remarks>The Julian Day Number.</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public struct JulianDayNumber
    : System.IComparable<JulianDayNumber>, System.IEquatable<JulianDayNumber>
  {
    public const double CalendarCutover = 2299160.5;

    public System.DateTime GregorianCalendarStartDate
      => new System.DateTime(1582, 10, 15);

    public System.DateTime JulianCalendarEndDate
      => new System.DateTime(1582, 10, 4);

    public const double HoursPerDay = 24;
    public const double MinutesPerDay = 1440;
    public const double SecondsPerDay = 86400;

    public static readonly JulianDayNumber Empty;

    private readonly int m_value;

    /// <summary>Create a Julian Date (JD) from the specified Julian Day Number (JDN).</summary>
    /// <param name="julianDayNumber"></param>
    public JulianDayNumber(int julianDayNumber)
      => m_value = julianDayNumber;

    public bool IsEmpty
      => Equals(Empty);

    /// <summary>Returns whether the Julian Date is considered in the proleptic Gregorian Calendar, i.e. before Friday, October 15, 1582.</summary>
    public bool IsProlepticGregorianCalendar
      => m_value < 2299160.5;

    public double Value
      => m_value;

    public JulianDayNumber AddDays(int days)
      => new JulianDayNumber(m_value + days);
    public JulianDayNumber AddWeeks(int weeks)
      => new JulianDayNumber(m_value + weeks * 7);

    public System.DayOfWeek DayOfWeek
      => (System.DayOfWeek)((DayOfWeek(m_value) + 1) % 7);

    public int GetAstronomicalYearNumber()
      => IsProlepticGregorianCalendar
      ? ConvertJulianDayNumberToJulianCalendar(m_value).year
      : ConvertJulianDayNumberToGregorianCalendar(m_value).year;

    public string ToDateString()
      => IsProlepticGregorianCalendar
      ? ToJulianCalendarDateString()
      : ToGregorianCalendarDateString();
    public string ToGregorianCalendarDateString(bool astronomicalYearNumber = false)
      => ConvertJulianDayNumberToGregorianCalendar(m_value) is var ymd && astronomicalYearNumber
      ? $"{System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ymd.month)} {ymd.day}, {GetAstronomicalYearNumber()}"
      : $"{System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ymd.month)} {ymd.day}, {(ymd.year <= 0 ? System.Math.Abs(ymd.year) + 1 : ymd.year)} {(ymd.year > 0 ? "AD" : "BC")} ({GetAstronomicalYearNumber()})";
    public string ToJulianCalendarDateString()
    {
      var ymd = ConvertJulianDayNumberToJulianCalendar(m_value);

      return $"{GetDayOfWeek()}, {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ymd.month)} {ymd.day}, {GetAstronomicalYearNumber()} ({(ymd.year <= 0 ? $"{System.Math.Abs(ymd.year) + 1} BC" : $"{ymd.year} AD")})";
    }

    #region Static methods
    /// <summary>Converts the specified year, month and day from the Gregorian calendar to the Julian Day Number (JDN). The algorithm is valid for all (possibly proleptic) Gregorian calendar dates after November 23, -4713 (per Wikipedia).</summary>
    /// <returns>The Julian day number corresponding to the specified Gregorian calendar date.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Proleptic_Gregorian_calendar"/>
    public static int ConvertGregorianCalendarToJulianDayNumber(int year, int month, int day)
      => ((1461 * (year + 4800 + (month - 14) / 12)) / 4 + (367 * (month - 2 - 12 * ((month - 14) / 12))) / 12 - (3 * ((year + 4900 + (month - 14) / 12) / 100)) / 4 + day - 32075);
    /// <summary>Converts the specified year, month and day from the Julian calendar to the Julian Day Number (JDN).</summary>
    /// <returns>The Julian day number corresponding to the specified Julian calendar date.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
    /// <see cref="https://en.wikipedia.org/wiki/Proleptic_Julian_calendar"/>
    public static int ConvertJulianCalendarToJulianDayNumber(int year, int month, int day)
      => (367 * year - (7 * (year + 5001 + (month - 9) / 7)) / 4 + (275 * month) / 9 + day + 1729777);

    /// <summary>Returns the proleptic Gregorian date corresponding to the specified Julian Day Number (JDN).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Julian_day#Julian_or_Gregorian_calendar_from_Julian_day_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Proleptic_Gregorian_calendar"/>
    public static (int year, int month, int day) ConvertJulianDayNumberToGregorianCalendar(int julianDayNumber)
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
    /// <summary>Returns the proleptic Julian date corresponding to the specified Julian Day Number (JDN).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Julian_day#Julian_or_Gregorian_calendar_from_Julian_day_number"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Proleptic_Gregorian_calendar"/>
    public static (int year, int month, int day) ConvertJulianDayNumberToJulianCalendar(int julianDayNumber)
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

    /// <summary>Returns the time from the specified Julian Date (fraction).</summary>
    public static Units.Time ConvertJulianDateFractionToTime(double julianDate)
      => new Units.Time((julianDate % 1 is var f && f >= 0.5 ? f - 0.5 : f + 0.5) * SecondsPerDay);
    /// <summary>Returns the time fraction from the specified unit time.</summary>
    public static double ConvertTimeToJulianDateFraction(Units.Time time)
     => time.Second / 86400 is var f && f >= 0.5 ? f - 0.5 : f + 0.5;

    public static double ConvertToModifiedJulianDate(double julianDate)
      => julianDate - 2400000.5;
    public static double ConvertToReducedJulianDate(double julianDate)
      => julianDate - 2400000;
    public static double ConvertToTruncatedJulianDate(double julianDate)
      => System.Math.Floor(julianDate - 2440000.5);

    /// <summary>Returns a day of week value which spans 0 (Monday) through 6 (Sunday) from the specified Julian Day Number 0 (which was a Monday noon).</summary>
    /// <returns>0=Monday, 1=Tuesday, 2=Wednesday, 3=Thursday, 4=Friday, 5=Saturday, 6=Sunday.</returns>
    public static int DayOfWeek(int julianDayNumber)
      => julianDayNumber % 7;

    public static JulianDayNumber FromArbitraryCalendar(int year, int month, int day)
      => IsProlepticGregorianCalendarDate(year, month, day)
      ? FromJulianCalendar(year, month, day)
      : FromGregorianCalendar(year, month, day);
    public static JulianDayNumber FromGregorianCalendar(int year, int month, int day)
      => new JulianDayNumber(ConvertGregorianCalendarToJulianDayNumber(year, month, day) - 0.5);
    public static JulianDayNumber FromJulianCalendar(int year, int month, int day)
      => new JulianDayNumber(ConvertJulianCalendarToJulianDayNumber(year, month, day) - 0.5);

    /// <summary>Returns whether the date is in the proleptic Gregorian calendar, i.e. before Friday, October 15, 1582.</summary>
    public static bool IsProlepticGregorianCalendarDate(int year, int month, int day)
      => year < 1582 || (year == 1582 && (month < 10 || (month == 10 && day <= 4)));
    /// <summary>Returns whether the specified year, month and day is a date in either the Gregorian or Julian calendar.</summary>
    public static bool IsValidDate(int year, int month, int day)
      => !(year == 1582 && month == 10 && day >= 5 && day <= 14); // Excludes the 10 days that were jumped in the transition from the Julian Calendar to the Gregorian Calendar.
    #endregion Static methods

    #region Overloaded operators
    //public static JulianDate operator +(JulianDate a, double b)
    //  => new JulianDate(a.m_value + b);
    public static JulianDayNumber operator +(JulianDayNumber a, System.TimeSpan b)
      => new JulianDayNumber(a.m_value + b.Days + ((b.Hours - 12) / HoursPerDay) + (b.Minutes / MinutesPerDay) + (b.Seconds / SecondsPerDay));
    //public static JulianDate operator -(JulianDate a, double b)
    //  => new JulianDate(a.m_value - b);

    public static bool operator <(JulianDayNumber a, JulianDayNumber b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(JulianDayNumber a, JulianDayNumber b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(JulianDayNumber a, JulianDayNumber b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(JulianDayNumber a, JulianDayNumber b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(JulianDayNumber a, JulianDayNumber b)
      => a.Equals(b);
    public static bool operator !=(JulianDayNumber a, JulianDayNumber b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(JulianDayNumber other)
      => m_value < other.m_value ? -1 : m_value > other.m_value ? 1 : 0;

    // IEquatable
    public bool Equals(JulianDayNumber other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is JulianDayNumber o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string? ToString()
      => $"<{GetType().Name} {m_value} ({ToDateString()})>";
    #endregion Object overrides
  }
}
