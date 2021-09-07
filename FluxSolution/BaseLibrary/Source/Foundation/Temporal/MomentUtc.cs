namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static MomentUtc ToMomentUtc(this System.DateTime source)
      => new MomentUtc(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, source.Millisecond);
  }

  /// <summary>A moment is a specific point in time down to the millisecond.</summary>
  public struct MomentUtc
    : System.IComparable<MomentUtc>, System.IEquatable<MomentUtc>
  {
    public static readonly MomentUtc Empty;

    public int m_year;
    public int m_month;
    public int m_day;
    public int m_hour;
    public int m_minute;
    public int m_second;
    public int m_millisecond;

    public MomentUtc(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
      m_year = year;
      m_month = month >= 1 ? month : throw new System.ArgumentOutOfRangeException(nameof(month));
      m_day = day >= 1 ? day : throw new System.ArgumentOutOfRangeException(nameof(day));
      m_hour = hour >= 0 && hour < 24 ? hour : throw new System.ArgumentOutOfRangeException(nameof(hour));
      m_minute = minute >= 0 && minute <= 59 ? minute : throw new System.ArgumentOutOfRangeException(nameof(minute));
      m_second = second >= 0 && second <= 59 ? second : throw new System.ArgumentOutOfRangeException(nameof(second));
      m_millisecond = millisecond >= 0 && millisecond <= 999 ? millisecond : throw new System.ArgumentOutOfRangeException(nameof(millisecond));
    }
    public MomentUtc(int year, int month, int day, int hour, int minute, int second)
      : this(year, month, day, hour, minute, second, 0)
    { }
    public MomentUtc(int year, int month, int day)
      : this(year, month, day, 0, 0, 0, 0)
    { }

    public int Year
      => m_year;
    public int Month
      => m_month;
    public int Day
      => m_day;
    public int Hour
      => m_hour;
    public int Minute
      => m_minute;
    public int Second
      => m_second;
    public int Millisecond
      => m_millisecond;

    ///// <summary>Returns a number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
    //public double TotalTime
    //  => System.Math.CopySign(System.Math.Abs(m_year) * 31536000L + m_month * 2628000L + m_day * 86400L + m_hour * 3600L + m_minute * 60L + m_second + m_millisecond / 1000.0, m_year);

    /// <summary>Create a new <see cref="JulianDate"/> from this instance.</summary>
    public JulianDate ToJulianDate(ConversionCalendar calendar)
      => new JulianDate(ComputeJulianDate(m_year, m_month, m_day, m_hour, m_minute, m_second, m_millisecond, calendar));

    #region Static methods
    /// <summary>Computes the Julian Date (JD) for the specified date/time components and calendar to use during conversion.</summary>
    public static double ComputeJulianDate(int year, int month, int day, int hour, int minute, int second, int millisecond, ConversionCalendar calendar)
      => ComputeJulianDayNumber(year, month, day, calendar) + ComputeJulianDateTimeOfDay(hour, minute, second, millisecond);
    /// <summary>Computes the Julian Date (JD) "time-of-day" fraction for the specified time components. This is not the same as the number of seconds.</summary>
    public static double ComputeJulianDateTimeOfDay(int hour, int minute, int second, int millisecond)
      => (hour - 12) / 24.0 + minute / 1440.0 + (second + millisecond / 1000.0) / 86400;
    /// <summary>Computes the Julian Day Number (JDN) for the specified date components and calendar to use during conversion.</summary>
    public static int ComputeJulianDayNumber(int year, int month, int day, ConversionCalendar calendar)
    {
      switch (calendar)
      {
        case ConversionCalendar.GregorianCalendar:
          return ((1461 * (year + 4800 + (month - 14) / 12)) / 4 + (367 * (month - 2 - 12 * ((month - 14) / 12))) / 12 - (3 * ((year + 4900 + (month - 14) / 12) / 100)) / 4 + day - 32075);
        case ConversionCalendar.JulianCalendar:
          return (367 * year - (7 * (year + 5001 + (month - 9) / 7)) / 4 + (275 * month) / 9 + day + 1729777);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(calendar));
      }
    }

    /// <summary>Returns whether the date is considered to be in the modern Gregorian Calendar.</summary>
    public static bool IsGregorianCalendar(int year, int month, int day)
      => year > 1582 || (year == 1582 && (month > 10 || (month == 10 && day >= 15)));
    /// <summary>Returns whether the date is considered to be in the traditional Julian Calendar.</summary>
    public static bool IsJulianCalendar(int year, int month, int day)
      => year >= -4712 && year < 1582 || (year == 1582 && (month < 10 || (month == 10 && day <= 4)));

    /// <summary>Returns whether the date is a valid date in the Gregorian calendar.</summary>
    public static bool IsValidGregorianCalendarDate(int year, int month, int day)
      => IsGregorianCalendar(year, month, day) && month >= 1 && month <= 12 && day >= 1 && day <= System.DateTime.DaysInMonth(year, month);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator <(MomentUtc a, MomentUtc b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(MomentUtc a, MomentUtc b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(MomentUtc a, MomentUtc b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(MomentUtc a, MomentUtc b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(MomentUtc a, MomentUtc b)
      => a.Equals(b);
    public static bool operator !=(MomentUtc a, MomentUtc b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    public int CompareTo(MomentUtc other)
      => m_year < other.m_year ? -1 : m_year > other.m_year ? 1
      : m_month < other.m_month ? -1 : m_month > other.m_month ? 1
      : m_day < other.m_day ? -1 : m_day > other.m_day ? 1
      : m_hour < other.m_hour ? -1 : m_hour > other.m_hour ? 1
      : m_minute < other.m_minute ? -1 : m_minute > other.m_minute ? 1
      : m_second < other.m_second ? -1 : m_second > other.m_second ? 1
      : m_millisecond < other.m_millisecond ? -1 : m_millisecond > other.m_millisecond ? 1
      : 0; // This means this instance is equal to the other.

    // IEquatable<>
    public bool Equals(MomentUtc other)
      => m_year == other.m_year
      && m_month == other.m_month
      && m_day == other.m_day
      && m_hour == other.m_hour
      && m_minute == other.m_minute
      && m_second == other.m_second
      && m_millisecond == other.m_millisecond;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is MomentUtc o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_year, m_month, m_day, m_hour, m_minute, m_second, m_millisecond);
    public override string? ToString()
      => $"<{GetType().Name}: {m_year:D4}-{m_month:D2}-{m_day:D2} {m_hour:D2}:{m_minute:D2}:{m_second:D2}.{m_millisecond:D3}>";
    #endregion Object overrides
  }
}
