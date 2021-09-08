namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static JulianDayNumber ToJulianDayNumber(this System.DateTime source, ConversionCalendar calendar)
      => ToMomentUtc(source).ToJulianDayNumber(calendar);
  }

  /// <summary>Julian Date unit of days with time of day fraction.</summary>
  /// <remarks>Julian Day Number and Julian Date for this code, has nothing to do with the Julian Calendar. Functionality that compute on the Julian Calendar will have JulianCalendar in the name.</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public struct JulianDayNumber
    : System.IComparable<JulianDayNumber>, System.IEquatable<JulianDayNumber>, Quantity.IValuedUnit
  {
    public const int DaysInWeek = 7;

    public static JulianDayNumber FirstGregorianCalendarDate
       => new MomentUtc(1582, 10, 15).ToJulianDayNumber(ConversionCalendar.GregorianCalendar);
    public static JulianDayNumber FirstJulianCalendarDate
      => new JulianDayNumber(0);
    public static JulianDayNumber LastJulianCalendarDate
      => new MomentUtc(1582, 10, 4).ToJulianDayNumber(ConversionCalendar.JulianCalendar);

    public static readonly JulianDayNumber Zero;

    private readonly int m_value;

    /// <summary>Create a Julian Date (JD) from the specified <paramref name="value"/> value.</summary>
    public JulianDayNumber(int value)
      => m_value = value;
    /// <summary>Computes the Julian Day Number (JDN) for the specified date components and calendar to use during conversion.</summary>
    public JulianDayNumber(int year, int month, int day, ConversionCalendar calendar)
      : this(ConvertFromDateParts(year, month, day, calendar))
    { }

    public JulianDayNumber AddWeeks(int weeks)
      => new JulianDayNumber(m_value + weeks * DaysInWeek);
    public JulianDayNumber AddDays(int days)
      => new JulianDayNumber(m_value + days);

    /// <summary>Returns a <see cref="System.DayOfWeek"/> from the Julian Day Number.</summary>
    public System.DayOfWeek DayOfWeek
      => (System.DayOfWeek)(DayOfWeekIso % DaysInWeek);
    /// <summary>Returns a day of week [1 (Monday), 7 (Sunday)] from the specified Julian Day Number. Julian Day Number 0 was Monday. For US day-of-week numbering, simply do "ComputeDayOfWeekIso(JDN) % 7".</summary>
    public int DayOfWeekIso
      => (m_value % DaysInWeek is var dow && dow <= 0 ? dow + DaysInWeek : dow) + 1;

    public double Value
      => m_value;

    public void GetDateParts(ConversionCalendar calendar, out int year, out int month, out int day)
      => ConvertToDateParts(m_value, calendar, out year, out month, out day);

    public string ToDateString(ConversionCalendar calendar)
    {
      var sb = new System.Text.StringBuilder();

      // if (calendar == ConversionCalendar.JulianCalendar)
      {
        sb.Append(DayOfWeek);
        sb.Append(@", ");
      }

      ConvertToDateParts(m_value, calendar, out var year, out var month, out var day); // Add 0.5 to the julian date value for date strings, because of the 12 noon convention in a Julian Date.

      sb.Append(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month));
      sb.Append(' ');
      sb.Append(day);
      sb.Append(@", ");
      sb.Append(year);

      if (year <= 0)
      {
        sb.Append(@" (");
        sb.Append(System.Math.Abs(year) + 1);
        sb.Append(@" BC)");
      }

      return sb.ToString();
    }
    public JulianDate ToJulianDate()
      => new JulianDate(m_value);
    public MomentUtc ToMomentUtc(ConversionCalendar calendar)
    {
      ConvertToDateParts((int)(m_value + 0.5), calendar, out var year, out var month, out var day);

      return new MomentUtc(year, month, day);
    }

    #region Static methods
    /// <summary>Computes the Julian Day Number (JDN) for the specified date components and calendar to use during conversion.</summary>
    public static int ConvertFromDateParts(int year, int month, int day, ConversionCalendar calendar)
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
    /// <summary>Create a new MomentUtc from the specified Julian Day Number and conversion calendar.</summary>
    public static void ConvertToDateParts(int julianDayNumber, ConversionCalendar calendar, out int year, out int month, out int day)
    {
      if (julianDayNumber < -1401) throw new System.ArgumentOutOfRangeException(nameof(julianDayNumber), $"The algorithm can only convert Julian Date values greater than or equal to -1401.");

      var J = julianDayNumber;

      const int y = 4716;
      const int v = 3;
      const int j = 1401;
      const int u = 5;
      const int m = 2;
      const int s = 153;
      const int n = 12;
      const int w = 2;
      const int r = 4;
      const int B = 274277;
      const int p = 1461;
      const int C = -38;

      var f = J + j;
      if (calendar == ConversionCalendar.GregorianCalendar)
        f += (((4 * J + B) / 146097) * 3) / 4 + C;
      var e = r * f + v;
      var g = (e % p) / r;
      var h = u * g + w;

      day = (h % s) / u + 1;
      month = ((h / s + m) % n) + 1;
      year = (e / p) - y + (n + m - month) / n;
    }

    /// <summary>Computes the Julian Period (JP) from the specified cyclic indices in the year.</summary>
    /// <param name="solarCycle">That year's position in the 28-year solar cycle.</param>
    /// <param name="lunarCycle">That year's position in the 19-year lunar cycle.</param>
    /// <param name="indictionCycle">That year's position in the 15-year indiction cycle.</param>
    public static int GetJulianPeriod(int solarCycle, int lunarCycle, int indictionCycle)
      => (indictionCycle * 6916 + lunarCycle * 4200 + solarCycle * 4845) % (15 * 19 * 28) is var year && year > 4713 ? year - 4713 : year < 4714 ? -(4714 - year) : year;

    ///// <summary>Returns whether the Julian Date value (JD) is considered to be on the Gregorian Calendar.</summary>
    //public bool IsGregorianCalendar(int julianDayNumber)
    //  => julianDayNumber >= 2299161;
    #endregion Static methods

    #region Overloaded operators
    public static bool operator <(JulianDayNumber a, JulianDayNumber b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(JulianDayNumber a, JulianDayNumber b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(JulianDayNumber a, JulianDayNumber b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(JulianDayNumber a, JulianDayNumber b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(JulianDayNumber a, JulianDayNumber b)
      => a.Equals(b);
    public static bool operator !=(JulianDayNumber a, JulianDayNumber b)
      => !a.Equals(b);

    public static JulianDayNumber operator -(JulianDayNumber jd)
      => new JulianDayNumber(-jd.m_value);
    public static double operator -(JulianDayNumber a, JulianDayNumber b)
      => a.m_value - b.m_value;

    public static JulianDayNumber operator +(JulianDayNumber a, int b)
      => new JulianDayNumber(a.m_value + b);
    public static JulianDayNumber operator /(JulianDayNumber a, int b)
      => new JulianDayNumber(a.m_value / b);
    public static JulianDayNumber operator *(JulianDayNumber a, int b)
      => new JulianDayNumber(a.m_value * b);
    public static JulianDayNumber operator %(JulianDayNumber a, int b)
      => new JulianDayNumber(a.m_value % b);
    public static JulianDayNumber operator -(JulianDayNumber a, int b)
      => new JulianDayNumber(a.m_value - b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(JulianDayNumber other)
      => m_value < other.m_value ? -1 : m_value > other.m_value ? 1 : 0;

    // IEquatable
    public bool Equals(JulianDayNumber other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is JulianDayNumber o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string? ToString()
      => $"<{GetType().Name}: {m_value} " + (JulianDate.IsGregorianCalendar(m_value) ? $"({ToDateString(ConversionCalendar.GregorianCalendar)})" : $"({ToDateString(ConversionCalendar.JulianCalendar)})*");
    #endregion Object overrides
  }
}
