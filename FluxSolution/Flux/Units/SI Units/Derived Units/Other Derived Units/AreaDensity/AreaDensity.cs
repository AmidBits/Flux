namespace Flux.Units
{
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

    public AreaDensity(MetricPrefix prefix, double gramPerSquareMeter) => m_value = prefix.ChangePrefix(gramPerSquareMeter, MetricPrefix.Unprefixed);

    public AreaDensity(Mass mass, Area volume) : this(mass.Value / volume.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(AreaDensity a, AreaDensity b) => a.CompareTo(b) < 0;
    public static bool operator >(AreaDensity a, AreaDensity b) => a.CompareTo(b) > 0;
    public static bool operator <=(AreaDensity a, AreaDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(AreaDensity a, AreaDensity b) => a.CompareTo(b) >= 0;

    public static AreaDensity operator -(AreaDensity v) => new(-v.m_value);
    public static AreaDensity operator *(AreaDensity a, AreaDensity b) => new(a.m_value * b.m_value);
    public static AreaDensity operator /(AreaDensity a, AreaDensity b) => new(a.m_value / b.m_value);
    public static AreaDensity operator %(AreaDensity a, AreaDensity b) => new(a.m_value % b.m_value);
    public static AreaDensity operator +(AreaDensity a, AreaDensity b) => new(a.m_value + b.m_value);
    public static AreaDensity operator -(AreaDensity a, AreaDensity b) => new(a.m_value - b.m_value);
    public static AreaDensity operator *(AreaDensity a, double b) => new(a.m_value * b);
    public static AreaDensity operator /(AreaDensity a, double b) => new(a.m_value / b);
    public static AreaDensity operator %(AreaDensity a, double b) => new(a.m_value % b);
    public static AreaDensity operator +(AreaDensity a, double b) => new(a.m_value + b);
    public static AreaDensity operator -(AreaDensity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is AreaDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(AreaDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(AreaDensityUnit.KilogramPerSquareMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + AreaDensityUnit.GramPerSquareMeter.GetUnitName(preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + AreaDensityUnit.GramPerSquareMeter.GetUnitSymbol(preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Kilo.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(AreaDensityUnit unit, double value)
      => unit switch
      {
        AreaDensityUnit.KilogramPerSquareMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(AreaDensityUnit unit, double value)
      => unit switch
      {
        AreaDensityUnit.KilogramPerSquareMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, AreaDensityUnit from, AreaDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(AreaDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AreaDensityUnit unit = AreaDensityUnit.KilogramPerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

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
