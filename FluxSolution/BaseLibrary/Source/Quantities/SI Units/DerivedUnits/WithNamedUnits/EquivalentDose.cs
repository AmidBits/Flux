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
    : System.IComparable, System.IComparable<EquivalentDose>, System.IFormattable, ISiPrefixValueQuantifiable<double, EquivalentDoseUnit>
  {
    private readonly double m_value;

    public EquivalentDose(double value, EquivalentDoseUnit unit = EquivalentDoseUnit.Sievert)
      => m_value = unit switch
      {
        EquivalentDoseUnit.Sievert => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitValueSymbolString(EquivalentDoseUnit.Sievert, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="EquivalentDose.Value"/> property is in <see cref="EquivalentDoseUnit.Sievert"/>.</para>
    /// </summary>
    public double Value => m_value;

    // ISiPrefixValueQuantifiable<>
    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetUnitName() + GetUnitName(EquivalentDoseUnit.Sievert, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetUnitSymbol(preferUnicode) + GetUnitSymbol(EquivalentDoseUnit.Sievert, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

    public string ToSiPrefixValueNameString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
      => GetSiPrefixValue(prefix).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetSiPrefixValue(prefix).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    // IUnitQuantifiable<>
    public string GetUnitName(EquivalentDoseUnit unit, bool preferPlural) => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(EquivalentDoseUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.EquivalentDoseUnit.Sievert => preferUnicode ? "\u33DC" : "Sv",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(EquivalentDoseUnit unit)
      => unit switch
      {
        EquivalentDoseUnit.Sievert => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(EquivalentDoseUnit unit = EquivalentDoseUnit.Sievert, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(EquivalentDoseUnit unit = EquivalentDoseUnit.Sievert, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
