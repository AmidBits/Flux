namespace Flux.Quantities
{
  public enum CurrentDensityUnit
  {
    /// <summary>This is the default unit for <see cref="CurrentDensity"/>.</summary>
    AmperePerSquareMeter,
  }

  /// <summary>Current density, unit of ampere per square meter.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Current_density"/>
  public readonly record struct CurrentDensity
    : System.IComparable, System.IComparable<CurrentDensity>, System.IFormattable, ISiUnitValueQuantifiable<double, CurrentDensityUnit>
  {
    private readonly double m_value;

    public CurrentDensity(double value, CurrentDensityUnit unit = CurrentDensityUnit.AmperePerSquareMeter) => m_value = ConvertFromUnit(unit, value);

    public CurrentDensity(MetricPrefix prefix, double amperePerSquareMeter) => m_value = prefix.ConvertTo(amperePerSquareMeter, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) < 0;
    public static bool operator >(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) > 0;
    public static bool operator <=(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) <= 0;
    public static bool operator >=(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) >= 0;

    public static CurrentDensity operator -(CurrentDensity v) => new(-v.m_value);
    public static CurrentDensity operator *(CurrentDensity a, CurrentDensity b) => new(a.m_value * b.m_value);
    public static CurrentDensity operator /(CurrentDensity a, CurrentDensity b) => new(a.m_value / b.m_value);
    public static CurrentDensity operator %(CurrentDensity a, CurrentDensity b) => new(a.m_value % b.m_value);
    public static CurrentDensity operator +(CurrentDensity a, CurrentDensity b) => new(a.m_value + b.m_value);
    public static CurrentDensity operator -(CurrentDensity a, CurrentDensity b) => new(a.m_value - b.m_value);
    public static CurrentDensity operator *(CurrentDensity a, double b) => new(a.m_value * b);
    public static CurrentDensity operator /(CurrentDensity a, double b) => new(a.m_value / b);
    public static CurrentDensity operator %(CurrentDensity a, double b) => new(a.m_value % b);
    public static CurrentDensity operator +(CurrentDensity a, double b) => new(a.m_value + b);
    public static CurrentDensity operator -(CurrentDensity a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is CurrentDensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(CurrentDensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(CurrentDensityUnit.AmperePerSquareMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(CurrentDensityUnit.AmperePerSquareMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(CurrentDensityUnit.AmperePerSquareMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(CurrentDensityUnit unit, double value)
      => unit switch
      {
        CurrentDensityUnit.AmperePerSquareMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(CurrentDensityUnit unit, double value)
      => unit switch
      {
        CurrentDensityUnit.AmperePerSquareMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, CurrentDensityUnit from, CurrentDensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(CurrentDensityUnit unit)
      => unit switch
      {
        CurrentDensityUnit.AmperePerSquareMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(CurrentDensityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(CurrentDensityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.CurrentDensityUnit.AmperePerSquareMeter => "A/m²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(CurrentDensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(CurrentDensityUnit unit = CurrentDensityUnit.AmperePerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="CurrentDensity.Value"/> property is in <see cref="CurrentDensityUnit.AmperePerSquareMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
