namespace Flux.Quantities
{
  public enum AbsorbedDoseUnit
  {
    /// <summary>This is the default unit for <see cref="AbsorbedDose"/>.</summary>
    Gray,
  }

  /// <summary>
  /// <para>Absorbed dose, unit of gray. This is an SI derived quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Absorbed_dose"/></para>
  /// </summary>
  public readonly record struct AbsorbedDose
    : System.IComparable, System.IComparable<AbsorbedDose>, System.IFormattable, ISiUnitValueQuantifiable<double, AbsorbedDoseUnit>
  {
    private readonly double m_value;

    public AbsorbedDose(double value, AbsorbedDoseUnit unit = AbsorbedDoseUnit.Gray) => m_value = ConvertFromUnit(unit, value);

    public AbsorbedDose(MetricPrefix prefix, double gray) => m_value = prefix.ConvertTo(gray, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) < 0;
    public static bool operator >(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) > 0;
    public static bool operator <=(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) <= 0;
    public static bool operator >=(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) >= 0;

    public static AbsorbedDose operator -(AbsorbedDose v) => new(-v.m_value);
    public static AbsorbedDose operator *(AbsorbedDose a, AbsorbedDose b) => new(a.m_value * b.m_value);
    public static AbsorbedDose operator /(AbsorbedDose a, AbsorbedDose b) => new(a.m_value / b.m_value);
    public static AbsorbedDose operator %(AbsorbedDose a, AbsorbedDose b) => new(a.m_value % b.m_value);
    public static AbsorbedDose operator +(AbsorbedDose a, AbsorbedDose b) => new(a.m_value + b.m_value);
    public static AbsorbedDose operator -(AbsorbedDose a, AbsorbedDose b) => new(a.m_value - b.m_value);
    public static AbsorbedDose operator *(AbsorbedDose a, double b) => new(a.m_value * b);
    public static AbsorbedDose operator /(AbsorbedDose a, double b) => new(a.m_value / b);
    public static AbsorbedDose operator %(AbsorbedDose a, double b) => new(a.m_value % b);
    public static AbsorbedDose operator +(AbsorbedDose a, double b) => new(a.m_value + b);
    public static AbsorbedDose operator -(AbsorbedDose a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is AbsorbedDose o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(AbsorbedDose other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiPrefixValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(AbsorbedDoseUnit.Gray, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(AbsorbedDoseUnit.Gray, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(AbsorbedDoseUnit unit, double value)
      => unit switch
      {
        AbsorbedDoseUnit.Gray => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(AbsorbedDoseUnit unit, double value)
      => unit switch
      {
        AbsorbedDoseUnit.Gray => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, AbsorbedDoseUnit from, AbsorbedDoseUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(AbsorbedDoseUnit unit)
      => unit switch
      {
        AbsorbedDoseUnit.Gray => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(AbsorbedDoseUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(AbsorbedDoseUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.AbsorbedDoseUnit.Gray => "Gy",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AbsorbedDoseUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AbsorbedDoseUnit unit = AbsorbedDoseUnit.Gray, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="AbsorbedDose.Value"/> property is in <see cref="AbsorbedDoseUnit.Gray"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
