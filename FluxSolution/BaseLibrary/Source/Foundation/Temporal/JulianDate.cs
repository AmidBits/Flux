namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static JulianDate ToJulianDate(this System.DateTime source, ConversionCalendar calendar)
      => source.ToMomentUtc().ToJulianDate(calendar);
  }

  /// <summary>Julian Date unit of days with time of day fraction.</summary>
  /// <remarks>Julian Day Number and Julian Date for this code, has nothing to do with the Julian Calendar. Functionality that compute on the Julian Calendar will have JulianCalendar in the name.</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public struct JulianDate
    : System.IComparable<JulianDate>, System.IEquatable<JulianDate>, Quantity.IValuedUnit
  {
    public static JulianDate FirstGregorianCalendarDate
       => new MomentUtc(1582, 10, 15, 0, 0, 0).ToJulianDate(ConversionCalendar.GregorianCalendar);
    public static JulianDate FirstJulianCalendarDate
      => new JulianDate(0);
    public static JulianDate LastJulianCalendarDate
      => new MomentUtc(1582, 10, 4, 23, 59, 59).ToJulianDate(ConversionCalendar.JulianCalendar);

    public static readonly JulianDate Zero;

    private readonly double m_value;

    /// <summary>Create a Julian Date (JD) from the specified <paramref name="value"/> value.</summary>
    public JulianDate(double value)
      => m_value = value;

    public JulianDate AddWeeks(int weeks)
      => new JulianDate(m_value + weeks * 7);
    public JulianDate AddDays(int days)
      => new JulianDate(m_value + days);
    public JulianDate AddHours(int hours)
      => new JulianDate(m_value + hours / 24.0);
    public JulianDate AddMinutes(int minutes)
      => new JulianDate(m_value + minutes / 1440.0);
    public JulianDate AddSeconds(int seconds)
      => new JulianDate(m_value + seconds / 86400.0);
    public JulianDate AddMilliseconds(int milliseconds)
      => new JulianDate(m_value + (milliseconds / 1000.0) / 86400);

    public System.DayOfWeek DayOfWeek
      => (System.DayOfWeek)(ComputeDayOfWeekIso((int)(m_value + 0.5)) % 7);

    /// <summary>Returns whether the Julian Date value (JD) is considered to be on the Gregorian Calendar.</summary>
    public bool IsGregorianCalendar
      => m_value >= 2299160.5;

    public double Value
      => m_value;

    public MomentUtc ToMomentUtc(ConversionCalendar calendar)
    {
      ComputeDateComponents((int)(m_value + 0.5), calendar, out var year, out var month, out var day);
      ComputeTimeComponents(m_value, out var hour, out var minute, out var second, out var millisecond);

      return new MomentUtc(year, month, day, hour, minute, second, millisecond);
    }

    public string ToDateString(ConversionCalendar calendar)
    {
      var sb = new System.Text.StringBuilder();

      if (calendar == ConversionCalendar.JulianCalendar)
        sb.Append($"{DayOfWeek}, ");

      ComputeDateComponents((int)(m_value + 0.5), calendar, out var year, out var month, out var day); // Add 0.5 to the julian date value for date strings, because of the 12 noon convention in a Julian Date.

      sb.Append($"{System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {day}, {year}");

      if (year <= 0)
        sb.Append($" ({System.Math.Abs(year) + 1} BC)");

      return sb.ToString();
    }
    /// <summary>Returns the time string of the time-of-day portion of the Julian Date.</summary>
    public string ToTimeString()
      => System.TimeSpan.FromSeconds(43200 + ComputeTimeOfDay(m_value)).ToString(@"hh\:mm\:ss"); // Add 12 hours (in seconds) to the julian date time-of-day value for time strings, because of the 12 noon day cut-over convention in Julian Date values.

    #region Static methods
    /// <summary>Create a new MomentUtc from the specified Julian Day Number and conversion calendar.</summary>
    public static void ComputeDateComponents(int julianDayNumber, ConversionCalendar calendar, out int year, out int month, out int day)
    {
      if (julianDayNumber < -1401) throw new System.ArgumentOutOfRangeException(nameof(julianDayNumber), $"The algorithm can only convert Julian Date values greater than or equal to -1401.");

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
      if (calendar == ConversionCalendar.GregorianCalendar)
        f += (((4 * J + B) / 146097) * 3) / 4 + C;
      var e = r * f + v;
      var g = (e % p) / r;
      var h = u * g + w;

      day = (h % s) / u + 1;
      month = ((h / s + m) % n) + 1;
      year = (e / p) - y + (n + m - month) / n;
    }
    /// <summary>Returns a day of week [1 (Monday), 7 (Sunday)] from the specified Julian Day Number. Julian Day Number 0 was Monday. For US day-of-week numbering, simply do "ComputeDayOfWeekIso(JDN) % 7".</summary>
    public static int ComputeDayOfWeekIso(int julianDayNumber)
      => (julianDayNumber % 7 is var dow && dow <= 0 ? dow + 7 : dow) + 1;
    /// <summary>Computes the Julian Period (JP) from the specified cyclic indices in the year.</summary>
    /// <param name="solarCycle">That year's position in the 28-year solar cycle.</param>
    /// <param name="lunarCycle">That year's position in the 19-year lunar cycle.</param>
    /// <param name="indictionCycle">That year's position in the 15-year indiction cycle.</param>
    public static double ComputeJulianPeriod(int solarCycle, int lunarCycle, int indictionCycle)
      => (indictionCycle * 6916 + lunarCycle * 4200 + solarCycle * 4845) % (15 * 19 * 28) is var year && year > 4713 ? year - 4713 : year < 4714 ? -(4714 - year) : year;
    /// <summary>This method is only concerned with the time portion of the Julian Date.</summary>
    public static void ComputeTimeComponents(double julianDate, out int hour, out int minute, out int second, out int millisecond)
    {
      var totalSeconds = ComputeTimeOfDay(julianDate);

      if (totalSeconds <= 43200)
        totalSeconds = (totalSeconds + 43200) % 86400;

      hour = (int)(totalSeconds / 3600);
      totalSeconds -= hour * 3600;

      minute = (int)(totalSeconds / 60);
      totalSeconds -= minute * 60;

      second = (int)totalSeconds;
      totalSeconds -= second;

      millisecond = (int)totalSeconds;
    }
    /// <summary>Compute the time-of-day. I.e. the number of seconds from 12 noon of the Julian Day Number part.</summary>
    public static double ComputeTimeOfDay(double julianDate)
      => julianDate % 1 * 86400;
    #endregion Static methods

    #region Overloaded operators
    public static bool operator <(JulianDate a, JulianDate b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(JulianDate a, JulianDate b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(JulianDate a, JulianDate b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(JulianDate a, JulianDate b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(JulianDate a, JulianDate b)
      => a.Equals(b);
    public static bool operator !=(JulianDate a, JulianDate b)
      => !a.Equals(b);

    public static JulianDate operator -(JulianDate jd)
      => new JulianDate(-jd.m_value);
    public static double operator -(JulianDate a, JulianDate b)
      => a.m_value - b.m_value;

    public static JulianDate operator +(JulianDate a, double b)
      => new JulianDate(a.m_value + b);
    public static JulianDate operator /(JulianDate a, double b)
      => new JulianDate(a.m_value / b);
    public static JulianDate operator *(JulianDate a, double b)
      => new JulianDate(a.m_value * b);
    public static JulianDate operator %(JulianDate a, double b)
      => new JulianDate(a.m_value % b);
    public static JulianDate operator -(JulianDate a, double b)
      => new JulianDate(a.m_value - b);

    public static JulianDate operator +(JulianDate a, Quantity.Time b)
      => a + (b.Value / 86400);
    public static JulianDate operator -(JulianDate a, Quantity.Time b)
      => a - (b.Value / 86400);

    public static JulianDate operator +(JulianDate a, System.TimeSpan b)
      => a + (b.TotalSeconds / 86400);
    public static JulianDate operator -(JulianDate a, System.TimeSpan b)
      => a - (b.TotalSeconds / 86400);
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
      => $"<{GetType().Name}: {m_value} ({(IsGregorianCalendar ? ToDateString(ConversionCalendar.GregorianCalendar) : ToDateString(ConversionCalendar.JulianCalendar))}, {ToTimeString()})>";
    #endregion Object overrides
  }
}
