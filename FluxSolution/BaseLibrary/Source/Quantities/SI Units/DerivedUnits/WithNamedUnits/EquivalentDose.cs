namespace Flux.Quantities
{
  public enum EquivalentDoseUnit
  {
    /// <summary>This is the default unit for <see cref="EquivalentDose"/>.</summary>
    Sievert,
  }

  /// <summary>Dose equivalent, unit of sievert.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Equivalent_dose"/>
  public readonly record struct EquivalentDose
    : System.IComparable, System.IComparable<EquivalentDose>, System.IFormattable, ISiUnitValueQuantifiable<double, EquivalentDoseUnit>
  {
    private readonly double m_value;

    public EquivalentDose(double value, EquivalentDoseUnit unit = EquivalentDoseUnit.Sievert) => m_value = ConvertFromUnit(unit, value);

    #region Overloaded operators

    public static bool operator <(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) < 0;
    public static bool operator <=(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) <= 0;
    public static bool operator >(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) > 0;
    public static bool operator >=(EquivalentDose a, EquivalentDose b) => a.CompareTo(b) >= 0;

    public static EquivalentDose operator -(EquivalentDose v) => new(-v.m_value);
    public static EquivalentDose operator +(EquivalentDose a, double b) => new(a.m_value + b);
    public static EquivalentDose operator +(EquivalentDose a, EquivalentDose b) => a + b.m_value;
    public static EquivalentDose operator /(EquivalentDose a, double b) => new(a.m_value / b);
    public static EquivalentDose operator /(EquivalentDose a, EquivalentDose b) => a / b.m_value;
    public static EquivalentDose operator *(EquivalentDose a, double b) => new(a.m_value * b);
    public static EquivalentDose operator *(EquivalentDose a, EquivalentDose b) => a * b.m_value;
    public static EquivalentDose operator %(EquivalentDose a, double b) => new(a.m_value % b);
    public static EquivalentDose operator %(EquivalentDose a, EquivalentDose b) => a % b.m_value;
    public static EquivalentDose operator -(EquivalentDose a, double b) => new(a.m_value - b);
    public static EquivalentDose operator -(EquivalentDose a, EquivalentDose b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is EquivalentDose o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(EquivalentDose other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiPrefixValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(EquivalentDoseUnit.Sievert, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(EquivalentDoseUnit.Sievert, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(EquivalentDoseUnit unit, double value)
      => unit switch
      {
        EquivalentDoseUnit.Sievert => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(EquivalentDoseUnit unit, double value)
      => unit switch
      {
        EquivalentDoseUnit.Sievert => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, EquivalentDoseUnit from, EquivalentDoseUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(EquivalentDoseUnit unit)
      => unit switch
      {
        EquivalentDoseUnit.Sievert => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(EquivalentDoseUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(EquivalentDoseUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.EquivalentDoseUnit.Sievert => preferUnicode ? "\u33DC" : "Sv",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(EquivalentDoseUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(EquivalentDoseUnit unit = EquivalentDoseUnit.Sievert, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

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
