namespace Flux.Quantities
{
  public enum LinearDensityUnit
  {
    /// <summary>This is the default unit for <see cref="LinearDensity"/>.</summary>
    KilogramPerMeter,
    GramPerMeter,
  }

  /// <summary>
  /// <para>Linear mass density, unit of kilograms per meter.</para>
  /// <see href="https://en.wikipedia.org/wiki/Linear_density"/>
  /// </summary>
  /// <remarks>Dimensional relationship: <see cref="LinearDensity"/>, <see cref="AreaDensity"/> and <see cref="Density"/>.</remarks>
  public readonly record struct LinearDensity
    : System.IComparable, System.IComparable<LinearDensity>, System.IFormattable, ISiUnitValueQuantifiable<double, LinearDensityUnit>
  {
    private readonly double m_value;

    public LinearDensity(double value, LinearDensityUnit unit = LinearDensityUnit.KilogramPerMeter) => m_value = ConvertFromUnit(unit, value);

    public LinearDensity(MetricPrefix prefix, double gramPerMeter) => m_value = prefix.ConvertTo(gramPerMeter, MetricPrefix.Unprefixed);

    public LinearDensity(Mass mass, Volume volume) : this(mass.Value / volume.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(LinearDensity a, LinearDensity b) => a.CompareTo(b) < 0;
    public static bool operator >(LinearDensity a, LinearDensity b) => a.CompareTo(b) > 0;
    public static bool operator <=(LinearDensity a, LinearDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(LinearDensity a, LinearDensity b) => a.CompareTo(b) >= 0;

    public static LinearDensity operator -(LinearDensity v) => new(-v.m_value);
    public static LinearDensity operator *(LinearDensity a, LinearDensity b) => new(a.m_value * b.m_value);
    public static LinearDensity operator /(LinearDensity a, LinearDensity b) => new(a.m_value / b.m_value);
    public static LinearDensity operator %(LinearDensity a, LinearDensity b) => new(a.m_value % b.m_value);
    public static LinearDensity operator +(LinearDensity a, LinearDensity b) => new(a.m_value + b.m_value);
    public static LinearDensity operator -(LinearDensity a, LinearDensity b) => new(a.m_value - b.m_value);
    public static LinearDensity operator *(LinearDensity a, double b) => new(a.m_value * b);
    public static LinearDensity operator /(LinearDensity a, double b) => new(a.m_value / b);
    public static LinearDensity operator %(LinearDensity a, double b) => new(a.m_value % b);
    public static LinearDensity operator +(LinearDensity a, double b) => new(a.m_value + b);
    public static LinearDensity operator -(LinearDensity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is LinearDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(LinearDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(LinearDensityUnit.KilogramPerMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(LinearDensityUnit.GramPerMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(LinearDensityUnit.GramPerMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Kilo.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(LinearDensityUnit unit, double value)
      => unit switch
      {
        LinearDensityUnit.KilogramPerMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(LinearDensityUnit unit, double value)
      => unit switch
      {
        LinearDensityUnit.KilogramPerMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, LinearDensityUnit from, LinearDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(LinearDensityUnit unit)
      => unit switch
      {
        LinearDensityUnit.KilogramPerMeter => 1,

        LinearDensityUnit.GramPerMeter => 1000,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(LinearDensityUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural);

    public static string GetUnitSymbol(LinearDensityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.LinearDensityUnit.KilogramPerMeter => "kg/m",

        Quantities.LinearDensityUnit.GramPerMeter => "g/m",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(LinearDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(LinearDensityUnit unit = LinearDensityUnit.KilogramPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="LinearDensity.Value"/> property is in <see cref="LinearDensityUnit.KilogramPerCubicMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
