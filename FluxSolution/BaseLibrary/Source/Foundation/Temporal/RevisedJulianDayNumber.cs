namespace Flux
{
  public static partial class ExtensionMethods
  {
    //public static RevisedJulianDate ToRevisedJulianDate(this System.DateTime source, ConversionCalendar calendar)
    //  => ToMomentUtc(source).ToJulianDate(calendar);
  }

  /// <see cref="https://en.wikipedia.org/wiki/Revised_Julian_calendar"/>
  public struct RevisedJulianDayNumber
    : System.IComparable<RevisedJulianDayNumber>, System.IEquatable<RevisedJulianDayNumber>, IValueGeneralizedUnit<int>
  {
    public const int Epoch = 1;

    public readonly static RevisedJulianDayNumber Zero;

    private readonly int m_value;

    /// <summary>Create a Julian Date (JD) from the specified <paramref name="value"/> value.</summary>
    public RevisedJulianDayNumber(int value)
      => m_value = value;
    /// <summary>Computes the Julian Date (JD) for the specified date/time components and calendar to use during conversion.</summary>
    //public RevisedJulianDate(int year, int month, int day, int hour, int minute, int second, int millisecond, ConversionCalendar calendar)
    //  : this(JulianDayNumber.ConvertFromDateParts(year, month, day, calendar) + ConvertFromTimeParts(hour, minute, second, millisecond))
    //{ }

    public int Value
      => m_value;

    //public RevisedJulianDate AddWeeks(int weeks)
    //  => this + (weeks * 7);
    //public RevisedJulianDate AddDays(int days)
    //  => this + days;
    //public RevisedJulianDate AddHours(int hours)
    //  => this + (hours / 24.0m);
    //public RevisedJulianDate AddMinutes(int minutes)
    //  => this + (minutes / 1440.0m);
    //public RevisedJulianDate AddSeconds(int seconds)
    //  => this + (seconds / 86400.0m);
    //public RevisedJulianDate AddMilliseconds(int milliseconds)
    //  => this + (milliseconds / 1000.0m / 86400m);

    //public ConversionCalendar GetConversionCalendar()
    //  => IsGregorianCalendar(m_value) ? ConversionCalendar.GregorianCalendar : ConversionCalendar.JulianCalendar;

    //public void GetParts(ConversionCalendar calendar, out int year, out int month, out int day, out int hour, out int minute, out int second, out int millisecond)
    //{
    //  ToJulianDayNumber().GetDateParts(calendar, out year, out month, out day);
    //  ConvertToTimeParts(m_value, out hour, out minute, out second, out millisecond);
    //}

    //public JulianDayNumber ToJulianDayNumber()
    //  => new((int)(m_value + 0.5m));

    //public MomentUtc ToMomentUtc(ConversionCalendar calendar)
    //{
    //  ToJulianDayNumber().GetDateParts(calendar, out var year, out var month, out var day);
    //  ConvertToTimeParts(m_value, out var hour, out var minute, out var second, out var millisecond);

    //  return new(year, month, day, hour, minute, second, millisecond);
    //}

    //public string ToTimeString()
    //  => System.TimeSpan.FromSeconds(System.Convert.ToDouble(43200m + GetTimeSinceNoon(m_value))).ToString(@"hh\:mm\:ss"); // Add 12 hours (in seconds) to the julian date time-of-day value for time strings, because of the 12 noon day cut-over convention in Julian Date values.

    #region Static methods
    public static bool IsLeapYear(int year)
    {
      var ly = year % 4 == 0;
      if (ly && year % 100 == 0 && (year / 100) % 9 is var century)
        ly = century == 2 || century == 6;
      return ly;
    }

    public static (int year, int month, int day) FromFixedDayNumber(int fixedDayNumber)
    {
      var days = fixedDayNumber - Epoch + 1;
      var priorCenturies = days / 36524;
      var remainingDays = days - 36524 * priorCenturies - (2 * priorCenturies + 6) / 9;
      var priorSubcycles = remainingDays / 1461;
      remainingDays %= 1461;
      var priorSubcycleYears = remainingDays / 365;
      var year = 100 * priorCenturies + 4 * priorSubcycles + priorSubcycleYears;
      remainingDays %= 365;
      if (remainingDays == 0)
      {
        year -= 1;
        remainingDays = IsLeapYear(year) && priorSubcycles == 0 ? 366 : 365;
      }
      var priorDays = remainingDays - 1;
      var correction = IsLeapYear(year) ? 1 : 0;
      correction = (priorDays < (31 + 28 + correction)) ? 0 : 2 - correction;
      var month = (12 * (priorDays + correction) + 373) / 367;
      var day = fixedDayNumber - ToFixedDayNumber(year, month, 1) + 1;
      return (year, month, day);
    }

    public static int ToFixedDayNumber(int year, int month, int day)
    {
      var priorYear = year - 1;
      var fixedDayNumber = Epoch + 365 * priorYear + (priorYear / 4) + (367 / month - 362) / 12 + day - 1;
      if (month > 2)
        fixedDayNumber -= IsLeapYear(year) ? 1 : 2;
      var priorCenturies = priorYear / 100;
      fixedDayNumber -= priorCenturies + (2 * priorCenturies + 6) / 9;
      return fixedDayNumber;
    }

    public static int WeekdayNumber(int fixedDayNumber)
      => (fixedDayNumber - Epoch + 1) % 7 + 1;

    ///// <summary>Converts the time components to a Julian Date (JD) "time-of-day" fraction value. This is not the same as the number of seconds.</summary>
    //public static decimal ConvertFromTimeParts(int hour, int minute, int second, int millisecond)
    //  => (hour - 12) / 24.0m + minute / 1440.0m + (second + millisecond / 1000.0m) / 86400m;
    ///// <summary>Converts the Julian Date (JD) to discrete time components. This method is only concerned with the time portion of the Julian Date.</summary>
    //public static void ConvertToTimeParts(decimal julianDate, out int hour, out int minute, out int second, out int millisecond)
    //{
    //  var totalSeconds = GetTimeSinceNoon(julianDate);

    //  if (totalSeconds <= 43200)
    //    totalSeconds = (totalSeconds + 43200) % 86400;

    //  hour = (int)(totalSeconds / 3600);
    //  totalSeconds -= hour * 3600;

    //  minute = (int)(totalSeconds / 60);
    //  totalSeconds -= minute * 60;

    //  second = (int)totalSeconds;
    //  totalSeconds -= second;

    //  millisecond = (int)totalSeconds;
    //}

    ///// <summary>Compute the time-of-day. I.e. the number of seconds from 12 noon of the Julian Day Number part.</summary>
    //public static decimal GetTimeSinceNoon(decimal julianDate)
    //  => julianDate.GetFraction() * 86400m;

    ///// <summary>Returns whether the Julian Date value (JD) is considered to be on the Gregorian Calendar.</summary>
    //public static bool IsGregorianCalendar(decimal julianDate)
    //  => julianDate >= 2299160.5m;
    #endregion Static methods

    #region Overloaded operators
    public static bool operator <(RevisedJulianDayNumber a, RevisedJulianDayNumber b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(RevisedJulianDayNumber a, RevisedJulianDayNumber b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(RevisedJulianDayNumber a, RevisedJulianDayNumber b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(RevisedJulianDayNumber a, RevisedJulianDayNumber b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(RevisedJulianDayNumber a, RevisedJulianDayNumber b)
      => a.Equals(b);
    public static bool operator !=(RevisedJulianDayNumber a, RevisedJulianDayNumber b)
      => !a.Equals(b);

    public static RevisedJulianDayNumber operator -(RevisedJulianDayNumber jd)
      => new(-jd.m_value);
    public static decimal operator -(RevisedJulianDayNumber a, RevisedJulianDayNumber b)
      => a.m_value - b.m_value;

    public static RevisedJulianDayNumber operator +(RevisedJulianDayNumber a, int b)
      => new(a.m_value + b);
    public static RevisedJulianDayNumber operator /(RevisedJulianDayNumber a, int b)
      => new(a.m_value / b);
    public static RevisedJulianDayNumber operator *(RevisedJulianDayNumber a, int b)
      => new(a.m_value * b);
    public static RevisedJulianDayNumber operator %(RevisedJulianDayNumber a, int b)
      => new(a.m_value % b);
    public static RevisedJulianDayNumber operator -(RevisedJulianDayNumber a, int b)
      => new(a.m_value - b);

    public static RevisedJulianDayNumber operator +(RevisedJulianDayNumber a, double b)
      => new(a.m_value + System.Convert.ToInt32(b));
    public static RevisedJulianDayNumber operator /(RevisedJulianDayNumber a, double b)
      => new(a.m_value / System.Convert.ToInt32(b));
    public static RevisedJulianDayNumber operator *(RevisedJulianDayNumber a, double b)
      => new(a.m_value * System.Convert.ToInt32(b));
    public static RevisedJulianDayNumber operator %(RevisedJulianDayNumber a, double b)
      => new(a.m_value % System.Convert.ToInt32(b));
    public static RevisedJulianDayNumber operator -(RevisedJulianDayNumber a, double b)
      => new(a.m_value - System.Convert.ToInt32(b));
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(RevisedJulianDayNumber other)
      => m_value < other.m_value ? -1 : m_value > other.m_value ? 1 : 0;

    // IEquatable
    public bool Equals(RevisedJulianDayNumber other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is RevisedJulianDayNumber o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string? ToString()
      => $"{GetType().Name} {{ {m_value} }}";
    #endregion Object overrides
  }
}
