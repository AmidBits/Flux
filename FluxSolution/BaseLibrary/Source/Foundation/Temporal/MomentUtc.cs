namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the approximate number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
    public static double GetTotalApproximateSeconds(this MomentUtc source)
      => System.Math.CopySign(System.Math.Abs(source.m_year) * 31536000L + source.m_month * 2628000L + source.m_day * 86400L + source.m_hour * 3600L + source.m_minute * 60L + source.m_second + source.m_millisecond / 1000.0, source.m_year);

    public static MomentUtc ToMomentUtc(this System.DateTime source)
      => new(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, source.Millisecond);
  }

  /// <summary>A moment is a specific point in time down to the millisecond.</summary>
#if NET5_0
  public struct MomentUtc
    : System.IComparable<MomentUtc>, System.IEquatable<MomentUtc>
#else
  public record struct MomentUtc
    : System.IComparable<MomentUtc>
#endif
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
      m_year = year >= -4712 ? year : throw new System.ArgumentOutOfRangeException(nameof(year));
      m_month = month >= 1 && month <= 12 ? month : throw new System.ArgumentOutOfRangeException(nameof(month));
      m_day = day >= 1 && day <= 31 ? day : throw new System.ArgumentOutOfRangeException(nameof(day));
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

    public ConversionCalendar GetConversionCalendar()
      => IsGregorianCalendar(m_year, m_month, m_day) ? ConversionCalendar.GregorianCalendar : IsJulianCalendar(m_year, m_month, m_day) ? ConversionCalendar.JulianCalendar : throw new System.NotImplementedException(@"Not a Julian/Gregorian Calendar date.");

    /// <summary>Creates a new <see cref="System.DateTime"/> from this instance.</summary>
    public System.DateTime ToDateTime()
      => new(m_year, m_month, m_day, m_hour, m_minute, m_second, m_millisecond);

    /// <summary>Creates a new <see cref="JulianDate"/> from this instance. Uses the specified conversion calendar.</summary>
    public JulianDate ToJulianDate(ConversionCalendar calendar)
      => new(m_year, m_month, m_day, m_hour, m_minute, m_second, m_millisecond, calendar);
    /// <summary>Creates a new <see cref="JulianDate"/> from this instance. Uses the default conversion calendar.</summary>
    public JulianDate ToJulianDate()
      => ToJulianDate(GetConversionCalendar());

    /// <summary>Creates a new <see cref="JulianDayNumber"/> from this instance. Uses the specified conversion calendar.</summary>
    public JulianDayNumber ToJulianDayNumber(ConversionCalendar calendar)
      => new(m_year, m_month, m_day, calendar);
    /// <summary>Creates a new <see cref="JulianDayNumber"/> from this instance. Uses the default conversion calendar.</summary>
    public JulianDayNumber ToJulianDayNumber()
      => ToJulianDayNumber(GetConversionCalendar());

    #region Static methods
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

#if NET5_0
    public static bool operator ==(MomentUtc a, MomentUtc b)
      => a.Equals(b);
    public static bool operator !=(MomentUtc a, MomentUtc b)
      => !a.Equals(b);
#endif
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

#if NET5_0
    // IEquatable<>
    public bool Equals(MomentUtc other)
      => m_year == other.m_year
      && m_month == other.m_month
      && m_day == other.m_day
      && m_hour == other.m_hour
      && m_minute == other.m_minute
      && m_second == other.m_second
      && m_millisecond == other.m_millisecond;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is MomentUtc o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_year, m_month, m_day, m_hour, m_minute, m_second, m_millisecond);
#endif
    public override string? ToString()
      => $"{GetType().Name} {{ {m_year:D4}-{m_month:D2}-{m_day:D2} {m_hour:D2}:{m_minute:D2}:{m_second:D2}.{m_millisecond:D3} }}";
    #endregion Object overrides
  }
}
