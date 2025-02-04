namespace Flux.Quantities
{
  public enum EnergyDensityUnit
  {
    /// <summary>This is the default unit for <see cref="EnergyDensity"/>.</summary>
    JoulePerCubicMeter,
  }

  /// <summary>
  /// <para>Volumetric mass density, unit of kilograms per cubic meter.</para>
  /// <see href="https://en.wikipedia.org/wiki/Density"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="EnergyDensity"/>.</remarks>
  public readonly record struct EnergyDensity
    : System.IComparable, System.IComparable<EnergyDensity>, System.IFormattable, ISiUnitValueQuantifiable<double, EnergyDensityUnit>
  {
    private readonly double m_value;

    public EnergyDensity(double value, EnergyDensityUnit unit = EnergyDensityUnit.JoulePerCubicMeter) => m_value = ConvertFromUnit(unit, value);

    public EnergyDensity(MetricPrefix prefix, double joulePerCubicMeter) => m_value = prefix.ConvertTo(joulePerCubicMeter, MetricPrefix.Unprefixed);

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(EnergyDensity a, EnergyDensity b) => a.CompareTo(b) < 0;
    public static bool operator >(EnergyDensity a, EnergyDensity b) => a.CompareTo(b) > 0;
    public static bool operator <=(EnergyDensity a, EnergyDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(EnergyDensity a, EnergyDensity b) => a.CompareTo(b) >= 0;

    public static EnergyDensity operator -(EnergyDensity v) => new(-v.m_value);
    public static EnergyDensity operator *(EnergyDensity a, EnergyDensity b) => new(a.m_value * b.m_value);
    public static EnergyDensity operator /(EnergyDensity a, EnergyDensity b) => new(a.m_value / b.m_value);
    public static EnergyDensity operator %(EnergyDensity a, EnergyDensity b) => new(a.m_value % b.m_value);
    public static EnergyDensity operator +(EnergyDensity a, EnergyDensity b) => new(a.m_value + b.m_value);
    public static EnergyDensity operator -(EnergyDensity a, EnergyDensity b) => new(a.m_value - b.m_value);
    public static EnergyDensity operator *(EnergyDensity a, double b) => new(a.m_value * b);
    public static EnergyDensity operator /(EnergyDensity a, double b) => new(a.m_value / b);
    public static EnergyDensity operator %(EnergyDensity a, double b) => new(a.m_value % b);
    public static EnergyDensity operator +(EnergyDensity a, double b) => new(a.m_value + b);
    public static EnergyDensity operator -(EnergyDensity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is EnergyDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(EnergyDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(EnergyDensityUnit.JoulePerCubicMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(EnergyDensityUnit.JoulePerCubicMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(EnergyDensityUnit.JoulePerCubicMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Kilo.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(EnergyDensityUnit unit, double value)
      => unit switch
      {
        EnergyDensityUnit.JoulePerCubicMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(EnergyDensityUnit unit, double value)
      => unit switch
      {
        EnergyDensityUnit.JoulePerCubicMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, EnergyDensityUnit from, EnergyDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(EnergyDensityUnit unit)
      => unit switch
      {
        EnergyDensityUnit.JoulePerCubicMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(EnergyDensityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(EnergyDensityUnit unit, bool preferUnicode)
      => unit switch
      {
        EnergyDensityUnit.JoulePerCubicMeter => "J/m³",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(EnergyDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(EnergyDensityUnit unit = EnergyDensityUnit.JoulePerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="EnergyDensity.Value"/> property is in <see cref="EnergyDensityUnit.JoulePerCubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
