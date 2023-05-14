namespace Flux
{
  public static partial class ExtensionMethodsDateTime
  {
    public static JulianDate ToJulianDate(this System.DateTime source, TemporalCalendar calendar)
      => new(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, source.Millisecond, calendar);
  }

  /// <summary>Julian Date unit of days with time of day fraction. The Julian Date struct can also be used to handle Julian Day Fraction, which is the number of days, hours, minutes and seconds since the beginning of the year.</summary>
  /// <remarks>Julian Date is not related to the Julian Calendar. Functionality that compute on the Julian Calendar will have JulianCalendar in the name.</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public readonly record struct JulianDate
    : System.IFormattable, System.IComparable<JulianDate>, IQuantifiable<double>
  {
    private readonly double m_value;

    /// <summary>Create a Julian Date (JD) from the specified <paramref name="value"/> value.</summary>
    public JulianDate(double value)
      => m_value = value;

    /// <summary>Computes the Julian Date (JD) for the specified date/time components and calendar to use during conversion.</summary>
    public JulianDate(int year, int month, int day, int hour, int minute, int second, int millisecond, TemporalCalendar calendar)
      : this(JulianDayNumber.ConvertFromDateParts(year, month, day, calendar) + ConvertFromTimeParts(hour, minute, second, millisecond))
    { }

    public JulianDate AddWeeks(int weeks) => this + (weeks * 7);
    public JulianDate AddDays(int days) => this + days;
    public JulianDate AddHours(int hours) => this + (hours / 24d);
    public JulianDate AddMinutes(int minutes) => this + (minutes / 1440d);
    public JulianDate AddSeconds(int seconds) => this + (seconds / 86400d);
    public JulianDate AddMilliseconds(int milliseconds) => this + (milliseconds / 1000d / 86400d);

    public TemporalCalendar GetConversionCalendar()
      => IsGregorianCalendar(m_value) ? TemporalCalendar.GregorianCalendar : TemporalCalendar.JulianCalendar;

    public void GetParts(TemporalCalendar calendar, out int year, out int month, out int day, out int hour, out int minute, out int second, out int millisecond)
    {
      (year, month, day) = ToJulianDayNumber().GetDateParts(calendar);

      (hour, minute, second, millisecond) = ConvertToTimeParts(m_value);
    }

    public JulianDayNumber ToJulianDayNumber() => new((int)(m_value + 0.5));

    public MomentUtc ToMomentUtc(TemporalCalendar calendar)
    {
      var (year, month, day) = ToJulianDayNumber().GetDateParts(calendar);
      var (hour, minute, second, millisecond) = ConvertToTimeParts(m_value);

      return new(year, month, day, hour, minute, second, millisecond);
    }

    public string ToTimeString()
      => System.TimeSpan.FromSeconds(System.Convert.ToDouble(43200 + GetTimeSinceNoon(m_value))).ToString(@"hh\:mm\:ss"); // Add 12 hours (in seconds) to the julian date time-of-day value for time strings, because of the 12 noon day cut-over convention in Julian Date values.

    #region Static methods

    public static (int days, int hours, int minutes, double seconds) ConvertToJdf(double julianDateFraction)
    {
      var days = (int)julianDateFraction;
      julianDateFraction -= days;

      julianDateFraction *= 24;
      var hours = (int)julianDateFraction;
      julianDateFraction -= hours;

      julianDateFraction *= 60;
      var minutes = (int)julianDateFraction;
      julianDateFraction -= minutes;

      var seconds = julianDateFraction * 60;

      return (days, hours, minutes, seconds);
    }

    /// <summary>Converts the time components to a Julian Date (JD) "time-of-day" fraction value. This is not the same as the number of seconds.</summary>
    public static double ConvertFromTimeParts(int hour, int minute, int second, int millisecond)
      => (hour - 12) / 24d + minute / 1440d + (second + millisecond / 1000d) / 86400d;

    /// <summary>Converts the Julian Date (JD) to discrete time components. This method is only concerned with the time portion of the Julian Date.</summary>
    public static (int hour, int minute, int second, int millisecond) ConvertToTimeParts(double julianDate)
    {
      var totalSeconds = GetTimeSinceNoon(julianDate);

      if (totalSeconds <= 43200)
        totalSeconds = (totalSeconds + 43200d) % 86400d;

      var hour = (int)(totalSeconds / 3600d);
      totalSeconds -= hour * 3600d;

      var minute = (int)(totalSeconds / 60d);
      totalSeconds -= minute * 60d;

      var second = (int)totalSeconds;
      totalSeconds -= second;

      var millisecond = (int)(totalSeconds * 1000);

      return (hour, minute, second, millisecond);
    }

    /// <summary>Compute the time-of-day. I.e. the number of seconds from 12 noon of the Julian Day Number part.</summary>
    public static double GetTimeSinceNoon(double julianDate)
      => (julianDate - System.Math.Truncate(julianDate)) * 86400d;

    /// <summary>Returns whether the Julian Date value (JD) is considered to be on the Gregorian Calendar.</summary>
    public static bool IsGregorianCalendar(double julianDate)
      => julianDate >= 2299160.5;

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(JulianDate a, JulianDate b) => a.CompareTo(b) < 0;
    public static bool operator <=(JulianDate a, JulianDate b) => a.CompareTo(b) <= 0;
    public static bool operator >(JulianDate a, JulianDate b) => a.CompareTo(b) > 0;
    public static bool operator >=(JulianDate a, JulianDate b) => a.CompareTo(b) >= 0;

    public static JulianDate operator -(JulianDate jd) => new(-jd.m_value);
    public static double operator -(JulianDate a, JulianDate b) => a.m_value - b.m_value;

    public static JulianDate operator +(JulianDate a, double b) => new(a.m_value + b);
    public static JulianDate operator /(JulianDate a, double b) => new(a.m_value / b);
    public static JulianDate operator *(JulianDate a, double b) => new(a.m_value * b);
    public static JulianDate operator %(JulianDate a, double b) => new(a.m_value % b);
    public static JulianDate operator -(JulianDate a, double b) => new(a.m_value - b);

    public static JulianDate operator +(JulianDate a, int b) => new(a.m_value + b);
    public static JulianDate operator /(JulianDate a, int b) => new(a.m_value / b);
    public static JulianDate operator *(JulianDate a, int b) => new(a.m_value * b);
    public static JulianDate operator %(JulianDate a, int b) => new(a.m_value % b);
    public static JulianDate operator -(JulianDate a, int b) => new(a.m_value - b);

    public static JulianDate operator +(JulianDate a, Units.Time b) => a + (b.Value / 86400);
    public static JulianDate operator -(JulianDate a, Units.Time b) => a - (b.Value / 86400);

    public static JulianDate operator +(JulianDate a, System.TimeSpan b) => a + (b.TotalSeconds / 86400);
    public static JulianDate operator -(JulianDate a, System.TimeSpan b) => a - (b.TotalSeconds / 86400);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(JulianDate other) => m_value < other.m_value ? -1 : m_value > other.m_value ? 1 : 0;

    // IComparable
    public int CompareTo(object? other) => other is not null && other is JulianDate o ? CompareTo(o) : -1;

    // IFormattable
    public string ToString(string? format, IFormatProvider? formatProvider) => format is null ? ToQuantityString() : m_value.ToString(format, formatProvider);

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => $"{ToJulianDayNumber().ToDateString(GetConversionCalendar())}, {ToTimeString()}";

    public double Value { get => m_value; init => m_value = value; }

    #endregion Implemented interfaces

    public override string? ToString() => ToQuantityString();
  }
}
