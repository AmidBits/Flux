namespace Flux.Quantities
{
  public enum SurfaceChargeDensityUnit
  {
    /// <summary>This is the default unit for <see cref="SurfaceChargeDensity"/>.</summary>
    CoulombPerSquareMeter,
  }

  /// <summary>
  /// <para>Area mass density, unit of kilograms per square meter.</para>
  /// <see href="https://en.wikipedia.org/wiki/Charge_density"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearChargeDensity"/>, <see cref="SurfaceChargeDensity"/> and <see cref="ElectricChargeDensity"/>.</remarks>
  public readonly record struct SurfaceChargeDensity
    : System.IComparable, System.IComparable<SurfaceChargeDensity>, System.IFormattable, IUnitValueQuantifiable<double, SurfaceChargeDensityUnit>
  {
    private readonly double m_value;

    public SurfaceChargeDensity(double value, SurfaceChargeDensityUnit unit = SurfaceChargeDensityUnit.CoulombPerSquareMeter) => m_value = ConvertFromUnit(unit, value);

    public SurfaceChargeDensity(MetricPrefix prefix, double coulombPerSquareMeter) => m_value = prefix.ConvertTo(coulombPerSquareMeter, MetricPrefix.Unprefixed);

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

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(SurfaceChargeDensityUnit.CoulombPerSquareMeter, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(SurfaceChargeDensityUnit.CoulombPerSquareMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(SurfaceChargeDensityUnit unit, double value)
      => unit switch
      {
        SurfaceChargeDensityUnit.CoulombPerSquareMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(SurfaceChargeDensityUnit unit, double value)
      => unit switch
      {
        SurfaceChargeDensityUnit.CoulombPerSquareMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, SurfaceChargeDensityUnit from, SurfaceChargeDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(SurfaceChargeDensityUnit unit)
      => unit switch
      {
        SurfaceChargeDensityUnit.CoulombPerSquareMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(SurfaceChargeDensityUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural);

    public static string GetUnitSymbol(SurfaceChargeDensityUnit unit, bool preferUnicode)
      => unit switch
      {
        SurfaceChargeDensityUnit.CoulombPerSquareMeter => "C/m²",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(SurfaceChargeDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(SurfaceChargeDensityUnit unit = SurfaceChargeDensityUnit.CoulombPerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

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
