namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static JulianDate ToJulianDate(this System.DateTime source, ConversionCalendar calendar)
      => ToMomentUtc(source).ToJulianDate(calendar);
  }

  /// <summary>Julian Date unit of days with time of day fraction.</summary>
  /// <remarks>Julian Date is not related to the Julian Calendar. Functionality that compute on the Julian Calendar will have JulianCalendar in the name.</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public struct JulianDate
    : System.IComparable<JulianDate>, System.IEquatable<JulianDate>, Quantity.IValuedUnit<decimal>
  {
    public readonly static JulianDate Zero;

    private readonly decimal m_value;

    /// <summary>Create a Julian Date (JD) from the specified <paramref name="value"/> value.</summary>
    public JulianDate(decimal value)
      => m_value = value;
    /// <summary>Computes the Julian Date (JD) for the specified date/time components and calendar to use during conversion.</summary>
    public JulianDate(int year, int month, int day, int hour, int minute, int second, int millisecond, ConversionCalendar calendar)
      : this(JulianDayNumber.ConvertFromDateParts(year, month, day, calendar) + ConvertFromTimeParts(hour, minute, second, millisecond))
    { }

    public decimal Value
      => m_value;

    public JulianDate AddWeeks(int weeks)
      => this + (weeks * 7);
    public JulianDate AddDays(int days)
      => this + days;
    public JulianDate AddHours(int hours)
      => this + (hours / 24.0m);
    public JulianDate AddMinutes(int minutes)
      => this + (minutes / 1440.0m);
    public JulianDate AddSeconds(int seconds)
      => this + (seconds / 86400.0m);
    public JulianDate AddMilliseconds(int milliseconds)
      => this + (milliseconds / 1000.0m / 86400m);

    public ConversionCalendar GetConversionCalendar()
      => IsGregorianCalendar(m_value) ? ConversionCalendar.GregorianCalendar : ConversionCalendar.JulianCalendar;

    public void GetParts(ConversionCalendar calendar, out int year, out int month, out int day, out int hour, out int minute, out int second, out int millisecond)
    {
      ToJulianDayNumber().GetDateParts(calendar, out year, out month, out day);
      ConvertToTimeParts(m_value, out hour, out minute, out second, out millisecond);
    }

    public JulianDayNumber ToJulianDayNumber()
      => new((int)(m_value + 0.5m));

    public MomentUtc ToMomentUtc(ConversionCalendar calendar)
    {
      ToJulianDayNumber().GetDateParts(calendar, out var year, out var month, out var day);
      ConvertToTimeParts(m_value, out var hour, out var minute, out var second, out var millisecond);

      return new(year, month, day, hour, minute, second, millisecond);
    }

    public string ToTimeString()
      => System.TimeSpan.FromSeconds(System.Convert.ToDouble(43200m + GetTimeSinceNoon(m_value))).ToString(@"hh\:mm\:ss"); // Add 12 hours (in seconds) to the julian date time-of-day value for time strings, because of the 12 noon day cut-over convention in Julian Date values.

    #region Static methods
    /// <summary>Converts the time components to a Julian Date (JD) "time-of-day" fraction value. This is not the same as the number of seconds.</summary>
    public static decimal ConvertFromTimeParts(int hour, int minute, int second, int millisecond)
      => (hour - 12) / 24.0m + minute / 1440.0m + (second + millisecond / 1000.0m) / 86400m;
    /// <summary>Converts the Julian Date (JD) to discrete time components. This method is only concerned with the time portion of the Julian Date.</summary>
    public static void ConvertToTimeParts(decimal julianDate, out int hour, out int minute, out int second, out int millisecond)
    {
      var totalSeconds = GetTimeSinceNoon(julianDate);

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
    public static decimal GetTimeSinceNoon(decimal julianDate)
      => julianDate.GetFraction() * 86400m;

    /// <summary>Returns whether the Julian Date value (JD) is considered to be on the Gregorian Calendar.</summary>
    public static bool IsGregorianCalendar(decimal julianDate)
      => julianDate >= 2299160.5m;
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
      => new(-jd.m_value);
    public static decimal operator -(JulianDate a, JulianDate b)
      => a.m_value - b.m_value;

    public static JulianDate operator +(JulianDate a, decimal b)
      => new(a.m_value + b);
    public static JulianDate operator /(JulianDate a, decimal b)
      => new(a.m_value / b);
    public static JulianDate operator *(JulianDate a, decimal b)
      => new(a.m_value * b);
    public static JulianDate operator %(JulianDate a, decimal b)
      => new(a.m_value % b);
    public static JulianDate operator -(JulianDate a, decimal b)
      => new(a.m_value - b);

    public static JulianDate operator +(JulianDate a, double b)
      => new(a.m_value + System.Convert.ToDecimal(b));
    public static JulianDate operator /(JulianDate a, double b)
      => new(a.m_value / System.Convert.ToDecimal(b));
    public static JulianDate operator *(JulianDate a, double b)
      => new(a.m_value * System.Convert.ToDecimal(b));
    public static JulianDate operator %(JulianDate a, double b)
      => new(a.m_value % System.Convert.ToDecimal(b));
    public static JulianDate operator -(JulianDate a, double b)
      => new(a.m_value - System.Convert.ToDecimal(b));

    public static JulianDate operator +(JulianDate a, int b)
      => new(a.m_value + System.Convert.ToDecimal(b));
    public static JulianDate operator /(JulianDate a, int b)
      => new(a.m_value / System.Convert.ToDecimal(b));
    public static JulianDate operator *(JulianDate a, int b)
      => new(a.m_value * System.Convert.ToDecimal(b));
    public static JulianDate operator %(JulianDate a, int b)
      => new(a.m_value % System.Convert.ToDecimal(b));
    public static JulianDate operator -(JulianDate a, int b)
      => new(a.m_value - System.Convert.ToDecimal(b));

    public static JulianDate operator +(JulianDate a, Quantity.Time b)
      => a + (System.Convert.ToDecimal(b.Value) / 86400m);
    public static JulianDate operator -(JulianDate a, Quantity.Time b)
      => a - (System.Convert.ToDecimal(b.Value) / 86400m);

    public static JulianDate operator +(JulianDate a, System.TimeSpan b)
      => a + (System.Convert.ToDecimal(b.TotalSeconds) / 86400m);
    public static JulianDate operator -(JulianDate a, System.TimeSpan b)
      => a - (System.Convert.ToDecimal(b.TotalSeconds) / 86400m);
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
      => $"<{GetType().Name}: {m_value} " + $"({ToJulianDayNumber().ToDateString(GetConversionCalendar())}, {ToTimeString()})";
    #endregion Object overrides
  }
}
