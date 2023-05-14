namespace Flux
{
  //  public static partial class MomentUtcExtensionMethods
  //  {
  //    /// <summary>Returns the approximate number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
  //    public static double GetTotalApproximateSeconds(this MomentUtc source)
  //      => System.Math.CopySign(System.Math.Abs(source.Year) * 31536000L + source.Month * 2628000L + source.Day * 86400L + source.Hour * 3600L + source.Minute * 60L + source.Second + source.Millisecond / 1000.0, source.Year);

  //    public static MomentUtc ToMomentUtc(this System.DateTime source)
  //      => new(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, source.Millisecond);
  //  }

  /// <summary>A moment is a specific point in time down to the millisecond.</summary>
  public readonly record struct MomentDate
    : System.IComparable<MomentDate>
  {
    private readonly int m_year;
    private readonly byte m_month;
    private readonly byte m_day;
    private readonly TemporalCalendar m_calendar;

    public MomentDate(int year, byte month, byte day, TemporalCalendar calendar)
    {
      m_year = year;
      m_month = month;
      m_day = day;
      m_calendar = calendar;
    }

    public int Year => m_year;
    public int Month => m_month;
    public int Day => m_day;
    public TemporalCalendar Calendar => m_calendar;

    /// <summary>Creates a new <see cref="System.DateOnly"/> from the date components in this instance.</summary>
    public System.DateOnly ToDateOnly() => new(m_year, m_month, m_day);

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
    public static bool operator <(MomentDate a, MomentDate b) => a.CompareTo(b) < 0;
    public static bool operator <=(MomentDate a, MomentDate b) => a.CompareTo(b) <= 0;
    public static bool operator >(MomentDate a, MomentDate b) => a.CompareTo(b) > 0;
    public static bool operator >=(MomentDate a, MomentDate b) => a.CompareTo(b) >= 0;
    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(MomentDate other)
      => m_year < other.m_year ? -1 : m_year > other.m_year ? 1
      : m_month < other.m_month ? -1 : m_month > other.m_month ? 1
      : m_day < other.m_day ? -1 : m_day > other.m_day ? 1
      : m_calendar < other.m_calendar ? -1 : m_calendar > other.m_calendar ? 1
      : 0; // This means this instance is equal to the other.

    #endregion Implemented interfaces
  }
}
