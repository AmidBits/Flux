namespace Flux
{
  public static partial class MomentUtcEm
  {
    /// <summary>Returns the approximate number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
    public static double GetTotalApproximateSeconds(this MomentUtc source)
      => System.Math.CopySign(System.Math.Abs(source.Year) * 31536000L + source.Month * 2628000L + source.Day * 86400L + source.Hour * 3600L + source.Minute * 60L + source.Second + source.Millisecond / 1000.0, source.Year);

    public static MomentUtc ToMomentUtc(this System.DateTime source)
      => new(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, source.Millisecond);
  }

  /// <summary>A moment is a specific point in time down to the millisecond.</summary>
  public struct MomentUtc
    : System.IComparable<MomentUtc>, System.IEquatable<MomentUtc>
  {
    public static readonly MomentUtc Empty;

    private readonly int m_year;
    private readonly byte m_month;
    private readonly byte m_day;
    private readonly byte m_hour;
    private readonly byte m_minute;
    private readonly byte m_second;
    private readonly short m_millisecond;

    public MomentUtc(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
      m_year = year >= -4712 ? year : throw new System.ArgumentOutOfRangeException(nameof(year));
      m_month = month >= 1 && month <= 12 ? (byte)month : throw new System.ArgumentOutOfRangeException(nameof(month));
      m_day = day >= 1 && day <= 31 ? (byte)day : throw new System.ArgumentOutOfRangeException(nameof(day));
      m_hour = hour >= 0 && hour < 24 ? (byte)hour : throw new System.ArgumentOutOfRangeException(nameof(hour));
      m_minute = minute >= 0 && minute <= 59 ? (byte)minute : throw new System.ArgumentOutOfRangeException(nameof(minute));
      m_second = second >= 0 && second <= 59 ? (byte)second : throw new System.ArgumentOutOfRangeException(nameof(second));
      m_millisecond = millisecond >= 0 && millisecond <= 999 ? (byte)millisecond : throw new System.ArgumentOutOfRangeException(nameof(millisecond));
    }
    public MomentUtc(int year, int month, int day, int hour, int minute, int second)
      : this(year, month, day, hour, minute, second, 0)
    { }
    public MomentUtc(int year, int month, int day)
      : this(year, month, day, 0, 0, 0, 0)
    { }

    [System.Diagnostics.Contracts.Pure]
    public int Year
      => m_year;
    [System.Diagnostics.Contracts.Pure]
    public int Month
      => m_month;
    [System.Diagnostics.Contracts.Pure]
    public int Day
      => m_day;
    [System.Diagnostics.Contracts.Pure]
    public int Hour
      => m_hour;
    [System.Diagnostics.Contracts.Pure]
    public int Minute
      => m_minute;
    [System.Diagnostics.Contracts.Pure]
    public int Second
      => m_second;
    [System.Diagnostics.Contracts.Pure]
    public int Millisecond
      => m_millisecond;

    [System.Diagnostics.Contracts.Pure]
    public ConversionCalendar GetConversionCalendar()
      => IsGregorianCalendar(m_year, m_month, m_day) ? ConversionCalendar.GregorianCalendar : IsJulianCalendar(m_year, m_month, m_day) ? ConversionCalendar.JulianCalendar : throw new System.NotImplementedException(@"Not a Julian/Gregorian Calendar date.");

#if NET6_0_OR_GREATER
    /// <summary>Creates a new <see cref="System.DateOnly"/> from the date components in this instance.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.DateOnly ToDateOnly()
      => new(m_year, m_month, m_day);
#endif

    /// <summary>Creates a new <see cref="System.DateTime"/> from all components in this instance.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.DateTime ToDateTime()
      => new(m_year, m_month, m_day, m_hour, m_minute, m_second, m_millisecond);

#if NET6_0_OR_GREATER
    /// <summary>Creates a new <see cref="System.TimeOnly"/> from the time components in this instance.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.TimeOnly ToTimeOnly()
      => new(m_hour, m_minute, m_second, m_millisecond);
#endif

    /// <summary>Creates a new <see cref="System.TimeSpan"/> from the day and all time components in this instance.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.TimeSpan ToTimeSpan()
      => new(m_day, m_hour, m_minute, m_second, m_millisecond);

    /// <summary>Creates a new <see cref="JulianDate"/> from this instance. Uses the specified conversion calendar.</summary>
    [System.Diagnostics.Contracts.Pure]
    public JulianDate ToJulianDate(ConversionCalendar calendar)
      => new(m_year, m_month, m_day, m_hour, m_minute, m_second, m_millisecond, calendar);
    /// <summary>Creates a new <see cref="JulianDate"/> from this instance. Uses the default conversion calendar.</summary>
    [System.Diagnostics.Contracts.Pure]
    public JulianDate ToJulianDate()
      => ToJulianDate(GetConversionCalendar());

    /// <summary>Creates a new <see cref="JulianDayNumber"/> from this instance. Uses the specified conversion calendar.</summary>
    [System.Diagnostics.Contracts.Pure]
    public JulianDayNumber ToJulianDayNumber(ConversionCalendar calendar)
      => new(m_year, m_month, m_day, calendar);
    /// <summary>Creates a new <see cref="JulianDayNumber"/> from this instance. Uses the default conversion calendar.</summary>
    [System.Diagnostics.Contracts.Pure]
    public JulianDayNumber ToJulianDayNumber()
      => ToJulianDayNumber(GetConversionCalendar());

    #region Static methods
    /// <summary>Returns whether the date is considered to be in the modern Gregorian Calendar.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsGregorianCalendar(int year, int month, int day)
      => year > 1582 || (year == 1582 && (month > 10 || (month == 10 && day >= 15)));
    /// <summary>Returns whether the date is considered to be in the traditional Julian Calendar.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsJulianCalendar(int year, int month, int day)
      => year >= -4712 && year < 1582 || (year == 1582 && (month < 10 || (month == 10 && day <= 4)));

    /// <summary>Returns whether the date is a valid date in the Gregorian calendar.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsValidGregorianCalendarDate(int year, int month, int day)
      => IsGregorianCalendar(year, month, day) && month >= 1 && month <= 12 && day >= 1 && day <= System.DateTime.DaysInMonth(year, month);
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator <(MomentUtc a, MomentUtc b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(MomentUtc a, MomentUtc b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(MomentUtc a, MomentUtc b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(MomentUtc a, MomentUtc b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(MomentUtc a, MomentUtc b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(MomentUtc a, MomentUtc b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure]
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
    [System.Diagnostics.Contracts.Pure]
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
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is MomentUtc o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_year, m_month, m_day, m_hour, m_minute, m_second, m_millisecond);
    [System.Diagnostics.Contracts.Pure] public override string? ToString() => $"{GetType().Name} {{ {m_year:D4}-{m_month:D2}-{m_day:D2} {m_hour:D2}:{m_minute:D2}:{m_second:D2}.{m_millisecond:D3} }}";
    #endregion Object overrides
  }
}
