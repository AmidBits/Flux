namespace Flux
{
  public static partial class Em
  {
    /// <summary>Returns the approximate number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
    public static long GetTotalApproximateNanoseconds(this Units.TimestampUtc source)
      => (source.Year * 31536000L + source.Month * 2628000L + source.Day * 86400L + source.Hour * 3600L + source.Minute * 60L + source.Second) * 1000000000L + source.Nanosecond;

    /// <summary>Returns the approximate number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
    public static double GetTotalApproximateSeconds(this Units.TimestampUtc source)
      => System.Math.CopySign(System.Math.Abs(source.Year) * 31536000L + source.Month * 2628000L + source.Day * 86400L + source.Hour * 3600L + source.Minute * 60L + source.Second + source.Nanosecond / 1e9, source.Year);

    public static Units.TimestampUtc ToTimestampUtc(this System.DateTime source)
      => new(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, source.Millisecond * 1000000);
  }

  namespace Units
  {
    /// <summary>A <see cref="TimestampUtc"/> is a specific point in time down to the nanosecond.</summary>
    public readonly record struct TimestampUtc
    {
      private readonly short m_year;
      private readonly byte m_month;
      private readonly byte m_day;
      private readonly byte m_hour;
      private readonly byte m_minute;
      private readonly byte m_second;
      private readonly int m_nanosecond;

      public TimestampUtc(int year, int month, int day, int hour, int minute, int second, int nanosecond)
      {
        m_year = year >= -4712 ? (short)year : throw new System.ArgumentOutOfRangeException(nameof(year));
        m_month = month >= 1 && month <= 12 ? (byte)month : throw new System.ArgumentOutOfRangeException(nameof(month));
        m_day = day >= 1 && day <= 31 ? (byte)day : throw new System.ArgumentOutOfRangeException(nameof(day));
        m_hour = hour >= 0 && hour < 24 ? (byte)hour : throw new System.ArgumentOutOfRangeException(nameof(hour));
        m_minute = minute >= 0 && minute <= 59 ? (byte)minute : throw new System.ArgumentOutOfRangeException(nameof(minute));
        m_second = second >= 0 && second <= 59 ? (byte)second : throw new System.ArgumentOutOfRangeException(nameof(second));
        m_nanosecond = nanosecond >= 0 && nanosecond <= int.MaxValue ? nanosecond : throw new System.ArgumentOutOfRangeException(nameof(nanosecond));
      }
      public TimestampUtc(int year, int month, int day, int hour, int minute, int second) : this(year, month, day, hour, minute, second, 0) { }
      public TimestampUtc(int year, int month, int day) : this(year, month, day, 0, 0, 0, 0) { }

      public int Year => m_year;
      public int Month => m_month;
      public int Day => m_day;
      public int Hour => m_hour;
      public int Minute => m_minute;
      public int Second => m_second;
      public int Nanosecond => m_nanosecond;

      public void Deconstruct(out int year, out int month, out int day, out int hour, out int minute, out int second, out long nanosecond)
      {
        year = m_year;
        month = m_month;
        day = m_day;
        hour = m_hour;
        minute = m_minute;
        second = m_second;
        nanosecond = m_nanosecond;
      }

      public TemporalCalendar GetConversionCalendar()
        => IsGregorianCalendar(m_year, m_month, m_day)
        ? TemporalCalendar.GregorianCalendar
        : IsJulianCalendar(m_year, m_month, m_day)
        ? TemporalCalendar.JulianCalendar
        : throw new System.NotImplementedException(@"Not a Julian/Gregorian Calendar date.");

      /// <summary>Creates a new <see cref="System.DateOnly"/> from the date components in this instance.</summary>
      public System.DateOnly ToDateOnly() => new(m_year, m_month, m_day);

      /// <summary>Creates a new <see cref="System.DateTime"/> from all components in this instance.</summary>
      public System.DateTime ToDateTime() => new(m_year, m_month, m_day, m_hour, m_minute, m_second, System.Convert.ToInt32(Units.Time.ConvertNanosecondToMillisecond(m_nanosecond)));

      /// <summary>Creates a new <see cref="JulianDate"/> from this instance. Uses the specified conversion calendar.</summary>
      public JulianDate ToJulianDate(TemporalCalendar calendar) => new(m_year, m_month, m_day, m_hour, m_minute, m_second, System.Convert.ToInt32(Units.Time.ConvertNanosecondToMillisecond(m_nanosecond)), calendar);

      /// <summary>Creates a new <see cref="JulianDate"/> from this instance. Uses the default conversion calendar.</summary>
      public JulianDate ToJulianDate() => ToJulianDate(GetConversionCalendar());

      /// <summary>Creates a new <see cref="JulianDayNumber"/> from this instance. Uses the specified conversion calendar.</summary>
      public JulianDayNumber ToJulianDayNumber(TemporalCalendar calendar) => new(m_year, m_month, m_day, calendar);

      /// <summary>Creates a new <see cref="JulianDayNumber"/> from this instance. Uses the default conversion calendar.</summary>
      public JulianDayNumber ToJulianDayNumber() => ToJulianDayNumber(GetConversionCalendar());

      /// <summary>Creates a new <see cref="JulianDate"/> from this instance. Uses the specified conversion calendar.</summary>
      public MomentUtc ToMomentUtc() => new(m_year, m_month, m_day, m_hour, m_minute, m_second, System.Convert.ToInt32(Units.Time.ConvertNanosecondToMillisecond(m_nanosecond)));

      /// <summary>Creates a new <see cref="System.TimeOnly"/> from the time components in this instance.</summary>
      public System.TimeOnly ToTimeOnly() => new(m_hour, m_minute, m_second, System.Convert.ToInt32(Units.Time.ConvertNanosecondToMillisecond(m_nanosecond)));

      /// <summary>Creates a new <see cref="System.TimeSpan"/> from the day and all time components in this instance.</summary>
      public System.TimeSpan ToTimeSpan() => new(m_day, m_hour, m_minute, m_second, System.Convert.ToInt32(Units.Time.ConvertNanosecondToMillisecond(m_nanosecond)));

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

      #endregion // Static methods

      #region Overloaded operators

      public static bool operator <(TimestampUtc a, TimestampUtc b) => a.CompareTo(b) < 0;
      public static bool operator <=(TimestampUtc a, TimestampUtc b) => a.CompareTo(b) <= 0;
      public static bool operator >(TimestampUtc a, TimestampUtc b) => a.CompareTo(b) > 0;
      public static bool operator >=(TimestampUtc a, TimestampUtc b) => a.CompareTo(b) >= 0;

      #endregion // Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? obj) => obj is TimestampUtc mutc ? CompareTo(mutc) : -1;

      // IComparable<>
      public int CompareTo(TimestampUtc other)
        => m_year < other.m_year ? -1 : m_year > other.m_year ? 1
        : m_month < other.m_month ? -1 : m_month > other.m_month ? 1
        : m_day < other.m_day ? -1 : m_day > other.m_day ? 1
        : m_hour < other.m_hour ? -1 : m_hour > other.m_hour ? 1
        : m_minute < other.m_minute ? -1 : m_minute > other.m_minute ? 1
        : m_second < other.m_second ? -1 : m_second > other.m_second ? 1
        : m_nanosecond < other.m_nanosecond ? -1 : m_nanosecond > other.m_nanosecond ? 1
        : 0; // This means this instance is equal to the other.

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{m_year:D4}-{m_month:D2}-{m_day:D2} {m_hour:D2}:{m_minute:D2}:{m_second:D2}.{m_nanosecond:D9}";

      #endregion // Implemented interfaces

      public override string? ToString() => ToString(null, null);
    }
  }
}
