namespace Flux.Units
{
  /// <summary>
  /// <para>Volumetric mass density, unit of kilograms per cubic meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Charge_density"/></para>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearChargeDensity"/>, <see cref="AreaDensity"/> and <see cref="ElectricChargeDensity"/>.</remarks>
  public readonly record struct ElectricChargeDensity
    : System.IComparable, System.IComparable<ElectricChargeDensity>, System.IFormattable, ISiUnitValueQuantifiable<double, ElectricChargeDensityUnit>
  {
    private readonly double m_value;

    public ElectricChargeDensity(double value, ElectricChargeDensityUnit unit = ElectricChargeDensityUnit.CoulombPerCubicMeter) => m_value = ConvertFromUnit(unit, value);

    public ElectricChargeDensity(MetricPrefix prefix, double coulombPerCubicMeter) => m_value = prefix.ConvertTo(coulombPerCubicMeter, MetricPrefix.Unprefixed);

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(ElectricChargeDensity a, ElectricChargeDensity b) => a.CompareTo(b) < 0;
    public static bool operator >(ElectricChargeDensity a, ElectricChargeDensity b) => a.CompareTo(b) > 0;
    public static bool operator <=(ElectricChargeDensity a, ElectricChargeDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(ElectricChargeDensity a, ElectricChargeDensity b) => a.CompareTo(b) >= 0;

    public static ElectricChargeDensity operator -(ElectricChargeDensity v) => new(-v.m_value);
    public static ElectricChargeDensity operator *(ElectricChargeDensity a, ElectricChargeDensity b) => new(a.m_value * b.m_value);
    public static ElectricChargeDensity operator /(ElectricChargeDensity a, ElectricChargeDensity b) => new(a.m_value / b.m_value);
    public static ElectricChargeDensity operator %(ElectricChargeDensity a, ElectricChargeDensity b) => new(a.m_value % b.m_value);
    public static ElectricChargeDensity operator +(ElectricChargeDensity a, ElectricChargeDensity b) => new(a.m_value + b.m_value);
    public static ElectricChargeDensity operator -(ElectricChargeDensity a, ElectricChargeDensity b) => new(a.m_value - b.m_value);
    public static ElectricChargeDensity operator *(ElectricChargeDensity a, double b) => new(a.m_value * b);
    public static ElectricChargeDensity operator /(ElectricChargeDensity a, double b) => new(a.m_value / b);
    public static ElectricChargeDensity operator %(ElectricChargeDensity a, double b) => new(a.m_value % b);
    public static ElectricChargeDensity operator +(ElectricChargeDensity a, double b) => new(a.m_value + b);
    public static ElectricChargeDensity operator -(ElectricChargeDensity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricChargeDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricChargeDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(ElectricChargeDensityUnit.CoulombPerCubicMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(ElectricChargeDensityUnit.CoulombPerCubicMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(ElectricChargeDensityUnit.CoulombPerCubicMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ElectricChargeDensityUnit unit, double value)
      => unit switch
      {
        ElectricChargeDensityUnit.CoulombPerCubicMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(ElectricChargeDensityUnit unit, double value)
      => unit switch
      {
        ElectricChargeDensityUnit.CoulombPerCubicMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, ElectricChargeDensityUnit from, ElectricChargeDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(ElectricChargeDensityUnit unit)
      => unit switch
      {
        ElectricChargeDensityUnit.CoulombPerCubicMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(ElectricChargeDensityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(ElectricChargeDensityUnit unit, bool preferUnicode)
      => unit switch
      {
        ElectricChargeDensityUnit.CoulombPerCubicMeter => "C/m³",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ElectricChargeDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ElectricChargeDensityUnit unit = ElectricChargeDensityUnit.CoulombPerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="ElectricChargeDensity.Value"/> property is in <see cref="ElectricChargeDensityUnit.KilogramPerCubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
