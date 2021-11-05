namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static JulianDayNumber ToJulianDayNumber(this System.DateTime source, ConversionCalendar calendar)
      => ToMomentUtc(source).ToJulianDayNumber(calendar);
  }

  /// <summary>Julian Day Number unit of days.</summary>
  /// <remarks>Julian Day Number is not related to the Julian Calendar. Functionality that compute on the Julian Calendar will have JulianCalendar in the name.</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public struct JulianDayNumber
    : System.IComparable<JulianDayNumber>, System.IEquatable<JulianDayNumber>, Quantity.IValuedUnit
  {
    public static readonly JulianDayNumber Epoch;

    private readonly int m_value;

    /// <summary>Create a Julian Date (JD) from the specified <paramref name="value"/> value.</summary>
    public JulianDayNumber(int value)
      => m_value = value;
    /// <summary>Computes the Julian Day Number (JDN) for the specified date components and calendar to use during conversion.</summary>
    public JulianDayNumber(int year, int month, int day, ConversionCalendar calendar)
      : this(ConvertFromDateParts(year, month, day, calendar))
    { }

    public JulianDayNumber AddWeeks(int weeks)
      => this + (weeks * 7);
    public JulianDayNumber AddDays(int days)
      => this + days;

    /// <summary>Returns a <see cref="System.DayOfWeek"/> from the Julian Day Number.</summary>
    public System.DayOfWeek DayOfWeek
      => (System.DayOfWeek)(GetDayOfWeekISO8601(m_value) % 7);
    /// <summary>Returns a day of week [1 (Monday), 7 (Sunday)] from the specified Julian Day Number. Julian Day Number 0 was Monday. For US day-of-week numbering, simply do "ComputeDayOfWeekIso(JDN) % 7".</summary>
    public int DayOfWeekISO8601
      => GetDayOfWeekISO8601(m_value);

    public double Value
      => m_value;

    public void GetDateParts(ConversionCalendar calendar, out int year, out int month, out int day)
      => ConvertToDateParts(m_value, calendar, out year, out month, out day);

    /// <summary>Creates a new string from this instance.</summary>
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
    /// <summary>Creates a new <see cref="JulianDate"/> from this instance.</summary>
    public JulianDate ToJulianDate()
      => new(m_value);
    /// <summary>Creates a new <see cref="MomentUtc"/> from this instance.</summary>
    public MomentUtc ToMomentUtc(ConversionCalendar calendar)
    {
      ConvertToDateParts((int)(m_value + 0.5), calendar, out var year, out var month, out var day);

      return new MomentUtc(year, month, day);
    }

    #region Static methods
    /// <summary>Computes the Julian Day Number (JDN) for the specified date components and calendar to use during conversion.</summary>
    public static int ConvertFromDateParts(int year, int month, int day, ConversionCalendar calendar)
      => calendar switch
      {
        ConversionCalendar.GregorianCalendar => (1461 * (year + 4800 + (month - 14) / 12)) / 4 + (367 * (month - 2 - 12 * ((month - 14) / 12))) / 12 - (3 * ((year + 4900 + (month - 14) / 12) / 100)) / 4 + day - 32075,// The algorithm is valid for all (possibly proleptic) Gregorian calendar dates after November 23, -4713. Divisions are integer divisions towards zero, fractional parts are ignored.
        ConversionCalendar.JulianCalendar => 367 * year - (7 * (year + 5001 + (month - 9) / 7)) / 4 + (275 * month) / 9 + day + 1729777,// The algorithm is valid for all (possibly proleptic) Julian calendar years >= -4712, that is, for all JDN >= 0. Divisions are integer divisions, fractional parts are ignored.
        _ => throw new System.ArgumentOutOfRangeException(nameof(calendar)),
      };
    /// <summary>Create a new MomentUtc from the specified Julian Day Number and conversion calendar.</summary>
    public static void ConvertToDateParts(int julianDayNumber, ConversionCalendar calendar, out int year, out int month, out int day)
    {
      // This is an algorithm by Edward Graham Richards to convert a Julian Day Number, J, to a date in the Gregorian calendar (proleptic, when applicable).
      // Richards states the algorithm is valid for Julian day numbers greater than or equal to 0.
      // All variables are integer values, and the notation "a div b" indicates integer division, and "mod(a,b)" denotes the modulus operator.

      var f = julianDayNumber + 1401;

      if (calendar == ConversionCalendar.GregorianCalendar)
        f += (4 * julianDayNumber + 274277) / 146097 * 3 / 4 + -38;

      var eq = System.Math.DivRem(4 * f + 3, 1461, out var er);
      var hq = System.Math.DivRem(5 * (er / 4) + 2, 153, out var hr);

      day = hr / 5 + 1;
      month = ((hq + 2) % 12) + 1;
      year = eq - 4716 + (14 - month) / 12;
    }

    /// <summary>Returns the ISO day of the week from the Julian Day Number. The US day of the week can be determined by: GetDayOfWeekISO(JDN) % 7.</summary>
    public static int GetDayOfWeekISO8601(int julianDayNumber)
      => (julianDayNumber % 7 is var dow && dow <= 0 ? dow + 7 : dow) + 1;

    /// <summary>Computes the Julian Period (JP) from the specified cyclic indices in the year.</summary>
    /// <param name="solarCycle">That year's position in the 28-year solar cycle.</param>
    /// <param name="lunarCycle">That year's position in the 19-year lunar cycle.</param>
    /// <param name="indictionCycle">That year's position in the 15-year indiction cycle.</param>
    public static int GetJulianPeriod(int solarCycle, int lunarCycle, int indictionCycle)
      => (indictionCycle * 6916 + lunarCycle * 4200 + solarCycle * 4845) % (15 * 19 * 28) is var year && year > 4713 ? year - 4713 : year < 4714 ? -(4714 - year) : year;

    /// <summary>Returns whether the Julian Date value (JD) is considered to be on the Gregorian Calendar.</summary>
    public static bool IsGregorianCalendar(int julianDayNumber)
      => julianDayNumber >= 2299161;
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
      => new(-jd.m_value);
    public static double operator -(JulianDayNumber a, JulianDayNumber b)
      => a.m_value - b.m_value;

    public static JulianDayNumber operator +(JulianDayNumber a, int b)
      => new(a.m_value + b);
    public static JulianDayNumber operator /(JulianDayNumber a, int b)
      => new(a.m_value / b);
    public static JulianDayNumber operator *(JulianDayNumber a, int b)
      => new(a.m_value * b);
    public static JulianDayNumber operator %(JulianDayNumber a, int b)
      => new(a.m_value % b);
    public static JulianDayNumber operator -(JulianDayNumber a, int b)
      => new(a.m_value - b);
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
      => $"<{GetType().Name}: {m_value} " + (IsGregorianCalendar(m_value) ? $"({ToDateString(ConversionCalendar.GregorianCalendar)})" : $"({ToDateString(ConversionCalendar.JulianCalendar)})*");
    #endregion Object overrides
  }
}
