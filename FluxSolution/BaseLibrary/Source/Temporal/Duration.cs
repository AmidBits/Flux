namespace Flux
{
  public static partial class Em
  {
    //  /// <summary>Returns the approximate number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
    //  public static long GetTotalApproximateMilliseconds(this Temporal.Moment source)
    //    => (source.Year * 31536000L + source.Month * 2628000L + source.Day * 86400L + source.Hour * 3600L + source.Minute * 60L + source.Second) * 1000L + source.Millisecond;

    /// <summary>Returns the approximate number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
    public static double GetTotalSeconds(this Temporal.Duration source)
    => double.CopySign(double.Abs(source.Years) * Temporal.Duration.AverageSecondsInYear + source.Months * Temporal.Duration.AverageSecondsInMonth + source.Days * Temporal.Duration.SecondsInDay + source.Hours * Temporal.Duration.HoursInDay + source.Minutes * Temporal.Duration.SecondsInMinute + source.Seconds + source.Fractions, source.Years);

    //  public static Temporal.Moment ToMomentUtc(this System.DateTime source)
    //    => new(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, (short)source.Millisecond);
  }

  namespace Temporal
  {
    /// <summary>
    /// <para>A moment is an specific composite (year, month, day, hour, minute second, millisecond, microsecond, nanosecond) timestamp.</para>
    /// </summary>
    /// <remarks>The <see cref="Year"/> component is the only component that can be negative.</remarks>
    public readonly record struct Duration
      : System.IComparable, System.IComparable<Duration>, System.IFormattable
    {
      public const double AverageDaysInYear = 365.25;

      public const double AverageSecondsInYear = SecondsInMinute * MinutesInHour * HoursInDay * AverageDaysInYear; // 31536000;
      public const double AverageSecondsInMonth = AverageSecondsInYear / MonthsInYear; // 2628000;

      public const int DaysInWeek = 7;

      public const int HoursInDay = 24;

      public const int MinutesInHour = 60;

      public const int MonthsInYear = 12;

      public const int SecondsInWeek = SecondsInMinute * MinutesInHour * HoursInDay * DaysInWeek;
      public const int SecondsInDay = SecondsInMinute * MinutesInHour * HoursInDay;
      public const int SecondsInHour = SecondsInMinute * MinutesInHour;

      public const int SecondsInMinute = 60;

      private readonly int m_years;
      private readonly int m_months;
      private readonly int m_days;
      private readonly int m_hours;
      private readonly int m_minutes;
      private readonly int m_seconds;
      private readonly double m_fractions;

      public Duration(int years, int months, int days, int hours, int minutes, int seconds, double fractions)
      {
        m_years = years;
        m_months = months;
        m_days = days;
        m_hours = hours;
        m_minutes = minutes;
        m_seconds = seconds;
        m_fractions = fractions;
      }

      /// <summary>
      /// <para>The year component of the <see cref="Duration"/> struct.</para>
      /// </summary>
      /// <remarks>This is the only component that can be negative.</remarks>
      public int Years => m_years;

      /// <summary>
      /// <para>The year component of the <see cref="Duration"/> struct.</para>
      /// </summary>
      public int Months => m_months;

      /// <summary>
      /// <para>The year component of the <see cref="Duration"/> struct.</para>
      /// </summary>
      public int Days => m_days;

      /// <summary>
      /// <para>The year component of the <see cref="Duration"/> struct.</para>
      /// </summary>
      public int Hours => m_hours;

      /// <summary>
      /// <para>The year component of the <see cref="Duration"/> struct.</para>
      /// </summary>
      public int Minutes => m_minutes;

      /// <summary>
      /// <para>The year component of the <see cref="Duration"/> struct.</para>
      /// </summary>
      public int Seconds => m_seconds;

      /// <summary>
      /// <para>The year component of the <see cref="Duration"/> struct.</para>
      /// </summary>
      public double Fractions => m_fractions;

      public void Deconstruct(out int years, out int months, out int days, out int hours, out int minutes, out int seconds, out double fractions)
      {
        years = m_years;
        months = m_months;
        days = m_days;
        hours = m_hours;
        minutes = m_minutes;
        seconds = m_seconds;
        fractions = m_fractions;
      }

      /// <summary>Creates a new <see cref="System.TimeSpan"/> from the day and all time components in this instance.</summary>
      public System.TimeSpan TimeSpan => System.DateTime.MinValue.AddYears(m_years).AddMonths(m_months).AddDays(m_days).AddHours(m_hours).AddMinutes(m_minutes).AddSeconds(m_seconds + m_fractions).Subtract(System.DateTime.MinValue);

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

      public static bool operator <(Duration a, Duration b) => a.CompareTo(b) < 0;
      public static bool operator <=(Duration a, Duration b) => a.CompareTo(b) <= 0;
      public static bool operator >(Duration a, Duration b) => a.CompareTo(b) > 0;
      public static bool operator >=(Duration a, Duration b) => a.CompareTo(b) >= 0;

      #endregion // Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? obj) => obj is Duration mutc ? CompareTo(mutc) : -1;

      // IComparable<>
      public int CompareTo(Duration other)
        => this.GetTotalSeconds() is var thisTotalSeconds && other.GetTotalSeconds() is var otherTotalSeconds && thisTotalSeconds < otherTotalSeconds ? -1 : thisTotalSeconds > otherTotalSeconds ? 1
        : 0; // This means this instance is equal to the other.

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider)
        => $"P{m_years:D4}Y{m_months:D2}M{m_days:D2}DT{m_hours:D2}H{m_minutes:D2}M{m_seconds:D2}{m_fractions}S";

      #endregion // Implemented interfaces

      public override string? ToString() => ToString(null, null);
    }
  }
}
