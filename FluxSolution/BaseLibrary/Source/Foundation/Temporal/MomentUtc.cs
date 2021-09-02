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
      : this(year, month, day, 0, 0, 0)
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

    /// <summary>Dates on or after 15 Oct 1582 are considered to be Gregorian dates. Any date in the 10 day gap is invalid in the Gregorian calendar.</summary>
    public bool InGregorianCalendar
      => InGregorianCalendarEra(m_year, m_month, m_day);
    /// <summary>Dates on or before 4 Oct 1582 are considered to be Julian dates, though this calendard are still in use today. Any date in the 10 day gap is invalid in the Gregorian calendar.</summary>
    public bool InJulianCalendar
      => InJulianCalendarEra(m_year, m_month, m_day);

    public Quantity.Time TotalSeconds
      => new Quantity.Time(System.Math.CopySign(System.Math.Abs(m_year) * 31536000L + m_month * 2628000L + m_day * 86400L + m_hour * 3600L + m_minute * 60L + m_second + m_millisecond / 1000.0, m_year));

    /// <summary>Compute a Julian Date (JD) from this instance and the specified calendar.</summary>
    public double GetJulianDate(ConversionCalendar calendar)
       => ComputeJulianDate(m_year, m_month, m_day, m_hour, m_minute, m_second, m_millisecond, calendar);
    /// <summary>Compute a Julian Day Number (JDN) from this instance and the specified calendar.</summary>
    public int GetJulianDayNumber(ConversionCalendar calendar)
      => ComputeJulianDayNumber(m_year, m_month, m_day, calendar);

    /// <summary>Create a new <see cref="JulianDate"/> from this instance.</summary>
    public JulianDate ToJulianDate(ConversionCalendar calendar)
      => new JulianDate(GetJulianDate(calendar));

    #region Static methods
    /// <summary>Computes the Julian Date (JD) "time-of-day" fraction for the specified time components.</summary>
    public static double ComputeJulianDateTimeOfDay(int hour, int minute, int second, int millisecond)
      => hour / 24.0 + minute / 1440.0 + (second + millisecond / 1000.0) / 86400;
    /// <summary>Computes the Julian Date (JD) for the specified date/time components and calendar to use during conversion.</summary>
    public static double ComputeJulianDate(int year, int month, int day, int hour, int minute, int second, int millisecond, ConversionCalendar calendar)
      => ComputeJulianDayNumber(year, month, day, calendar) + ComputeJulianDateTimeOfDay(hour, minute, second, millisecond);
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

    /// <summary>Returns whether the date is considered to be in the Gregorian Calendar.</summary>
    public static bool InGregorianCalendarEra(int year, int month, int day)
      => year > 1582 || (year == 1582 && (month > 10 || (month == 10 && day >= 15)));
    /// <summary>Returns whether the date is considered to be in the traditional Julian Calendar.</summary>
    public static bool InJulianCalendarEra(int year, int month, int day)
      => year < 1582 || (year == 1582 && (month < 10 || (month == 10 && day <= 4)));

    public static bool IsValidGregorianCalendarDate(int year, int month, int day)
      => InGregorianCalendarEra(year, month, day) && month >= 1 && month <= 12 && day >= 1 && day <= System.DateTime.DaysInMonth(year, month);
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
    // IComparable
    public int CompareTo(MomentUtc other)
      => TotalSeconds is var ts && other.TotalSeconds is var ots && ts < ots ? -1 : ts > ots ? 1 : 0;

    // IEquatable
    public bool Equals(MomentUtc other)
      => m_year == other.m_year && m_month == other.m_month && m_day == other.m_day && m_hour == other.m_hour && m_minute == other.m_minute && m_second == other.m_second && m_millisecond == other.m_millisecond;
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
