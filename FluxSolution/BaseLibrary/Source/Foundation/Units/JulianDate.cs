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
    : System.IComparable<JulianDate>, System.IEquatable<JulianDate>, Units.IValuedUnit
  {
    public static JulianDate FirstGregorianDate
      => new MomentUtc(1582, 10, 15, 12, 0, 0).ToJulianDate(ConversionCalendar.GregorianCalendar);
    public static JulianDate LastJulianDate
      => new MomentUtc(1582, 10, 4, 12, 0, 0).ToJulianDate(ConversionCalendar.JulianCalendar);
    public static JulianDate FirstJulianDate
      => new MomentUtc(-4712, 1, 1, 12, 0, 0).ToJulianDate(ConversionCalendar.JulianCalendar);

    public static readonly JulianDate Zero;

    private readonly double m_value;

    /// <summary>Create a Julian Date (JD) from the specified <paramref name="julianDate"/>.</summary>
    public JulianDate(double julianDate)
      => m_value = julianDate;

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
    public JulianDate AddMillieconds(int milliseconds)
      => new JulianDate(m_value + (milliseconds / 1000.0) / 86400);

    public System.DayOfWeek DayOfWeek
      => (System.DayOfWeek)((ComputeDayOfWeek((int)m_value) + 1) % 7);

    /// <summary>Returns whether the Julian Date value (JD) is considered to be on the Gregorian Calendar.</summary>
    public bool IsGregorianCalendar
      => m_value > 2299160.5;

    public Units.Time TimeOfDay
      => new Units.Time(m_value % 1 * 86400);

    public double Value
      => m_value;

    public MomentUtc ToMomentUtc(ConversionCalendar calendar)
    {
      ComputeDateComponents((int)m_value, calendar, out var year, out var month, out var day);
      ComputeTimeComponents(m_value % 1, out var hour, out var minute, out var second, out var millisecond);

      return new MomentUtc(year, month, day, hour, minute, second, millisecond);
    }

    public string ToDateString(ConversionCalendar calendar)
    {
      var sb = new System.Text.StringBuilder();

      if (calendar == ConversionCalendar.JulianCalendar)
        sb.Append($"{DayOfWeek}, ");

      ComputeDateComponents((int)m_value, calendar, out var year, out var month, out var day);

      sb.Append($"{System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {day}, {year}");

      if (year <= 0)
        sb.Append($" ({System.Math.Abs(year) + 1} BC)");

      return sb.ToString();
    }
    public string ToTimeString()
      => System.TimeSpan.FromSeconds(TimeOfDay.Value).ToString(@"hh\:mm\:ss");

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
      if (calendar == ConversionCalendar.GregorianCalendar)
        f += (((4 * J + B) / 146097) * 3) / 4 + C;
      var e = r * f + v;
      var g = (e % p) / r;
      var h = u * g + w;

      day = (h % s) / u + 1;
      month = ((h / s + m) % n) + 1;
      year = (e / p) - y + (n + m - month) / n;
    }
    /// <summary>Returns a day of week [0 (Monday), 6 (Sunday)] from the specified Julian Day Number. Julian Day Number 0 was Monday.</summary>
    public static int ComputeDayOfWeek(int julianDayNumber)
      => julianDayNumber % 7;
    /// <summary>Computes the Julian Period (JP) from the specified cyclic indices in the year.</summary>
    /// <param name="solarCycle">That year's position in the 28-year solar cycle.</param>
    /// <param name="lunarCycle">That year's position in the 19-year lunar cycle.</param>
    /// <param name="indictionCycle">That year's position in the 15-year indiction cycle.</param>
    public static double ComputeJulianPeriod(int solarCycle, int lunarCycle, int indictionCycle)
      => (indictionCycle * 6916 + lunarCycle * 4200 + solarCycle * 4845) % (15 * 19 * 28) is var year && year > 4713 ? year - 4713 : year < 4714 ? -(4714 - year) : year;
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
    #endregion Static methods

    #region Overloaded operators
    public static JulianDate operator +(JulianDate a, double b)
      => new JulianDate(a.m_value + b);
    public static JulianDate operator +(JulianDate a, Units.Time b)
      => a + (b.Value / 86400.0);
    public static JulianDate operator +(JulianDate a, System.TimeSpan b)
      => a.AddDays(b.Days).AddHours(b.Hours).AddMinutes(b.Minutes).AddSeconds(b.Seconds).AddMillieconds(b.Milliseconds);
    public static JulianDate operator -(JulianDate a, double b)
      => new JulianDate(a.m_value - b);
    public static JulianDate operator -(JulianDate a, Units.Time b)
      => a - (b.Value / 86400.0);
    public static JulianDate operator -(JulianDate a, System.TimeSpan b)
      => a.AddDays(-b.Days).AddHours(-b.Hours).AddMinutes(-b.Minutes).AddSeconds(-b.Seconds).AddMillieconds(-b.Milliseconds);

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
      => $"<{GetType().Name}: {m_value} ({(IsGregorianCalendar ? ToDateString(ConversionCalendar.GregorianCalendar) : ToDateString(ConversionCalendar.JulianCalendar))}, {ToTimeString()})>";
    #endregion Object overrides
  }
}
