namespace Flux
{
  public static partial class Em
  {
    public static Quantities.JulianDayNumber ToJulianDayNumber(this System.DateTime source, Quantities.TemporalCalendar calendar = Quantities.TemporalCalendar.GregorianCalendar)
      => new(source.Year, source.Month, source.Day, calendar);
  }

  namespace Quantities
  {
    /// <summary>
    /// <para>Julian Day Number, unit of days.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Julian_day"/></para>
    /// </summary>
    /// <remarks>Julian Day Number is not related to the Julian Calendar. Functionality that compute on the Julian Calendar will have JulianCalendar in the name.</remarks>
    public readonly record struct JulianDayNumber
      : System.IComparable, System.IComparable<JulianDayNumber>, System.IFormattable, IValueQuantifiable<int>
    {
      public readonly int m_value;

      /// <summary>Create a Julian Date (JD) from the specified <paramref name="value"/> value.</summary>
      public JulianDayNumber(int value) => m_value = value;

      /// <summary>Computes the Julian Day Number (JDN) for the specified date components and calendar to use during conversion.</summary>
      public JulianDayNumber(int year, int month, int day, TemporalCalendar calendar)
        : this(ConvertDatePartsToJulianDayNumber(year, month, day, calendar)) { }

      /// <summary>Returns a <see cref="System.DayOfWeek"/> from the Julian Day Number.</summary>
      public System.DayOfWeek DayOfWeek => (System.DayOfWeek)GetDayOfWeek(m_value);

      public JulianDayNumber AddDays(int days) => this + days;

      public TemporalCalendar GetConversionCalendar() => TemporalCalendar.GregorianCalendar.Contains(m_value) ? TemporalCalendar.GregorianCalendar : TemporalCalendar.JulianCalendar;

      public (int Year, int Month, int Day) GetParts(TemporalCalendar calendar) => ConvertJulianDayNumberToDateParts(m_value, calendar);

      /// <summary>Creates a new string from this instance.</summary>
      public string ToDateString(TemporalCalendar calendar, bool indicateOutOfRangeCalendar = true)
      {
        var sb = new System.Text.StringBuilder();

        if (indicateOutOfRangeCalendar)
        {
          if (calendar.Contains(m_value) is var ic && !ic)
            sb.Append("Proleptic ");

          if (!ic || calendar != GetConversionCalendar())
          {
            sb.Append(calendar switch
            {
              TemporalCalendar.GregorianCalendar => "Gregorian Calendar, ",
              TemporalCalendar.JulianCalendar => "Julian Calendar, ",
              _ => throw new System.ArgumentOutOfRangeException(nameof(calendar))
            });
          }
        }

        sb.Append(DayOfWeek);
        sb.Append(StringOps.CommaSpace);

        var (year, month, day) = ConvertJulianDayNumberToDateParts(m_value, calendar); // Add 0.5 to the julian date value for date strings, because of the 12 noon convention in a Julian Date.

        sb.Append(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month));
        sb.Append(' ');
        sb.Append(day);
        sb.Append(StringOps.CommaSpace);
        sb.Append(year);

        if (year <= 0 && calendar == TemporalCalendar.GregorianCalendar)
        {
          sb.Append(@" or ");
          sb.Append(System.Math.Abs(year) + 1);
          sb.Append(@" BCE");
        }

        return sb.ToString();
      }

      /// <summary>Creates a new <see cref="JulianDate"/> from this instance.</summary>
      public JulianDate ToJulianDate() => new(m_value);

      /// <summary>Creates a new <see cref="Moment"/> from this instance.</summary>
      public Moment ToMomentUtc(TemporalCalendar calendar)
      {
        var (year, month, day) = ConvertJulianDayNumberToDateParts((int)(m_value + 0.5), calendar);

        return new Moment(year, month, day);
      }

      #region Static methods

      #region Conversion methods

      /// <summary>
      /// <para>Computes the Julian Day Number (JDN) for the specified date components and calendar to use during conversion.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Julian_day#Julian_day_number_calculation"/></para>
      /// </summary>
      /// <remarks>
      /// <para>The algorithm is valid for all (possibly proleptic) Gregorian calendar dates after November 23, -4713.</para>
      /// <para>The algorithm is valid for all (possibly proleptic) Julian calendar years >= -4712, that is, for all JDN >= 0.</para>
      /// </remarks>
      public static int ConvertDatePartsToJulianDayNumber(int year, int month, int day, TemporalCalendar calendar)
        => calendar switch
        {
          // The algorithm is valid for all (possibly proleptic) Gregorian calendar dates after November 23, -4713. Divisions are integer divisions towards zero, fractional parts are ignored.
          TemporalCalendar.GregorianCalendar => (1461 * (year + 4800 + (month - 14) / 12)) / 4 + (367 * (month - 2 - 12 * ((month - 14) / 12))) / 12 - (3 * ((year + 4900 + (month - 14) / 12) / 100)) / 4 + day - 32075,

          // The algorithm is valid for all (possibly proleptic) Julian calendar years >= -4712, that is, for all JDN >= 0. Divisions are integer divisions, fractional parts are ignored.
          TemporalCalendar.JulianCalendar => 367 * year - (7 * (year + 5001 + (month - 9) / 7)) / 4 + (275 * month) / 9 + day + 1729777,

          _ => throw new System.ArgumentOutOfRangeException(nameof(calendar)),
        };

      /// <summary>
      /// <para>Create a new MomentUtc from the specified Julian Day Number and conversion calendar.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Julian_day#Julian_day_number_calculation"/></para>
      /// </summary>
      /// <remarks>
      /// <para>This is an algorithm by Edward Graham Richards to convert a Julian Day Number, JDN, to a date in the Gregorian calendar (proleptic, when applicable).</para>
      /// <para>Richards states the algorithm is valid for Julian day numbers greater than or equal to 0.</para>
      /// </remarks>
      public static (int year, int month, int day) ConvertJulianDayNumberToDateParts(int julianDayNumber, TemporalCalendar calendar)
      {
        // All variables are integer values, and the notation "a div b" indicates integer division, and "mod(a,b)" denotes the modulus operator.

        var f = julianDayNumber + 1401;

        if (calendar == TemporalCalendar.GregorianCalendar)
          f += (4 * julianDayNumber + 274277) / 146097 * 3 / 4 + -38;

        var eq = System.Math.DivRem(4 * f + 3, 1461, out var er);
        var hq = System.Math.DivRem(5 * (er / 4) + 2, 153, out var hr);

        var day = hr / 5 + 1;
        var month = ((hq + 2) % 12) + 1;
        var year = eq - 4716 + (14 - month) / 12;

        return (year, month, day);
      }

      #endregion // Conversion methods

      /// <summary>
      /// <para>Returns the USA day of week W1 in the range [0 (Sunday), 6 (Saturday)] for the Julian Day Number.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Julian_day#Finding_day_of_week_given_Julian_day_number"/></para>
      /// </summary>
      /// <param name="julianDayNumber"></param>
      /// <returns></returns>
      /// <remarks>This is also the US day of the week W1 (for an afternoon or evening UT).</remarks>
      public static int GetDayOfWeek(int julianDayNumber) => (julianDayNumber + 1) % 7 is var dow && dow < 0 ? dow + 7 : dow;

      /// <summary>
      /// <para>Returns the ISO day of the week W0 in the range [1 (Monday), 7 (Sunday)] for the Julian Day Number. Julian Day Number 0 was Monday.</para>
      /// <para>The US day of the week W0 can be computed by: <c>GetDayOfWeekISO(JDN) % 7</c></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Julian_day#Finding_day_of_week_given_Julian_day_number"/></para>
      /// </summary>
      public static int GetDayOfWeekISO8601(int julianDayNumber) => (julianDayNumber % 7 is var dow && dow < 0 ? dow + 7 : dow) + 1;

      /// <summary>
      /// <para>Computes the Julian Period (JP) from the specified cyclic indices in the year.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Julian_day#Julian_Period_from_indiction,_Metonic_and_solar_cycles"/></para>
      /// </summary>
      /// <param name="solarCycle">That year's position in the 28-year solar cycle.</param>
      /// <param name="lunarCycle">That year's position in the 19-year lunar cycle.</param>
      /// <param name="indictionCycle">That year's position in the 15-year indiction cycle.</param>
      public static int GetJulianPeriod(int solarCycle, int lunarCycle, int indictionCycle)
        => (indictionCycle * 6916 + lunarCycle * 4200 + solarCycle * 4845) % (15 * 19 * 28) is var year && year > 4713 ? year - 4713 : year < 4714 ? -(4714 - year) : year;

      #endregion // Static methods

      #region Overloaded operators

      public static bool operator <(JulianDayNumber a, JulianDayNumber b) => a.CompareTo(b) < 0;
      public static bool operator <=(JulianDayNumber a, JulianDayNumber b) => a.CompareTo(b) <= 0;
      public static bool operator >(JulianDayNumber a, JulianDayNumber b) => a.CompareTo(b) > 0;
      public static bool operator >=(JulianDayNumber a, JulianDayNumber b) => a.CompareTo(b) >= 0;

      public static JulianDayNumber operator -(JulianDayNumber jd) => new(-jd.m_value);
      public static double operator -(JulianDayNumber a, JulianDayNumber b) => a.m_value - b.m_value;

      public static JulianDayNumber operator +(JulianDayNumber a, int b) => new(a.m_value + b);
      public static JulianDayNumber operator /(JulianDayNumber a, int b) => new(a.m_value / b);
      public static JulianDayNumber operator *(JulianDayNumber a, int b) => new(a.m_value * b);
      public static JulianDayNumber operator %(JulianDayNumber a, int b) => new(a.m_value % b);
      public static JulianDayNumber operator -(JulianDayNumber a, int b) => new(a.m_value - b);

      #endregion // Overloaded operators

      #region Implemented interfaces

      // IComparable<>
      public int CompareTo(JulianDayNumber other) => m_value < other.m_value ? -1 : m_value > other.m_value ? 1 : 0;

      // IComparable
      public int CompareTo(object? other) => other is not null && other is JulianDayNumber o ? CompareTo(o) : -1;

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToDateString(GetConversionCalendar()) + $" (JDN = {m_value})";

      #region IValueQuantifiable<>

      /// <summary>
      /// <para>The <see cref="JulianDayNumber.Value"/> property is the Julian day number.</para>
      /// </summary>
      public int Value => m_value;

      #endregion // IValueQuantifiable<>

      #endregion // Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
