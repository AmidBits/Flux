namespace Flux.Quantities
{
  public enum AreaDensityUnit
  {
    /// <summary>This is the default unit for <see cref="AreaDensity"/>.</summary>
    KilogramPerSquareMeter,
    GramPerSquareMeter,
  }

  /// <summary>
  /// <para>Area mass density, unit of kilograms per square meter.</para>
  /// <see href="https://en.wikipedia.org/wiki/Area_density"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="Density"/>.</remarks>
  public readonly record struct AreaDensity
    : System.IComparable, System.IComparable<AreaDensity>, System.IFormattable, IUnitValueQuantifiable<double, AreaDensityUnit>
  {
    private readonly double m_value;

    public AreaDensity(double value, AreaDensityUnit unit = AreaDensityUnit.KilogramPerSquareMeter) => m_value = ConvertFromUnit(unit, value);

    public AreaDensity(MetricPrefix prefix, double gramPerSquareMeter) => m_value = prefix.ConvertTo(gramPerSquareMeter, MetricPrefix.Unprefixed);

    public AreaDensity(Mass mass, Area volume) : this(mass.Value / volume.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(AreaDensity a, AreaDensity b) => a.CompareTo(b) < 0;
    public static bool operator <=(AreaDensity a, AreaDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >(AreaDensity a, AreaDensity b) => a.CompareTo(b) > 0;
    public static bool operator >=(AreaDensity a, AreaDensity b) => a.CompareTo(b) >= 0;

    public static AreaDensity operator -(AreaDensity v) => new(-v.m_value);
    public static AreaDensity operator +(AreaDensity a, double b) => new(a.m_value + b);
    public static AreaDensity operator +(AreaDensity a, AreaDensity b) => a + b.m_value;
    public static AreaDensity operator /(AreaDensity a, double b) => new(a.m_value / b);
    public static AreaDensity operator /(AreaDensity a, AreaDensity b) => a / b.m_value;
    public static AreaDensity operator *(AreaDensity a, double b) => new(a.m_value * b);
    public static AreaDensity operator *(AreaDensity a, AreaDensity b) => a * b.m_value;
    public static AreaDensity operator %(AreaDensity a, double b) => new(a.m_value % b);
    public static AreaDensity operator %(AreaDensity a, AreaDensity b) => a % b.m_value;
    public static AreaDensity operator -(AreaDensity a, double b) => new(a.m_value - b);
    public static AreaDensity operator -(AreaDensity a, AreaDensity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is AreaDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(AreaDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AreaDensityUnit.KilogramPerSquareMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(AreaDensityUnit.GramPerSquareMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(AreaDensityUnit.GramPerSquareMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Kilo.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(AreaDensityUnit unit, double value)
      => unit switch
      {
        AreaDensityUnit.KilogramPerSquareMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(AreaDensityUnit unit, double value)
      => unit switch
      {
        AreaDensityUnit.KilogramPerSquareMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, AreaDensityUnit from, AreaDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(AreaDensityUnit unit)
      => unit switch
      {
        AreaDensityUnit.KilogramPerSquareMeter => 1,

        AreaDensityUnit.GramPerSquareMeter => 1000,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(AreaDensityUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural);

    public static string GetUnitSymbol(AreaDensityUnit unit, bool preferUnicode)
      => unit switch
      {
        AreaDensityUnit.KilogramPerSquareMeter => "kg/m²",

        AreaDensityUnit.GramPerSquareMeter => "g/m²",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AreaDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AreaDensityUnit unit = AreaDensityUnit.KilogramPerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AreaDensity.Value"/> property is in <see cref="AreaDensityUnit.KilogramPerSquareMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
