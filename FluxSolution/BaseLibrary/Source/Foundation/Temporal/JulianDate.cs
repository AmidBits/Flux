namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static JulianDate ToJulianDate(this System.DateTime source, ConversionCalendar calendar)
      => ToMomentUtc(source).ToJulianDate(calendar);
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
      => new MomentUtc(1582, 10, 4, 23, 59, 59, 999).ToJulianDate(ConversionCalendar.JulianCalendar);

    public static readonly JulianDate Zero;

    private readonly double m_value;

    /// <summary>Create a Julian Date (JD) from the specified <paramref name="value"/> value.</summary>
    public JulianDate(double value)
      => m_value = value;
    /// <summary>Computes the Julian Date (JD) for the specified date/time components and calendar to use during conversion.</summary>
    public JulianDate(int year, int month, int day, int hour, int minute, int second, int millisecond, ConversionCalendar calendar)
      : this(JulianDayNumber.ConvertFromDateParts(year, month, day, calendar) + ConvertFromTimeParts(hour, minute, second, millisecond))
    { }

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
      => new JulianDate(m_value + (milliseconds / 1000.0) / 86400.0);

    public double Value
      => m_value;

    public void GetParts(ConversionCalendar calendar, out int year, out int month, out int day, out int hour, out int minute, out int second, out int millisecond)
    {
      ToJulianDayNumber().GetDateParts(calendar, out year, out month, out day);
      ConvertToTimeParts(m_value, out hour, out minute, out second, out millisecond);
    }

    public JulianDayNumber ToJulianDayNumber()
      => new JulianDayNumber((int)(m_value + 0.5));
    public MomentUtc ToMomentUtc(ConversionCalendar calendar)
    {
      ToJulianDayNumber().GetDateParts(calendar, out var year, out var month, out var day);
      ConvertToTimeParts(m_value, out var hour, out var minute, out var second, out var millisecond);

      return new MomentUtc(year, month, day, hour, minute, second, millisecond);
    }
    public string ToTimeString()
      => System.TimeSpan.FromSeconds(43200 + GetTimeSinceNoon(m_value).Value).ToString(@"hh\:mm\:ss"); // Add 12 hours (in seconds) to the julian date time-of-day value for time strings, because of the 12 noon day cut-over convention in Julian Date values.

    #region Static methods
    /// <summary>Converts the time components to a Julian Date (JD) "time-of-day" fraction value. This is not the same as the number of seconds.</summary>
    public static double ConvertFromTimeParts(int hour, int minute, int second, int millisecond)
      => (hour - 12) / 24.0 + minute / 1440.0 + (second + millisecond / 1000.0) / 86400;
    /// <summary>Converts the Julian Date (JD) to discrete time components. This method is only concerned with the time portion of the Julian Date.</summary>
    public static void ConvertToTimeParts(double julianDate, out int hour, out int minute, out int second, out int millisecond)
    {
      var totalSeconds = GetTimeSinceNoon(julianDate).Value;

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

    /// <summary>Returns the time string of the time-of-day portion of the Julian Date.</summary>
    /// <summary>Compute the time-of-day. I.e. the number of seconds from 12 noon of the Julian Day Number part.</summary>
    public static Quantity.Time GetTimeSinceNoon(double julianDate)
      => new Quantity.Time(julianDate.GetFraction() * 86400);

    /// <summary>Returns whether the Julian Date value (JD) is considered to be on the Gregorian Calendar.</summary>
    public static bool IsGregorianCalendar(double julianDate)
      => julianDate >= 2299160.5;
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
    {
      return $"<{GetType().Name}: {m_value} " + (IsGregorianCalendar(m_value) ? $"({ToJulianDayNumber().ToDateString(ConversionCalendar.GregorianCalendar)}, {ToTimeString()})" : $"({ToJulianDayNumber().ToDateString(ConversionCalendar.JulianCalendar)}, {ToTimeString()})*");
    }
    #endregion Object overrides
  }
}
