namespace Flux.Units
{
  /// <summary>
  /// <para>Absorbed dose, unit of gray. This is an SI derived quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Absorbed_dose"/></para>
  /// </summary>
  public readonly record struct AbsorbedDose
    : System.IComparable, System.IComparable<AbsorbedDose>, System.IFormattable, ISiUnitValueQuantifiable<double, AbsorbedDoseUnit>
  {
    private readonly double m_value;

    public AbsorbedDose(double value, AbsorbedDoseUnit unit = AbsorbedDoseUnit.Gray) => m_value = ConvertFromUnit(unit, value);

    public AbsorbedDose(MetricPrefix prefix, double gray) => m_value = prefix.ChangePrefix(gray, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + AbsorbedDoseUnit.Gray.GetUnitSymbol();

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(AbsorbedDoseUnit unit, double value)
      => unit switch
      {
        AbsorbedDoseUnit.Gray => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(AbsorbedDoseUnit unit, double value)
      => unit switch
      {
        AbsorbedDoseUnit.Gray => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, AbsorbedDoseUnit from, AbsorbedDoseUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(AbsorbedDoseUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(AbsorbedDoseUnit unit = AbsorbedDoseUnit.Gray, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
          + Unicode.UnicodeSpacing.Space.ToSpacingString()
          + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

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
