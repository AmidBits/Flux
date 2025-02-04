namespace Flux.Quantities
{
  public enum PermeabilityUnit
  {
    /// <summary>This is the default unit for <see cref="Permeability"/>.</summary>
    HenryPerMeter,
  }

  /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Force"/>
  public readonly record struct Permeability
    : System.IComparable, System.IComparable<Permeability>, System.IFormattable, ISiUnitValueQuantifiable<double, PermeabilityUnit>
  {
    private readonly double m_value;

    public Permeability(double value, PermeabilityUnit unit = PermeabilityUnit.HenryPerMeter) => m_value = ConvertFromUnit(unit, value);

    public Permeability(MetricPrefix prefix, double henryPerMeter) => m_value = prefix.ConvertTo(henryPerMeter, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(Permeability a, Permeability b) => a.CompareTo(b) < 0;
    public static bool operator >(Permeability a, Permeability b) => a.CompareTo(b) > 0;
    public static bool operator <=(Permeability a, Permeability b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Permeability a, Permeability b) => a.CompareTo(b) >= 0;

    public static Permeability operator -(Permeability v) => new(-v.m_value);
    public static Permeability operator *(Permeability a, Permeability b) => new(a.m_value * b.m_value);
    public static Permeability operator /(Permeability a, Permeability b) => new(a.m_value / b.m_value);
    public static Permeability operator %(Permeability a, Permeability b) => new(a.m_value % b.m_value);
    public static Permeability operator +(Permeability a, Permeability b) => new(a.m_value + b.m_value);
    public static Permeability operator -(Permeability a, Permeability b) => new(a.m_value - b.m_value);
    public static Permeability operator *(Permeability a, double b) => new(a.m_value * b);
    public static Permeability operator /(Permeability a, double b) => new(a.m_value / b);
    public static Permeability operator %(Permeability a, double b) => new(a.m_value % b);
    public static Permeability operator +(Permeability a, double b) => new(a.m_value + b);
    public static Permeability operator -(Permeability a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Permeability o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Permeability other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(PermeabilityUnit.HenryPerMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(PermeabilityUnit.HenryPerMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(PermeabilityUnit.HenryPerMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(PermeabilityUnit unit, double value)
      => unit switch
      {
        PermeabilityUnit.HenryPerMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(PermeabilityUnit unit, double value)
      => unit switch
      {
        PermeabilityUnit.HenryPerMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, PermeabilityUnit from, PermeabilityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(PermeabilityUnit unit)
      => unit switch
      {
        PermeabilityUnit.HenryPerMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(PermeabilityUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(PermeabilityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.PermeabilityUnit.HenryPerMeter => "H/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(PermeabilityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(PermeabilityUnit unit = PermeabilityUnit.HenryPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Permeability.Value"/> property is in <see cref="PermeabilityUnit.HenryPerMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
