namespace Flux
{
  public static partial class Em
  {
    /// <summary>Returns the approximate number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
    public static long GetTotalApproximateMilliseconds(this Temporal.Moment source)
      => (source.Year * 31536000L + source.Month * 2628000L + source.Day * 86400L + source.Hour * 3600L + source.Minute * 60L + source.Second) * 1000L + source.Millisecond;

    /// <summary>Returns the approximate number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
    public static double GetTotalApproximateSeconds(this Temporal.Moment source)
      => System.Math.CopySign(System.Math.Abs(source.Year) * 31536000L + source.Month * 2628000L + source.Day * 86400L + source.Hour * 3600L + source.Minute * 60L + source.Second + source.Millisecond / 1e3, source.Year);

    public static Temporal.Moment ToMomentUtc(this System.DateTime source)
      => new(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, (short)source.Millisecond);
  }

  namespace Temporal
  {
    /// <summary>
    /// <para>A moment is an specific composite (year, month, day, hour, minute second, millisecond, microsecond, nanosecond) timestamp.</para>
    /// </summary>
    /// <remarks>The <see cref="Year"/> component is the only component that can be negative.</remarks>
    public readonly record struct Moment
      : System.IComparable, System.IComparable<Moment>, System.IFormattable
    {
      private readonly short m_year;
      private readonly byte m_month;
      private readonly byte m_day;
      private readonly byte m_hour;
      private readonly byte m_minute;
      private readonly byte m_second;
      private readonly ushort m_millisecond;
      private readonly ushort m_microsecond;
      private readonly ushort m_nanosecond;

      public Moment(int year, int month, int day, int hour, int minute, int second, int millisecond, int microsecond, int nanosecond)
      {
        m_year = short.CreateChecked(year);
        m_month = byte.CreateChecked(month);
        m_day = byte.CreateChecked(day);
        m_hour = byte.CreateChecked(hour);
        m_minute = byte.CreateChecked(minute);
        m_second = byte.CreateChecked(second);
        m_millisecond = ushort.CreateChecked(millisecond);
        m_microsecond = ushort.CreateChecked(microsecond);
        m_nanosecond = ushort.CreateChecked(nanosecond);
      }
      public Moment(int year, int month, int day, int hour, int minute, int second, int millisecond, int microsecond) : this(year, month, day, hour, minute, second, millisecond, microsecond, 0) { }
      public Moment(int year, int month, int day, int hour, int minute, int second, int millisecond) : this(year, month, day, hour, minute, second, millisecond, 0, 0) { }
      public Moment(int year, int month, int day, int hour, int minute, int second) : this(year, month, day, hour, minute, second, 0, 0, 0) { }
      public Moment(int year, int month, int day)
      {
        m_year = short.CreateChecked(year);
        m_month = byte.CreateChecked(month);
        m_day = byte.CreateChecked(day);
      }

      /// <summary>
      /// <para>The year component of the <see cref="Moment"/> struct.</para>
      /// </summary>
      /// <remarks>This is the only component that can be negative.</remarks>
      public int Year => m_year;

      /// <summary>
      /// <para>The year component of the <see cref="Moment"/> struct.</para>
      /// </summary>
      public int Month => m_month;

      /// <summary>
      /// <para>The year component of the <see cref="Moment"/> struct.</para>
      /// </summary>
      public int Day => m_day;

      /// <summary>
      /// <para>The year component of the <see cref="Moment"/> struct.</para>
      /// </summary>
      public int Hour => m_hour;

      /// <summary>
      /// <para>The year component of the <see cref="Moment"/> struct.</para>
      /// </summary>
      public int Minute => m_minute;

      /// <summary>
      /// <para>The year component of the <see cref="Moment"/> struct.</para>
      /// </summary>
      public int Second => m_second;

      /// <summary>
      /// <para>The year component of the <see cref="Moment"/> struct.</para>
      /// </summary>
      public int Millisecond => m_millisecond;

      /// <summary>
      /// <para>The year component of the <see cref="Moment"/> struct.</para>
      /// </summary>
      public int Microsecond => m_microsecond;

      /// <summary>
      /// <para>The year component of the <see cref="Moment"/> struct.</para>
      /// </summary>
      public int Nanosecond => m_nanosecond;

      public void Deconstruct(out int year, out int month, out int day, out int hour, out int minute, out int second, out int millisecond, out int microsecond, out int nanosecond)
      {
        year = m_year;
        month = m_month;
        day = m_day;
        hour = m_hour;
        minute = m_minute;
        second = m_second;
        millisecond = m_millisecond;
        microsecond = m_microsecond;
        nanosecond = m_nanosecond;
      }

      public TemporalCalendar GetConversionCalendar() => TemporalCalendar.GregorianCalendar.Contains(m_year, m_month, m_day) ? TemporalCalendar.GregorianCalendar : TemporalCalendar.JulianCalendar;

      /// <summary>Returns whether the date is a valid date in the Gregorian calendar.</summary>
      public bool IsValidGregorianCalendarDate
         => TemporalCalendar.GregorianCalendar.Contains(m_year, m_month, m_day) && m_month >= 1 && m_month <= 12 && m_day >= 1 && m_day <= System.DateTime.DaysInMonth(m_year, m_month);

      public bool IsPossibleDateTime
        => m_year >= -4712
        && m_month >= 1 && m_month <= 12
        && m_day >= 1 && m_day <= 31
        && m_hour >= 0 && m_hour < 24
        && m_minute >= 0 && m_minute <= 59
        && m_second >= 0 && m_second <= 59
        && m_millisecond >= 0 && m_millisecond <= 999
        && m_microsecond >= 0 && m_microsecond <= 999
        && m_nanosecond >= 0 && m_nanosecond <= 999;

      /// <summary>Creates a new <see cref="System.DateOnly"/> from the date components in this instance.</summary>
      public System.DateOnly ToDateOnly() => new(m_year, m_month, m_day);

      /// <summary>Creates a new <see cref="System.DateTime"/> from all components in this instance.</summary>
      public System.DateTime ToDateTime() => new System.DateTime(m_year, m_month, m_day, m_hour, m_minute, m_second, m_millisecond, m_microsecond);

      /// <summary>Creates a new <see cref="JulianDate"/> from this instance. Uses the specified conversion calendar.</summary>
      public JulianDate ToJulianDate(TemporalCalendar calendar) => new(m_year, m_month, m_day, m_hour, m_minute, m_second, m_millisecond, m_microsecond, m_nanosecond, calendar);

      /// <summary>Creates a new <see cref="JulianDate"/> from this instance. Uses the default conversion calendar.</summary>
      public JulianDate ToJulianDate() => ToJulianDate(GetConversionCalendar());

      /// <summary>Creates a new <see cref="JulianDayNumber"/> from this instance. Uses the specified conversion calendar.</summary>
      public JulianDayNumber ToJulianDayNumber(TemporalCalendar calendar) => new(m_year, m_month, m_day, calendar);

      /// <summary>Creates a new <see cref="JulianDayNumber"/> from this instance. Uses the default conversion calendar.</summary>
      public JulianDayNumber ToJulianDayNumber() => ToJulianDayNumber(GetConversionCalendar());

      public Quantities.Time ToTime() => new(ConvertTimePartsToTotalSeconds(m_day, m_hour, m_minute, m_second, m_millisecond, m_microsecond, m_nanosecond));

      /// <summary>Creates a new <see cref="System.TimeOnly"/> from the time components in this instance.</summary>
      public System.TimeOnly ToTimeOnly() => new(m_hour, m_minute, m_second, m_millisecond, m_microsecond);

      /// <summary>Creates a new <see cref="System.TimeSpan"/> from the day and all time components in this instance.</summary>
      public System.TimeSpan ToTimeSpan() => new System.TimeSpan(m_day, m_hour, m_minute, m_second, m_millisecond, m_microsecond);

      #region Static methods

      #region Conversion methods

      /// <summary>
      /// <para>Convert all determinate time parts to total seconds.</para>
      /// </summary>
      /// <param name="day"></param>
      /// <param name="hour"></param>
      /// <param name="minute"></param>
      /// <param name="second"></param>
      /// <param name="millisecond"></param>
      /// <param name="microsecond"></param>
      /// <param name="nanosecond"></param>
      /// <returns></returns>
      /// <remarks>
      /// <para>The determinate parts are Day, Hour, Minute, Second, MilliSecond, MicroSecond and NanoSecond.</para>
      /// <para>All other parts are indeterminite and excluded, i.e. Year and Month.</para>
      /// </remarks>
      public double ConvertTimePartsToTotalSeconds(int day, int hour, int minute, int second, int millisecond, int microsecond, int nanosecond)
        => day * 86400 + hour * 3600 + minute * 60 + (second + millisecond / 1000d + microsecond / 1000000d + nanosecond / 1000000000d);

      #endregion // Conversion methods

      ///// <summary>Returns whether the date is considered to be in the modern Gregorian Calendar.</summary>
      //public static bool IsGregorianCalendar(int year, int month, int day)
      //  => year > 1582 || (year == 1582 && (month > 10 || (month == 10 && day >= 15)));

      ///// <summary>Returns whether the date is considered to be in the traditional Julian Calendar.</summary>
      //public static bool IsJulianCalendar(int year, int month, int day)
      //  => year >= -4712 && year < 1582 || (year == 1582 && (month < 10 || (month == 10 && day <= 4)));

      ///// <summary>Returns whether the date is a valid date in the Gregorian calendar.</summary>
      //public static bool IsValidGregorianCalendarDate(int year, int month, int day)
      //   => IsGregorianCalendar(year, month, day) && month >= 1 && month <= 12 && day >= 1 && day <= System.DateTime.DaysInMonth(year, month);

      #endregion // Static methods

      #region Overloaded operators

      public static bool operator <(Moment a, Moment b) => a.CompareTo(b) < 0;
      public static bool operator <=(Moment a, Moment b) => a.CompareTo(b) <= 0;
      public static bool operator >(Moment a, Moment b) => a.CompareTo(b) > 0;
      public static bool operator >=(Moment a, Moment b) => a.CompareTo(b) >= 0;

      #endregion // Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? obj) => obj is Moment mutc ? CompareTo(mutc) : -1;

      // IComparable<>
      public int CompareTo(Moment other)
        => m_year < other.m_year ? -1 : m_year > other.m_year ? 1
        : m_month < other.m_month ? -1 : m_month > other.m_month ? 1
        : m_day < other.m_day ? -1 : m_day > other.m_day ? 1
        : m_hour < other.m_hour ? -1 : m_hour > other.m_hour ? 1
        : m_minute < other.m_minute ? -1 : m_minute > other.m_minute ? 1
        : m_second < other.m_second ? -1 : m_second > other.m_second ? 1
        : m_millisecond < other.m_millisecond ? -1 : m_millisecond > other.m_millisecond ? 1
        : m_microsecond < other.m_microsecond ? -1 : m_microsecond > other.m_microsecond ? 1
        : m_nanosecond < other.m_nanosecond ? -1 : m_nanosecond > other.m_nanosecond ? 1
        : 0; // This means this instance is equal to the other.

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{m_year:D4}-{m_month:D2}-{m_day:D2} {m_hour:D2}:{m_minute:D2}:{m_second:D2}.{m_millisecond:D3}{m_microsecond:D3}{m_nanosecond:D3}";

      #endregion // Implemented interfaces

      public override string? ToString() => ToString(null, null);
    }
  }
}
