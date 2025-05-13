namespace Flux.Units
{
  /// <summary>
  /// <para>Volumetric mass density, unit of kilograms per cubic meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Density"/></para>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="EnergyDensity"/>.</remarks>
  public readonly record struct EnergyDensity
    : System.IComparable, System.IComparable<EnergyDensity>, System.IFormattable, ISiUnitValueQuantifiable<double, EnergyDensityUnit>
  {
    private readonly double m_value;

    public EnergyDensity(double value, EnergyDensityUnit unit = EnergyDensityUnit.JoulePerCubicMeter) => m_value = ConvertFromUnit(unit, value);

    public EnergyDensity(MetricPrefix prefix, double joulePerCubicMeter) => m_value = prefix.ChangePrefix(joulePerCubicMeter, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Kilo.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + EnergyDensityUnit.JoulePerCubicMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(EnergyDensityUnit unit, double value)
      => unit switch
      {
        EnergyDensityUnit.JoulePerCubicMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(EnergyDensityUnit unit, double value)
      => unit switch
      {
        EnergyDensityUnit.JoulePerCubicMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, EnergyDensityUnit from, EnergyDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(EnergyDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(EnergyDensityUnit unit = EnergyDensityUnit.JoulePerCubicMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

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
