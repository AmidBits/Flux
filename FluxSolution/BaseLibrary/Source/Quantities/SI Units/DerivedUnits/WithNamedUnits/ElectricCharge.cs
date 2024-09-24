namespace Flux.Quantities
{
  public enum ElectricChargeUnit
  {
    /// <summary>This is the default unit for <see cref="ElectricCharge"/>.</summary>
    Coulomb,
  }

  /// <summary>Electric charge, unit of Coulomb.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Electric_charge"/>
  public readonly record struct ElectricCharge
    : System.IComparable, System.IComparable<ElectricCharge>, System.IFormattable, ISiPrefixValueQuantifiable<double, ElectricChargeUnit>
  {
    public static ElectricCharge ElementaryCharge => new(1.602176634e-19);

    private readonly double m_value;

    public ElectricCharge(double value, ElectricChargeUnit unit = ElectricChargeUnit.Coulomb)
      => m_value = unit switch
      {
        ElectricChargeUnit.Coulomb => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators

    public static bool operator <(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) >= 0;

    public static ElectricCharge operator -(ElectricCharge v) => new(-v.m_value);
    public static ElectricCharge operator +(ElectricCharge a, double b) => new(a.m_value + b);
    public static ElectricCharge operator +(ElectricCharge a, ElectricCharge b) => a + b.m_value;
    public static ElectricCharge operator /(ElectricCharge a, double b) => new(a.m_value / b);
    public static ElectricCharge operator /(ElectricCharge a, ElectricCharge b) => a / b.m_value;
    public static ElectricCharge operator *(ElectricCharge a, double b) => new(a.m_value * b);
    public static ElectricCharge operator *(ElectricCharge a, ElectricCharge b) => a * b.m_value;
    public static ElectricCharge operator %(ElectricCharge a, double b) => new(a.m_value % b);
    public static ElectricCharge operator %(ElectricCharge a, ElectricCharge b) => a % b.m_value;
    public static ElectricCharge operator -(ElectricCharge a, double b) => new(a.m_value - b);
    public static ElectricCharge operator -(ElectricCharge a, ElectricCharge b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricCharge o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricCharge other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixValueSymbolString(MetricPrefix.Unprefixed);

    // ISiUnitValueQuantifiable<>
    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(ElectricChargeUnit.Coulomb, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(ElectricChargeUnit.Coulomb, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixValueNameString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="ElectricCharge.Value"/> property is in <see cref="ElectricChargeUnit.Ohm"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(ElectricChargeUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(ElectricChargeUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.ElectricChargeUnit.Coulomb => "C",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ElectricChargeUnit unit)
        => unit switch
        {
          ElectricChargeUnit.Coulomb => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

    public string ToUnitValueNameString(ElectricChargeUnit unit = ElectricChargeUnit.Coulomb, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(ElectricChargeUnit unit = ElectricChargeUnit.Coulomb, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
