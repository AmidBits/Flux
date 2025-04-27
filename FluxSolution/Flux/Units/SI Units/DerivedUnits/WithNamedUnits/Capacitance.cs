namespace Flux.Units
{
  /// <summary>
  /// <para>Electrical capacitance, unit of Farad.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Capacitance"/></para>
  /// </summary>
  public readonly record struct Capacitance
    : System.IComparable, System.IComparable<Capacitance>, System.IFormattable, ISiUnitValueQuantifiable<double, CapacitanceUnit>
  {
    private readonly double m_value;

    public Capacitance(double value, CapacitanceUnit unit = CapacitanceUnit.Farad) => m_value = ConvertFromUnit(unit, value);

    public Capacitance(MetricPrefix prefix, double farad) => m_value = prefix.ConvertTo(farad, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(Capacitance a, Capacitance b) => a.CompareTo(b) < 0;
    public static bool operator >(Capacitance a, Capacitance b) => a.CompareTo(b) > 0;
    public static bool operator <=(Capacitance a, Capacitance b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Capacitance a, Capacitance b) => a.CompareTo(b) >= 0;

    public static Capacitance operator -(Capacitance v) => new(-v.m_value);
    public static Capacitance operator *(Capacitance a, Capacitance b) => new(a.m_value * b.m_value);
    public static Capacitance operator /(Capacitance a, Capacitance b) => new(a.m_value / b.m_value);
    public static Capacitance operator %(Capacitance a, Capacitance b) => new(a.m_value % b.m_value);
    public static Capacitance operator +(Capacitance a, Capacitance b) => new(a.m_value + b.m_value);
    public static Capacitance operator -(Capacitance a, Capacitance b) => new(a.m_value - b.m_value);
    public static Capacitance operator *(Capacitance a, double b) => new(a.m_value * b);
    public static Capacitance operator /(Capacitance a, double b) => new(a.m_value / b);
    public static Capacitance operator %(Capacitance a, double b) => new(a.m_value % b);
    public static Capacitance operator +(Capacitance a, double b) => new(a.m_value + b);
    public static Capacitance operator -(Capacitance a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Capacitance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Capacitance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(CapacitanceUnit.Farad, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(CapacitanceUnit.Farad, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetSiUnitValue(prefix);

      return value.ToSiFormattedString(format, formatProvider)
        + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString()
        + (fullName ? GetSiUnitName(prefix, value.IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));
    }

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(CapacitanceUnit unit, double value)
      => unit switch
      {
        CapacitanceUnit.Farad => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(CapacitanceUnit unit, double value)
      => unit switch
      {
        CapacitanceUnit.Farad => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, CapacitanceUnit from, CapacitanceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(CapacitanceUnit unit)
      => unit switch
      {
        CapacitanceUnit.Farad => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(CapacitanceUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(CapacitanceUnit unit, bool preferUnicode)
      => unit switch
      {
        Units.CapacitanceUnit.Farad => "F",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(CapacitanceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(CapacitanceUnit unit = CapacitanceUnit.Farad, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + Unicode.UnicodeSpacing.Space.ToSpacingString()
        + (fullName ? GetUnitName(unit, value.IsConsideredPlural()) : GetUnitSymbol(unit, false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Capacitance.Value"/> property is in <see cref="CapacitanceUnit.Farad"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
