namespace Flux.Units
{
  /// <summary>
  /// <para>Equivalent dose, unit of sievert.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Equivalent_dose"/></para>
  /// </summary>
  public readonly record struct EquivalentDose
    : System.IComparable, System.IComparable<EquivalentDose>, System.IFormattable, ISiUnitValueQuantifiable<double, EquivalentDoseUnit>
  {
    private readonly double m_value;

    public EquivalentDose(double value, EquivalentDoseUnit unit = EquivalentDoseUnit.Sievert) => m_value = ConvertFromUnit(unit, value);

    public EquivalentDose(MetricPrefix prefix, double sievert) => m_value = prefix.ChangePrefix(sievert, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) < 0;
    public static bool operator >(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) > 0;
    public static bool operator <=(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) <= 0;
    public static bool operator >=(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) >= 0;

    public static EquivalentDose operator -(EquivalentDose v) => new(-v.m_value);
    public static EquivalentDose operator *(EquivalentDose a, EquivalentDose b) => new(a.m_value * b.m_value);
    public static EquivalentDose operator /(EquivalentDose a, EquivalentDose b) => new(a.m_value / b.m_value);
    public static EquivalentDose operator %(EquivalentDose a, EquivalentDose b) => new(a.m_value % b.m_value);
    public static EquivalentDose operator +(EquivalentDose a, EquivalentDose b) => new(a.m_value + b.m_value);
    public static EquivalentDose operator -(EquivalentDose a, EquivalentDose b) => new(a.m_value - b.m_value);
    public static EquivalentDose operator *(EquivalentDose a, double b) => new(a.m_value * b);
    public static EquivalentDose operator /(EquivalentDose a, double b) => new(a.m_value / b);
    public static EquivalentDose operator %(EquivalentDose a, double b) => new(a.m_value % b);
    public static EquivalentDose operator +(EquivalentDose a, double b) => new(a.m_value + b);
    public static EquivalentDose operator -(EquivalentDose a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is EquivalentDose o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(EquivalentDose other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiPrefixValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + EquivalentDoseUnit.Sievert.GetUnitSymbol();

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(EquivalentDoseUnit unit, double value)
      => unit switch
      {
        EquivalentDoseUnit.Sievert => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(EquivalentDoseUnit unit, double value)
      => unit switch
      {
        EquivalentDoseUnit.Sievert => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, EquivalentDoseUnit from, EquivalentDoseUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(EquivalentDoseUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(EquivalentDoseUnit unit = EquivalentDoseUnit.Sievert, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + Unicode.UnicodeSpacing.Space.ToSpacingString()
        + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="EquivalentDose.Value"/> property is in <see cref="EquivalentDoseUnit.Sievert"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
