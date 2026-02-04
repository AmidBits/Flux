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

    public ElectricChargeDensity(MetricPrefix prefix, double coulombPerCubicMeter) => m_value = prefix.ConvertPrefix(coulombPerCubicMeter, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + ElectricChargeDensityUnit.CoulombPerCubicMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ElectricChargeDensityUnit unit, double value)
      => unit switch
      {
        ElectricChargeDensityUnit.CoulombPerCubicMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(ElectricChargeDensityUnit unit, double value)
      => unit switch
      {
        ElectricChargeDensityUnit.CoulombPerCubicMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, ElectricChargeDensityUnit from, ElectricChargeDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(ElectricChargeDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ElectricChargeDensityUnit unit = ElectricChargeDensityUnit.CoulombPerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(Number.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

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
