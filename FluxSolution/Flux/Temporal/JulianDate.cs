namespace Flux
{
  public static partial class Em
  {
    public static Temporal.JulianDate ToJulianDate(this System.DateTime source, Temporal.TemporalCalendar calendar = Temporal.TemporalCalendar.GregorianCalendar)
      => new(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, source.Millisecond, source.Microsecond, source.Nanosecond, calendar);
  }

  namespace Temporal
  {
    /// <summary>
    /// <para>Julian Date, unit of days with time-of-day as the fraction. The time-of-day fraction is the time from the preceeding noon. This is why it is necessary to add 0.5 to a julian-date in order to obtain a correct julian-day-number based on midnight.</para>
    /// <para>The Julian Date struct can also be used to handle Julian Day Fraction, which is the number of days, hours, minutes and seconds since the beginning of the year.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Julian_day"/></para>
    /// </summary>
    /// <remarks>Julian Date is not related to the Julian Calendar. Functionality that compute on the Julian Calendar will have JulianCalendar in the name.</remarks>
    public readonly record struct JulianDate
      : System.IComparable, System.IComparable<JulianDate>, System.IFormattable, IValueQuantifiable<double>
    {
      public static readonly JulianDate GregorianCalendarEpoch = new(2299160.5); // 1582/10/15 (midnight)
      public static readonly JulianDate JulianCalendarEpoch = new(1718501.5); // -0007/01/01 (midnight)

      private readonly double m_value;

      /// <summary>Create a Julian Date (JD) from the specified <paramref name="value"/> value.</summary>
      public JulianDate(double value) => m_value = value;

      /// <summary>Computes the Julian Date (JD) for the specified date/time components and calendar to use during conversion.</summary>
      public JulianDate(int year, int month, int day, int hour, int minute, int second, int millisecond, int microsecond, int nanosecond, TemporalCalendar calendar)
        : this(JulianDayNumber.ConvertDatePartsToJulianDayNumber(year, month, day, calendar) + ConvertTimePartsToTimeOfDay(hour, minute, second, millisecond, microsecond, nanosecond)) { }

      public JulianDate AddDays(int days) => this + days;
      public JulianDate AddHours(int hours) => this + (hours / 24d);
      public JulianDate AddMinutes(int minutes) => this + (minutes / 1440d);
      public JulianDate AddSeconds(int seconds) => this + (seconds / 86400d);
      public JulianDate AddMilliseconds(int milliseconds) => this + (milliseconds / 1000d / 86400d);
      public JulianDate AddMicroseconds(int microseconds) => this + (microseconds / 1000000d / 86400d);
      public JulianDate AddNanoseconds(int nanoseconds) => this + (nanoseconds / 1000000000d / 86400d);

      public TemporalCalendar GetConversionCalendar() => TemporalCalendar.GregorianCalendar.Contains(m_value) ? TemporalCalendar.GregorianCalendar : TemporalCalendar.JulianCalendar;

      public (int Year, int Month, int Day, int Hour, int Minute, int Second, int Millisecond, int Microsecond, int Nanosecond) GetParts(TemporalCalendar? calendar = null)
      {
        var (year, month, day) = ToJulianDayNumber().GetParts(calendar ?? GetConversionCalendar());
        var (hour, minute, second, millisecond, microsecond, nanosecond) = ConvertTimeOfDayToTimeParts(m_value);

        return (year, month, day, hour, minute, second, millisecond, microsecond, nanosecond);
      }

      public JulianDayNumber ToJulianDayNumber() => new(ConvertJulianDateToJulianDayNumber(m_value));

      public Moment ToMoment(TemporalCalendar? calendar = null)
      {
        var (year, month, day, hour, minute, second, millisecond, microsecond, nanosecond) = GetParts(calendar);

        return new(year, month, day, hour, minute, second, millisecond, microsecond, nanosecond);
      }

      public string ToTimeString()
        => System.TimeSpan.FromSeconds(ConvertTimeOfDayToTime(m_value).Value + 43200d).ToString(@"hh\:mm\:ss"); // Add 12 hours (in seconds) to the julian date time-of-day value for time strings, because of the 12 noon day cut-over convention in Julian Date values.

      #region Static methods

      #region Conversion methods

      /// <summary>Converts the julian-date time fraction to time-of-day in seconds. The fractional part represents the unit time, from the preceeding noon to the next, in Universal Time.</summary>
      public static Units.Time ConvertTimeOfDayToTime(double julianDate) => new((julianDate - System.Math.Truncate(julianDate)) * 86400d);

      /// <summary>
      /// <para>Converts a Julian Date (JD) to a Julian Day Number (JDN).</para>
      /// <para>JD is a JDN + time-of-day (TOD) from the preceeding noon, in other words, the range is noon-to-noon.</para>
      /// <para>TOD is a fraction with essentially two ranges, range [0.0, 0.5] is time from the preceeding noon to midnight, and range [0.5, 1.0] is time from midnight to noon of the JDN.</para>
      /// <para>To ensure the second range being overflowed into the "next day", adding 0.5 and truncating, the range becomes the familiar year/month/day, i.e. midnight-to-midnight.</para>
      /// </summary>
      /// <param name="julianDate"></param>
      /// <returns></returns>
      public static int ConvertJulianDateToJulianDayNumber(double julianDate) => (int)(julianDate + 0.5);

      //public static (int days, int hours, int minutes, double seconds) ConvertToJdf(double julianDateFraction)
      //{
      //  var days = (int)julianDateFraction;

      //  julianDateFraction = (julianDateFraction - days) * 24;

      //  var hours = (int)julianDateFraction;

      //  julianDateFraction = (julianDateFraction - hours) * 60;

      //  var minutes = (int)julianDateFraction;

      //  julianDateFraction = (julianDateFraction - minutes) * 60;

      //  var seconds = julianDateFraction;

      //  return (days, hours, minutes, seconds);
      //}

      /// <summary>
      /// <para>Converts a Julian Date (JD) time-of-day fraction value to time components. This method is only concerned with the fractional time portion of the Julian Date (JD).</para>
      /// </summary>
      public static (int hour, int minute, int second, int millisecond, int microsecond, int nanosecond) ConvertTimeOfDayToTimeParts(double julianDate)
      {
        var totalSeconds = ConvertTimeOfDayToTime(julianDate).Value;

        if (totalSeconds <= 43200) // Adjust for noon.
          totalSeconds = (totalSeconds + 43200d) % 86400d;

        var hour = (int)(totalSeconds / 3600d);
        totalSeconds -= hour * 3600d;

        var minute = (int)(totalSeconds / 60d);
        totalSeconds -= minute * 60d;

        var second = (int)totalSeconds;
        totalSeconds -= second;

        var millisecond = (int)(totalSeconds * 1000);
        totalSeconds -= millisecond / 1000d;

        var microsecond = (int)(totalSeconds * 1000000);
        totalSeconds -= microsecond / 1000000d;

        var nanosecond = (int)(totalSeconds * 1000000000);

        return (hour, minute, second, millisecond, microsecond, nanosecond);
      }

      /// <summary>
      /// <para>Converts time components to a Julian Date (JD) time-of-day fraction value. This is not the same as the number of seconds.</para>
      /// </summary>
      public static double ConvertTimePartsToTimeOfDay(int hour, int minute, int second, int millisecond, int microsecond, int nanosecond)
        => (hour - 12) / 24d + minute / 1440d + (second + millisecond / 1000d + microsecond / 1000000d + nanosecond / 1000000000d) / 86400d;

      #endregion // Conversion methods

      #endregion // Static methods

      #region Overloaded operators

      public static explicit operator JulianDate(double v) => new(v);
      public static explicit operator double(JulianDate v) => v.m_value;

      public static bool operator <(JulianDate a, JulianDate b) => a.CompareTo(b) < 0;
      public static bool operator <=(JulianDate a, JulianDate b) => a.CompareTo(b) <= 0;
      public static bool operator >(JulianDate a, JulianDate b) => a.CompareTo(b) > 0;
      public static bool operator >=(JulianDate a, JulianDate b) => a.CompareTo(b) >= 0;

      public static JulianDate operator -(JulianDate jd) => new(-jd.m_value);
      public static double operator -(JulianDate a, JulianDate b) => a.m_value - b.m_value;

      public static JulianDate operator +(JulianDate a, double b) => new(a.m_value + b);
      public static double operator +(double a, JulianDate b) => a + b.m_value;
      public static JulianDate operator /(JulianDate a, double b) => new(a.m_value / b);
      public static JulianDate operator *(JulianDate a, double b) => new(a.m_value * b);
      public static JulianDate operator %(JulianDate a, double b) => new(a.m_value % b);
      public static JulianDate operator -(JulianDate a, double b) => new(a.m_value - b);
      public static double operator -(double a, JulianDate b) => a - b.m_value;

      public static JulianDate operator +(JulianDate a, Units.Time b) => a + (b.Value / 86400);
      public static JulianDate operator -(JulianDate a, Units.Time b) => a - (b.Value / 86400);

      public static JulianDate operator +(JulianDate a, System.TimeSpan b) => a + (b.TotalSeconds / 86400);
      public static JulianDate operator -(JulianDate a, System.TimeSpan b) => a - (b.TotalSeconds / 86400);

      #endregion // Overloaded operators

      #region Implemented interfaces

      // IComparable<>
      public int CompareTo(JulianDate other) => m_value < other.m_value ? -1 : m_value > other.m_value ? 1 : 0;

      // IComparable
      public int CompareTo(object? other) => other is not null && other is JulianDate o ? CompareTo(o) : -1;

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider) => $"{ToJulianDayNumber().ToDateString(GetConversionCalendar())}, {ToTimeString()} (JD = {m_value})";

      #region IValueQuantifiable<>

      /// <summary>
      /// <para>The <see cref="JulianDate.Value"/> property is the Julian date.</para>
      /// </summary>
      public double Value => m_value;

      #endregion // IValueQuantifiable<>

      #endregion // Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
