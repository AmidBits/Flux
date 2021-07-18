namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static bool IsProlepticGregorianCalendar(this System.DateTime source)
      => JulianDate.IsProlepticGregorianCalendarDate(source.Year, source.Month, source.Day);

    //public static JulianDate ToJulianDate(this System.DateTime source)
    //  => JulianDate.From(source.Year, source.Month, source.Day);
    public static JulianDate ToJulianDate(this System.DateTime source)
    {
      var julianDayNumber = JulianDate.IsProlepticGregorianCalendarDate(source.Year, source.Month, source.Day)
      ? JulianDate.ConvertJulianCalendarToJulianDayNumber(source.Year, source.Month, source.Day)
      : JulianDate.ConvertGregorianCalendarToJulianDayNumber(source.Year, source.Month, source.Day);

      var julianDateFraction = JulianDate.ConvertTimeToJulianDateFraction(new Units.Time(source.TimeOfDay.TotalSeconds));

      return new JulianDate(julianDayNumber + julianDateFraction);
    }
  }

  /// <summary>Julian Day struct</summary>
  /// <remarks>The Julian Day, Julian Day Number and Julian Date for this code, has nothing to do with the Julian Calendar. Functionality that compute on the Julian Calendar will have JulianCalendar in the name.</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public struct JulianDate
    : System.IComparable<JulianDate>, System.IEquatable<JulianDate>
  {
    public const double CalendarCutover = 2299160.5;

    public System.DateTime GregorianCalendarStartDate
      => new System.DateTime(1582, 10, 15);

    public System.DateTime JulianCalendarEndDate
      => new System.DateTime(1582, 10, 4);

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

    /// <summary>Returns whether the Julian Date is considered in the proleptic Gregorian Calendar, i.e. before Friday, October 15, 1582.</summary>
    public bool IsProlepticGregorianCalendar
      => m_value < 2299160.5;

    public int JulianDayNumber
      => (int)System.Math.Floor(m_value);

    public System.TimeSpan Time
      => System.TimeSpan.FromSeconds((m_value % 1 is var f && f >= 0.5 ? f - 0.5 : f + 0.5) * 86400);

    public double ModifiedJD
      => m_value - 2400000.5;
    public double ReducedJD
      => m_value - 2400000;
    public double TruncatedJD
      => System.Math.Floor(m_value - 2440000.5);

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
      => (System.DayOfWeek)((DayOfWeek((int)m_value) + 1) % 7);

    public int GetAstronomicalYearNumber()
      => IsProlepticGregorianCalendar
      ? ConvertJulianDayNumberToJulianCalendar(JulianDayNumber).year
      : ConvertJulianDayNumberToGregorianCalendar(JulianDayNumber).year;

    public string ToDateString()
      => IsProlepticGregorianCalendar
      ? ToJulianCalendarDateString()
      : ToGregorianCalendarDateString();

    public string ToGregorianCalendarDateString(bool astronomicalYearNumber = false)
      => ConvertJulianDayNumberToGregorianCalendar(JulianDayNumber) is var ymd && astronomicalYearNumber
      ? $"{System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ymd.month)} {ymd.day}, {GetAstronomicalYearNumber()}"
      : $"{System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ymd.month)} {ymd.day}, {(ymd.year <= 0 ? System.Math.Abs(ymd.year) + 1 : ymd.year)} {(ymd.year > 0 ? "AD" : "BC")} ({GetAstronomicalYearNumber()})";

    public string ToJulianCalendarDateString()
    {
      var ymd = ConvertJulianDayNumberToJulianCalendar(JulianDayNumber);

      return $"{GetDayOfWeek()}, {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ymd.month)} {ymd.day}, {GetAstronomicalYearNumber()} ({(ymd.year <= 0 ? $"{System.Math.Abs(ymd.year) + 1} BC" : $"{ymd.year} AD")})";
    }

    public string ToTimeString()
      => System.TimeSpan.FromSeconds(ConvertJulianDateFractionToTime(m_value).Second).ToString(@"hh\:mm\:ss");

    #region Static methods
    /// <summary>returns the Julian Day Number (JDN) from the specified year, month and day from the Gregorian Calendar. The algorithm is valid for all (possibly proleptic) Gregorian calendar dates after November 23, -4713 (per Wikipedia).</summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <returns>The Julian day number corresponding to the specified Gregorian calendar date.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Proleptic_Gregorian_calendar"/>
    public static int ConvertGregorianCalendarToJulianDayNumber(int year, int month, int day)
      => ((1461 * (year + 4800 + (month - 14) / 12)) / 4 + (367 * (month - 2 - 12 * ((month - 14) / 12))) / 12 - (3 * ((year + 4900 + (month - 14) / 12) / 100)) / 4 + day - 32075);
    /// <summary>returns the Julian Day Number (JDN) from the specified year, month and day from the Julian Calendar.</summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <returns>The Julian day number corresponding to the specified Julian calendar date.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
    /// <see cref="https://en.wikipedia.org/wiki/Proleptic_Julian_calendar"/>
    public static int ConvertJulianCalendarToJulianDayNumber(int year, int month, int day)
      => (367 * year - (7 * (year + 5001 + (month - 9) / 7)) / 4 + (275 * month) / 9 + day + 1729777);

    public static double ConvertJulianDateToModified(double julianDate)
      => julianDate - 2400000.5;
    public static double ConvertJulianDateToReduced(double julianDate)
      => julianDate - 2400000;
    public static double ConvertJulianDateToTruncated(double julianDate)
      => System.Math.Floor(julianDate - 2440000.5);

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

    /// <summary>Returns a day of week value which spans 0 (Monday) through 6 (Sunday) from the specified Julian Day Number 0 (which was a Monday noon).</summary>
    /// <returns>0=Monday, 1=Tuesday, 2=Wednesday, 3=Thursday, 4=Friday, 5=Saturday, 6=Sunday.</returns>
    public static int DayOfWeek(int julianDayNumber)
      => julianDayNumber % 7;

    public static JulianDate From(int year, int month, int day)
      => IsProlepticGregorianCalendarDate(year, month, day)
      ? new JulianDate(ConvertJulianCalendarToJulianDayNumber(year, month, day))
      : new JulianDate(ConvertGregorianCalendarToJulianDayNumber(year, month, day));

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
    public static JulianDate operator +(JulianDate a, System.TimeSpan b)
      => new JulianDate(a.m_value + b.Days + ((b.Hours - 12) / HoursPerDay) + (b.Minutes / MinutesPerDay) + (b.Seconds / SecondsPerDay));
    //public static JulianDate operator -(JulianDate a, double b)
    //  => new JulianDate(a.m_value - b);

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
