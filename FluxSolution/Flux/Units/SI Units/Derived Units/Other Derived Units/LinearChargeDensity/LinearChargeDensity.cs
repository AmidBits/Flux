namespace Flux.Units
{
  /// <summary>
  /// <para>Linear mass density, unit of kilograms per meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Charge_density"/></para>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearChargeDensity"/>, <see cref="AreaDensity"/> and <see cref="Density"/>.</remarks>
  public readonly record struct LinearChargeDensity
    : System.IComparable, System.IComparable<LinearChargeDensity>, System.IFormattable, ISiUnitValueQuantifiable<double, LinearChargeDensityUnit>
  {
    private readonly double m_value;

    public LinearChargeDensity(double value, LinearChargeDensityUnit unit = LinearChargeDensityUnit.CoulombPerMeter) => m_value = ConvertFromUnit(unit, value);

    public LinearChargeDensity(MetricPrefix prefix, double coulombPerMeter) => m_value = prefix.ConvertPrefix(coulombPerMeter, MetricPrefix.Unprefixed);

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(LinearChargeDensity a, LinearChargeDensity b) => a.CompareTo(b) < 0;
    public static bool operator >(LinearChargeDensity a, LinearChargeDensity b) => a.CompareTo(b) > 0;
    public static bool operator <=(LinearChargeDensity a, LinearChargeDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(LinearChargeDensity a, LinearChargeDensity b) => a.CompareTo(b) >= 0;

    public static LinearChargeDensity operator -(LinearChargeDensity v) => new(-v.m_value);
    public static LinearChargeDensity operator *(LinearChargeDensity a, LinearChargeDensity b) => new(a.m_value * b.m_value);
    public static LinearChargeDensity operator /(LinearChargeDensity a, LinearChargeDensity b) => new(a.m_value / b.m_value);
    public static LinearChargeDensity operator %(LinearChargeDensity a, LinearChargeDensity b) => new(a.m_value % b.m_value);
    public static LinearChargeDensity operator +(LinearChargeDensity a, LinearChargeDensity b) => new(a.m_value + b.m_value);
    public static LinearChargeDensity operator -(LinearChargeDensity a, LinearChargeDensity b) => new(a.m_value - b.m_value);
    public static LinearChargeDensity operator *(LinearChargeDensity a, double b) => new(a.m_value * b);
    public static LinearChargeDensity operator /(LinearChargeDensity a, double b) => new(a.m_value / b);
    public static LinearChargeDensity operator %(LinearChargeDensity a, double b) => new(a.m_value % b);
    public static LinearChargeDensity operator +(LinearChargeDensity a, double b) => new(a.m_value + b);
    public static LinearChargeDensity operator -(LinearChargeDensity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is LinearChargeDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(LinearChargeDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(LinearChargeDensityUnit.CoulombPerMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + LinearChargeDensityUnit.CoulombPerMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(LinearChargeDensityUnit unit, double value)
      => unit switch
      {
        LinearChargeDensityUnit.CoulombPerMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(LinearChargeDensityUnit unit, double value)
      => unit switch
      {
        LinearChargeDensityUnit.CoulombPerMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, LinearChargeDensityUnit from, LinearChargeDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(LinearChargeDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(LinearChargeDensityUnit unit = LinearChargeDensityUnit.CoulombPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="LinearChargeDensity.Value"/> property is in <see cref="LinearChargeDensityUnit.KilogramPerCubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
