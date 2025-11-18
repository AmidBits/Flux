namespace Flux.Units
{
  /// <summary>
  /// <para>Area mass density, unit of kilograms per square meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Charge_density"/></para>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearChargeDensity"/>, <see cref="SurfaceChargeDensity"/> and <see cref="ElectricChargeDensity"/>.</remarks>
  public readonly record struct SurfaceChargeDensity
    : System.IComparable, System.IComparable<SurfaceChargeDensity>, System.IFormattable, IUnitValueQuantifiable<double, SurfaceChargeDensityUnit>
  {
    private readonly double m_value;

    public SurfaceChargeDensity(double value, SurfaceChargeDensityUnit unit = SurfaceChargeDensityUnit.CoulombPerSquareMeter) => m_value = ConvertFromUnit(unit, value);

    public SurfaceChargeDensity(MetricPrefix prefix, double coulombPerSquareMeter) => m_value = prefix.ConvertPrefix(coulombPerSquareMeter, MetricPrefix.Unprefixed);

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(SurfaceChargeDensity a, SurfaceChargeDensity b) => a.CompareTo(b) < 0;
    public static bool operator >(SurfaceChargeDensity a, SurfaceChargeDensity b) => a.CompareTo(b) > 0;
    public static bool operator <=(SurfaceChargeDensity a, SurfaceChargeDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(SurfaceChargeDensity a, SurfaceChargeDensity b) => a.CompareTo(b) >= 0;

    public static SurfaceChargeDensity operator -(SurfaceChargeDensity v) => new(-v.m_value);
    public static SurfaceChargeDensity operator *(SurfaceChargeDensity a, SurfaceChargeDensity b) => new(a.m_value * b.m_value);
    public static SurfaceChargeDensity operator /(SurfaceChargeDensity a, SurfaceChargeDensity b) => new(a.m_value / b.m_value);
    public static SurfaceChargeDensity operator %(SurfaceChargeDensity a, SurfaceChargeDensity b) => new(a.m_value % b.m_value);
    public static SurfaceChargeDensity operator +(SurfaceChargeDensity a, SurfaceChargeDensity b) => new(a.m_value + b.m_value);
    public static SurfaceChargeDensity operator -(SurfaceChargeDensity a, SurfaceChargeDensity b) => new(a.m_value - b.m_value);
    public static SurfaceChargeDensity operator *(SurfaceChargeDensity a, double b) => new(a.m_value * b);
    public static SurfaceChargeDensity operator /(SurfaceChargeDensity a, double b) => new(a.m_value / b);
    public static SurfaceChargeDensity operator %(SurfaceChargeDensity a, double b) => new(a.m_value % b);
    public static SurfaceChargeDensity operator +(SurfaceChargeDensity a, double b) => new(a.m_value + b);
    public static SurfaceChargeDensity operator -(SurfaceChargeDensity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is SurfaceChargeDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(SurfaceChargeDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(SurfaceChargeDensityUnit.CoulombPerSquareMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + SurfaceChargeDensityUnit.CoulombPerSquareMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(SurfaceChargeDensityUnit unit, double value)
      => unit switch
      {
        SurfaceChargeDensityUnit.CoulombPerSquareMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(SurfaceChargeDensityUnit unit, double value)
      => unit switch
      {
        SurfaceChargeDensityUnit.CoulombPerSquareMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, SurfaceChargeDensityUnit from, SurfaceChargeDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(SurfaceChargeDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(SurfaceChargeDensityUnit unit = SurfaceChargeDensityUnit.CoulombPerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="SurfaceChargeDensity.Value"/> property is in <see cref="SurfaceChargeDensityUnit.KilogramPerSquareMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
