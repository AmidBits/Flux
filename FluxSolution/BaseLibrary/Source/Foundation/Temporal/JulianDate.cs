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
    : System.IComparable<JulianDate>, System.IConvertible, System.IEquatable<JulianDate>, IQuantifiable<double>
  {
    public readonly static JulianDate Zero;

    private readonly double m_value;

    /// <summary>Create a Julian Date (JD) from the specified <paramref name="value"/> value.</summary>
    public JulianDate(double value)
      => m_value = value;
    /// <summary>Computes the Julian Date (JD) for the specified date/time components and calendar to use during conversion.</summary>
    public JulianDate(int year, int month, int day, int hour, int minute, int second, int millisecond, ConversionCalendar calendar)
      : this(JulianDayNumber.ConvertFromDateParts(year, month, day, calendar) + ConvertFromTimeParts(hour, minute, second, millisecond))
    { }

    public double Value
      => m_value;

    public JulianDate AddWeeks(int weeks)
      => this + (weeks * 7);
    public JulianDate AddDays(int days)
      => this + days;
    public JulianDate AddHours(int hours)
      => this + (hours / 24d);
    public JulianDate AddMinutes(int minutes)
      => this + (minutes / 1440d);
    public JulianDate AddSeconds(int seconds)
      => this + (seconds / 86400d);
    public JulianDate AddMilliseconds(int milliseconds)
      => this + (milliseconds / 1000d / 86400d);

    public ConversionCalendar GetConversionCalendar()
      => IsGregorianCalendar(m_value) ? ConversionCalendar.GregorianCalendar : ConversionCalendar.JulianCalendar;

    public void GetParts(ConversionCalendar calendar, out int year, out int month, out int day, out int hour, out int minute, out int second, out int millisecond)
    {
      ToJulianDayNumber().GetDateParts(calendar, out year, out month, out day);
      ConvertToTimeParts(m_value, out hour, out minute, out second, out millisecond);
    }

    public JulianDayNumber ToJulianDayNumber()
      => new((int)(m_value + 0.5));

    public MomentUtc ToMomentUtc(ConversionCalendar calendar)
    {
      ToJulianDayNumber().GetDateParts(calendar, out var year, out var month, out var day);
      ConvertToTimeParts(m_value, out var hour, out var minute, out var second, out var millisecond);

      return new(year, month, day, hour, minute, second, millisecond);
    }

    public string ToTimeString()
      => System.TimeSpan.FromSeconds(System.Convert.ToDouble(43200 + GetTimeSinceNoon(m_value))).ToString(@"hh\:mm\:ss"); // Add 12 hours (in seconds) to the julian date time-of-day value for time strings, because of the 12 noon day cut-over convention in Julian Date values.

    #region Static methods
    /// <summary>Converts the time components to a Julian Date (JD) "time-of-day" fraction value. This is not the same as the number of seconds.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertFromTimeParts(int hour, int minute, int second, int millisecond)
      => (hour - 12) / 24d + minute / 1440d + (second + millisecond / 1000d) / 86400d;
    /// <summary>Converts the Julian Date (JD) to discrete time components. This method is only concerned with the time portion of the Julian Date.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static void ConvertToTimeParts(double julianDate, out int hour, out int minute, out int second, out int millisecond)
    {
      var totalSeconds = GetTimeSinceNoon(julianDate);

      if (totalSeconds <= 43200)
        totalSeconds = (totalSeconds + 43200d) % 86400d;

      hour = (int)(totalSeconds / 3600d);
      totalSeconds -= hour * 3600d;

      minute = (int)(totalSeconds / 60d);
      totalSeconds -= minute * 60d;

      second = (int)totalSeconds;
      totalSeconds -= second;

      millisecond = (int)totalSeconds;
    }

    /// <summary>Compute the time-of-day. I.e. the number of seconds from 12 noon of the Julian Day Number part.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double GetTimeSinceNoon(double julianDate)
      => julianDate.GetFraction() * 86400d;

    /// <summary>Returns whether the Julian Date value (JD) is considered to be on the Gregorian Calendar.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsGregorianCalendar(double julianDate)
      => julianDate >= 2299160.5;
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator <(JulianDate a, JulianDate b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(JulianDate a, JulianDate b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(JulianDate a, JulianDate b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(JulianDate a, JulianDate b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(JulianDate a, JulianDate b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(JulianDate a, JulianDate b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static JulianDate operator -(JulianDate jd) => new(-jd.m_value);
    [System.Diagnostics.Contracts.Pure] public static double operator -(JulianDate a, JulianDate b) => a.m_value - b.m_value;

    [System.Diagnostics.Contracts.Pure] public static JulianDate operator +(JulianDate a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static JulianDate operator /(JulianDate a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static JulianDate operator *(JulianDate a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static JulianDate operator %(JulianDate a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static JulianDate operator -(JulianDate a, double b) => new(a.m_value - b);

    [System.Diagnostics.Contracts.Pure] public static JulianDate operator +(JulianDate a, int b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static JulianDate operator /(JulianDate a, int b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static JulianDate operator *(JulianDate a, int b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static JulianDate operator %(JulianDate a, int b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static JulianDate operator -(JulianDate a, int b) => new(a.m_value - b);

    [System.Diagnostics.Contracts.Pure] public static JulianDate operator +(JulianDate a, Time b) => a + (b.Value / 86400);
    [System.Diagnostics.Contracts.Pure] public static JulianDate operator -(JulianDate a, Time b) => a - (b.Value / 86400);

    [System.Diagnostics.Contracts.Pure] public static JulianDate operator +(JulianDate a, System.TimeSpan b) => a + (b.TotalSeconds / 86400);
    [System.Diagnostics.Contracts.Pure] public static JulianDate operator -(JulianDate a, System.TimeSpan b) => a - (b.TotalSeconds / 86400);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(JulianDate other) => m_value < other.m_value ? -1 : m_value > other.m_value ? 1 : 0;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
    #endregion IConvertible

    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(JulianDate other) => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure]
    public override bool Equals(object? obj)
      => obj is JulianDate o && Equals(o);
    [System.Diagnostics.Contracts.Pure]
    public override int GetHashCode()
      => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure]
    public override string? ToString()
      => $"{GetType().Name} {{ {m_value} ({ToJulianDayNumber().ToDateString(GetConversionCalendar())}, {ToTimeString()}) }}";
    #endregion Object overrides
  }
}
