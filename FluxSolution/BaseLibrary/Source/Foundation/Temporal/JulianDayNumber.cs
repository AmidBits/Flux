namespace Flux
{
  public static partial class JulianDayNumberEm
  {
    public static JulianDayNumber ToJulianDayNumber(this System.DateTime source, ConversionCalendar calendar)
      => source.ToMomentUtc().ToJulianDayNumber(calendar);
  }

  /// <summary>Julian Day Number unit of days.</summary>
  /// <remarks>Julian Day Number is not related to the Julian Calendar. Functionality that compute on the Julian Calendar will have JulianCalendar in the name.</remarks>
  /// <see cref="https://en.wikipedia.org/wiki/Julian_day"/>
  public struct JulianDayNumber
    : System.IComparable<JulianDayNumber>, System.IConvertible, System.IEquatable<JulianDayNumber>, IQuantifiable<int>
  {
    public static readonly JulianDayNumber Zero;

    public int m_value;

    /// <summary>Create a Julian Date (JD) from the specified <paramref name="value"/> value.</summary>
    public JulianDayNumber(int value)
      => m_value = value;
    /// <summary>Computes the Julian Day Number (JDN) for the specified date components and calendar to use during conversion.</summary>
    public JulianDayNumber(int year, int month, int day, ConversionCalendar calendar)
      : this(ConvertFromDateParts(year, month, day, calendar))
    { }

    /// <summary>Returns a <see cref="System.DayOfWeek"/> from the Julian Day Number.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.DayOfWeek DayOfWeek
      => (System.DayOfWeek)(GetDayOfWeekISO8601(m_value) % 7);
    /// <summary>Returns a day of week [1 (Monday), 7 (Sunday)] from the specified Julian Day Number. Julian Day Number 0 was Monday. For US day-of-week numbering, simply do "ComputeDayOfWeekIso(JDN) % 7".</summary>
    [System.Diagnostics.Contracts.Pure]
    public int DayOfWeekISO8601
      => GetDayOfWeekISO8601(m_value);

    [System.Diagnostics.Contracts.Pure]
    public int Value
      => m_value;

    [System.Diagnostics.Contracts.Pure]
    public JulianDayNumber AddWeeks(int weeks)
      => this + (weeks * 7);
    [System.Diagnostics.Contracts.Pure]
    public JulianDayNumber AddDays(int days)
      => this + days;

    [System.Diagnostics.Contracts.Pure]
    public ConversionCalendar GetConversionCalendar()
      => IsGregorianCalendar(m_value) ? ConversionCalendar.GregorianCalendar : ConversionCalendar.JulianCalendar;

    [System.Diagnostics.Contracts.Pure]
    public void GetDateParts(ConversionCalendar calendar, out int year, out int month, out int day)
      => ConvertToDateParts(m_value, calendar, out year, out month, out day);

    /// <summary>Creates a new string from this instance.</summary>
    [System.Diagnostics.Contracts.Pure]
    public string ToDateString(ConversionCalendar calendar)
    {
      var sb = new System.Text.StringBuilder();

      if (calendar != GetConversionCalendar())
        switch (calendar)
        {
          case ConversionCalendar.GregorianCalendar:
            sb.Append(@"Gregorian Proleptic Calendar, ");
            break;
          case ConversionCalendar.JulianCalendar:
            sb.Append(@"Julian Proleptic Calendar, ");
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(calendar));
        }

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
        sb.Append(@" or ");
        sb.Append(System.Math.Abs(year) + 1);
        sb.Append(@" BC");
      }

      return sb.ToString();
    }

    /// <summary>Creates a new <see cref="JulianDate"/> from this instance.</summary>
    [System.Diagnostics.Contracts.Pure]
    public JulianDate ToJulianDate()
      => new(m_value);

    /// <summary>Creates a new <see cref="MomentUtc"/> from this instance.</summary>
    [System.Diagnostics.Contracts.Pure]
    public MomentUtc ToMomentUtc(ConversionCalendar calendar)
    {
      ConvertToDateParts((int)(m_value + 0.5), calendar, out var year, out var month, out var day);

      return new MomentUtc(year, month, day);
    }

    #region Static methods
    /// <summary>Computes the Julian Day Number (JDN) for the specified date components and calendar to use during conversion.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static int ConvertFromDateParts(int year, int month, int day, ConversionCalendar calendar)
      => calendar switch
      {
        ConversionCalendar.GregorianCalendar => (1461 * (year + 4800 + (month - 14) / 12)) / 4 + (367 * (month - 2 - 12 * ((month - 14) / 12))) / 12 - (3 * ((year + 4900 + (month - 14) / 12) / 100)) / 4 + day - 32075,// The algorithm is valid for all (possibly proleptic) Gregorian calendar dates after November 23, -4713. Divisions are integer divisions towards zero, fractional parts are ignored.
        ConversionCalendar.JulianCalendar => 367 * year - (7 * (year + 5001 + (month - 9) / 7)) / 4 + (275 * month) / 9 + day + 1729777,// The algorithm is valid for all (possibly proleptic) Julian calendar years >= -4712, that is, for all JDN >= 0. Divisions are integer divisions, fractional parts are ignored.
        _ => throw new System.ArgumentOutOfRangeException(nameof(calendar)),
      };
    /// <summary>Create a new MomentUtc from the specified Julian Day Number and conversion calendar.</summary>
    [System.Diagnostics.Contracts.Pure]
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
    [System.Diagnostics.Contracts.Pure]
    public static int GetDayOfWeekISO8601(int julianDayNumber)
      => (julianDayNumber % 7 is var dow && dow < 0 ? dow + 7 : dow) + 1;

    /// <summary>Computes the Julian Period (JP) from the specified cyclic indices in the year.</summary>
    /// <param name="solarCycle">That year's position in the 28-year solar cycle.</param>
    /// <param name="lunarCycle">That year's position in the 19-year lunar cycle.</param>
    /// <param name="indictionCycle">That year's position in the 15-year indiction cycle.</param>
    [System.Diagnostics.Contracts.Pure]
    public static int GetJulianPeriod(int solarCycle, int lunarCycle, int indictionCycle)
      => (indictionCycle * 6916 + lunarCycle * 4200 + solarCycle * 4845) % (15 * 19 * 28) is var year && year > 4713 ? year - 4713 : year < 4714 ? -(4714 - year) : year;

    /// <summary>Returns whether the Julian Date value (JD) is considered to be on the Gregorian Calendar.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static bool IsGregorianCalendar(int julianDayNumber)
      => julianDayNumber >= 2299161;
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator <(JulianDayNumber a, JulianDayNumber b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(JulianDayNumber a, JulianDayNumber b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(JulianDayNumber a, JulianDayNumber b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(JulianDayNumber a, JulianDayNumber b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(JulianDayNumber a, JulianDayNumber b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(JulianDayNumber a, JulianDayNumber b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static JulianDayNumber operator -(JulianDayNumber jd) => new(-jd.m_value);
    [System.Diagnostics.Contracts.Pure] public static double operator -(JulianDayNumber a, JulianDayNumber b) => a.m_value - b.m_value;

    [System.Diagnostics.Contracts.Pure] public static JulianDayNumber operator +(JulianDayNumber a, int b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static JulianDayNumber operator /(JulianDayNumber a, int b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static JulianDayNumber operator *(JulianDayNumber a, int b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static JulianDayNumber operator %(JulianDayNumber a, int b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static JulianDayNumber operator -(JulianDayNumber a, int b) => new(a.m_value - b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(JulianDayNumber other) => m_value < other.m_value ? -1 : m_value > other.m_value ? 1 : 0;
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is JulianDayNumber o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(JulianDayNumber other) => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is JulianDayNumber o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string? ToString() => $"{GetType().Name} {{ {m_value} ({ToDateString(GetConversionCalendar())}) }}";
    #endregion Object overrides
  }
}
