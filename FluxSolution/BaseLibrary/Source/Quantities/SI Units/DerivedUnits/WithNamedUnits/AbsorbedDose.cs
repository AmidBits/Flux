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
    : System.IComparable, System.IComparable<AbsorbedDose>, System.IFormattable, IUnitValueQuantifiable<double, AbsorbedDoseUnit>
  {
    private readonly double m_value;

    public AbsorbedDose(double value, AbsorbedDoseUnit unit = AbsorbedDoseUnit.Gray)
      => m_value = unit switch
      {
        AbsorbedDoseUnit.Gray => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators

    public static bool operator <(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) < 0;
    public static bool operator <=(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) <= 0;
    public static bool operator >(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) > 0;
    public static bool operator >=(AbsorbedDose a, AbsorbedDose b) => a.CompareTo(b) >= 0;

    public static AbsorbedDose operator -(AbsorbedDose v) => new(-v.m_value);
    public static AbsorbedDose operator +(AbsorbedDose a, double b) => new(a.m_value + b);
    public static AbsorbedDose operator +(AbsorbedDose a, AbsorbedDose b) => a + b.m_value;
    public static AbsorbedDose operator /(AbsorbedDose a, double b) => new(a.m_value / b);
    public static AbsorbedDose operator /(AbsorbedDose a, AbsorbedDose b) => a / b.m_value;
    public static AbsorbedDose operator *(AbsorbedDose a, double b) => new(a.m_value * b);
    public static AbsorbedDose operator *(AbsorbedDose a, AbsorbedDose b) => a * b.m_value;
    public static AbsorbedDose operator %(AbsorbedDose a, double b) => new(a.m_value % b);
    public static AbsorbedDose operator %(AbsorbedDose a, AbsorbedDose b) => a % b.m_value;
    public static AbsorbedDose operator -(AbsorbedDose a, double b) => new(a.m_value - b);
    public static AbsorbedDose operator -(AbsorbedDose a, AbsorbedDose b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is AbsorbedDose o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(AbsorbedDose other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(AbsorbedDoseUnit.Gray, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="AbsorbedDose.Value"/> property is in <see cref="AbsorbedDoseUnit.Gray"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(AbsorbedDoseUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(AbsorbedDoseUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.AbsorbedDoseUnit.Gray => "Gy",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(AbsorbedDoseUnit unit)
      => unit switch
      {
        AbsorbedDoseUnit.Gray => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(AbsorbedDoseUnit unit = AbsorbedDoseUnit.Gray, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitName(unit, preferPlural));

    public string ToUnitValueSymbolString(AbsorbedDoseUnit unit = AbsorbedDoseUnit.Gray, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitSymbol(unit, preferUnicode));

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
