namespace Flux.Units
{
  /// <summary>
  /// <para>Volumetric mass density, unit of kilograms per cubic meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Density"/></para>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="Molarity"/>.</remarks>
  public readonly record struct Molarity
    : System.IComparable, System.IComparable<Molarity>, System.IFormattable, ISiUnitValueQuantifiable<double, MolarityUnit>
  {
    private readonly double m_value;

    public Molarity(double value, MolarityUnit unit = MolarityUnit.MolesPerCubicMeter) => m_value = ConvertFromUnit(unit, value);

    public Molarity(MetricPrefix prefix, double molesPerCubicMeter) => m_value = prefix.ConvertTo(molesPerCubicMeter, MetricPrefix.Unprefixed);

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Molarity a, Molarity b) => a.CompareTo(b) < 0;
    public static bool operator >(Molarity a, Molarity b) => a.CompareTo(b) > 0;
    public static bool operator <=(Molarity a, Molarity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Molarity a, Molarity b) => a.CompareTo(b) >= 0;

    public static Molarity operator -(Molarity v) => new(-v.m_value);
    public static Molarity operator *(Molarity a, Molarity b) => new(a.m_value * b.m_value);
    public static Molarity operator /(Molarity a, Molarity b) => new(a.m_value / b.m_value);
    public static Molarity operator %(Molarity a, Molarity b) => new(a.m_value % b.m_value);
    public static Molarity operator +(Molarity a, Molarity b) => new(a.m_value + b.m_value);
    public static Molarity operator -(Molarity a, Molarity b) => new(a.m_value - b.m_value);
    public static Molarity operator *(Molarity a, double b) => new(a.m_value * b);
    public static Molarity operator /(Molarity a, double b) => new(a.m_value / b);
    public static Molarity operator %(Molarity a, double b) => new(a.m_value % b);
    public static Molarity operator +(Molarity a, double b) => new(a.m_value + b);
    public static Molarity operator -(Molarity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Molarity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Molarity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(MolarityUnit.MolesPerCubicMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(MolarityUnit.MolesPerCubicMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(MolarityUnit.MolesPerCubicMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Kilo.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(MolarityUnit unit, double value)
      => unit switch
      {
        MolarityUnit.MolesPerCubicMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(MolarityUnit unit, double value)
      => unit switch
      {
        MolarityUnit.MolesPerCubicMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, MolarityUnit from, MolarityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(MolarityUnit unit)
      => unit switch
      {
        MolarityUnit.MolesPerCubicMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(MolarityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(MolarityUnit unit, bool preferUnicode)
      => unit switch
      {
        MolarityUnit.MolesPerCubicMeter => "J/m³",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(MolarityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MolarityUnit unit = MolarityUnit.MolesPerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Molarity.Value"/> property is in <see cref="MolarityUnit.MolesPerCubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
