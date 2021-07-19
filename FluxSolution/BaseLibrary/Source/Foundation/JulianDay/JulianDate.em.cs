namespace Flux
{
  public static partial class ExtensionMethods
  {
    //public static JulianDate ToJulianDate(this System.DateTime source)
    //  => JulianDate.From(source.Year, source.Month, source.Day);
    //public static JulianDate ToJulianDate(this System.DateTime source)
    //{
    //  var julianDayNumber = JulianDayNumber.IsProlepticGregorianCalendarDate(source.Year, source.Month, source.Day)
    //  ? JulianDayNumber.ConvertJulianCalendarToJulianDayNumber(source.Year, source.Month, source.Day)
    //  : JulianDayNumber.ConvertGregorianCalendarToJulianDayNumber(source.Year, source.Month, source.Day);

    //  var julianDateFraction = JulianDate.ConvertTimeToJulianDateFraction(new Units.Time(source.TimeOfDay.TotalSeconds));

    //  return new JulianDate(julianDayNumber + julianDateFraction);
    //}
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

    /// <summary>Create a Julian Date (JD) from the specified <paramref name="julianDate"/>.</summary>
    public JulianDate(double julianDate)
      => m_value = julianDate;

    public bool IsEmpty
      => Equals(Empty);

    /// <summary>The numerical value of the Julian Date Number (JDN).</summary>
    public int JulianDayNumber
      => (int)m_value;

    public System.TimeSpan Time
      => System.TimeSpan.FromSeconds((m_value % 1 is var f && f >= 0.5 ? f - 0.5 : f + 0.5) * 86400);

    public MomentUtc ToMomentUtc(ConversionCalendar calendar)
    {
      ComputeDateComponents((int)m_value, calendar, out var year, out var month, out var day);
      ComputeTimeComponents(m_value % 1, out var hour, out var minute, out var second, out var millisecond);

      return new MomentUtc(year, month, day, hour, minute, second, millisecond);
    }

    public double Value
      => m_value;

    public JulianDate AddWeeks(int weeks)
      => AddDays(weeks * 7);
    public JulianDate AddDays(int days)
      => new JulianDate(m_value + days);
    public JulianDate AddHours(int hours)
      => new JulianDate(m_value + hours / HoursPerDay);
    public JulianDate AddMinutes(int minutes)
      => new JulianDate(m_value + minutes / MinutesPerDay);
    public JulianDate AddSeconds(int seconds)
      => new JulianDate(m_value + seconds / SecondsPerDay);

    public string ToDateString(ConversionCalendar calendar, bool includeHistoricalYearLabel = false)
    {
      var sb = new System.Text.StringBuilder();

      if (calendar == ConversionCalendar.JulianCalendar)
        sb.Append($"{ComputeDayOfWeek((int)m_value, out _)}, ");

      ComputeDateComponents((int)m_value, calendar, out var year, out var month, out var day);

      sb.Append($"{System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {day}, {year}");

      if (includeHistoricalYearLabel)
        sb.Append($", a.k.a. {(year <= 0 ? $"{System.Math.Abs(year) + 1} BC" : $"{year} AD")}");

      sb.Append($" ({calendar.ToString()})");

      return sb.ToString();
    }
    public string ToTimeString()
      => System.TimeSpan.FromSeconds(ConvertJulianDateFractionToTime(m_value).Second).ToString(@"hh\:mm\:ss");

    #region Static methods
    /// <summary>Create a new MomentUtc from the specified Julian Day Number and conversion calendar.</summary>
    public static void ComputeDateComponents(int julianDayNumber, ConversionCalendar calendar, out int year, out int month, out int day)
    {
      if (julianDayNumber < 0) throw new System.ArgumentOutOfRangeException(nameof(julianDayNumber));

      var J = julianDayNumber;

      const int y = 4716;
      const int v = 3;
      const int j = 1401;
      const int u = 5;
      const int m = 2;
      const int s = 153;
      const int n = 12;
      const int w = 2;
      const int r = 4;
      const int B = 274277;
      const int p = 1461;
      const int C = -38;

      var f = J + j;
      if (calendar == ConversionCalendar.GregorianCalendar) f += (((4 * J + B) / 146097) * 3) / 4 + C;
      var e = r * f + v;
      var g = (e % p) / r;
      var h = u * g + w;

      day = (h % s) / u + 1;
      month = ((h / s + m) % n) + 1;
      year = (e / p) - y + (n + m - month) / n;
    }
    /// <summary>This method is only concerned with the time portion of the Julian Date.</summary>
    public static void ComputeTimeComponents(double julianDate, out int hour, out int minute, out int second, out int millisecond)
    {
      double f = julianDate % 1 * 86400;

      hour = (int)System.Math.Round(f / 3600);
      f -= hour * 3600;

      minute = (int)System.Math.Round(f / 60);
      f -= minute * 60;

      second = (int)System.Math.Round(f);
      f -= second;

      millisecond = (int)System.Math.Round(f);
    }
    /// <summary>Returns a day of week [0 (Monday), 6 (Sunday)] from the specified Julian Day Number. Julian Day Number 0 was Monday.</summary>
    /// <returns>0=Monday, 1=Tuesday, 2=Wednesday, 3=Thursday, 4=Friday, 5=Saturday, 6=Sunday.</returns>
    public static System.DayOfWeek ComputeDayOfWeek(int julianDayNumber, out int dayOfWeek)
      => (System.DayOfWeek)(((dayOfWeek = julianDayNumber % 7) + 1) % 7);

    /// <summary>Returns the time from the specified Julian Date (fraction).</summary>
    public static Units.Time ConvertJulianDateFractionToTime(double julianDate)
      => new Units.Time((julianDate % 1) * 86400);
    /// <summary>Returns the time fraction from the specified unit time.</summary>
    public static double ConvertTimeToJulianDateFraction(Units.Time time)
     => time.Second / 86400 is var f && f >= 0.5 ? f - 0.5 : f + 0.5;

    public static double ConvertToModifiedJulianDate(double julianDate)
      => julianDate - 2400000.5;
    public static double ConvertToReducedJulianDate(double julianDate)
      => julianDate - 2400000;
    public static double ConvertToTruncatedJulianDate(double julianDate)
      => System.Math.Floor(julianDate - 2440000.5);

    /// <summary>Returns whether the Julian Date value is considered in the proleptic Gregorian Calendar.</summary>
    public static bool IsProlepticGregorianCalendar(double julianDate)
      => julianDate < 2299160.5;
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
      => $"<{GetType().Name} {m_value} ({ToDateString(ConversionCalendar.GregorianCalendar, true)} {ToTimeString()})>";
    #endregion Object overrides
  }
}
