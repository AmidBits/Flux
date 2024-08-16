namespace Flux.Quantities
{
  public enum CatalyticActivityUnit
  {
    /// <summary>This is the default unit for <see cref="CatalyticActivity"/>. Katal = (mol/s).</summary>
    Katal,
  }

  /// <summary>Catalytic activity unit of Katal.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Catalysis"/>
  public readonly record struct CatalyticActivity
    : System.IComparable, System.IComparable<CatalyticActivity>, System.IFormattable, ISiPrefixValueQuantifiable<double, CatalyticActivityUnit>
  {
    private readonly double m_value;

    public CatalyticActivity(double value, CatalyticActivityUnit unit = CatalyticActivityUnit.Katal)
      => m_value = unit switch
      {
        CatalyticActivityUnit.Katal => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators

    public static bool operator <(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) < 0;
    public static bool operator <=(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) <= 0;
    public static bool operator >(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) > 0;
    public static bool operator >=(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) >= 0;

    public static CatalyticActivity operator -(CatalyticActivity v) => new(-v.m_value);
    public static CatalyticActivity operator +(CatalyticActivity a, double b) => new(a.m_value + b);
    public static CatalyticActivity operator +(CatalyticActivity a, CatalyticActivity b) => a + b.m_value;
    public static CatalyticActivity operator /(CatalyticActivity a, double b) => new(a.m_value / b);
    public static CatalyticActivity operator /(CatalyticActivity a, CatalyticActivity b) => a / b.m_value;
    public static CatalyticActivity operator *(CatalyticActivity a, double b) => new(a.m_value * b);
    public static CatalyticActivity operator *(CatalyticActivity a, CatalyticActivity b) => a * b.m_value;
    public static CatalyticActivity operator %(CatalyticActivity a, double b) => new(a.m_value % b);
    public static CatalyticActivity operator %(CatalyticActivity a, CatalyticActivity b) => a % b.m_value;
    public static CatalyticActivity operator -(CatalyticActivity a, double b) => new(a.m_value - b);
    public static CatalyticActivity operator -(CatalyticActivity a, CatalyticActivity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is CatalyticActivity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(CatalyticActivity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(CatalyticActivityUnit.Katal, format, formatProvider);

    // ISiUnitValueQuantifiable<>
    public (MetricPrefix Prefix, CatalyticActivityUnit Unit) GetSiPrefixUnit(MetricPrefix prefix) => (prefix, CatalyticActivityUnit.Katal);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetUnitSymbol(preferUnicode) + GetUnitSymbol(GetSiPrefixUnit(prefix).Unit, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    {
      var sb = new System.Text.StringBuilder();
      sb.Append(GetSiPrefixValue(prefix).ToString(format, formatProvider));
      sb.Append(unitSpacing.ToSpacingString());
      sb.Append(GetSiPrefixSymbol(prefix, preferUnicode));
      return sb.ToString();
    }

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="CatalyticActivity.Value"/> property is in <see cref="CatalyticActivityUnit.Katal"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(CatalyticActivityUnit unit, bool preferPlural)
      => unit.ToString() + GetUnitValue(unit).PluralStringSuffix();

    public string GetUnitSymbol(CatalyticActivityUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.CatalyticActivityUnit.Katal => preferUnicode ? "\u33CF" : "kat",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(CatalyticActivityUnit unit)
      => unit switch
      {
        CatalyticActivityUnit.Katal => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(CatalyticActivityUnit unit = CatalyticActivityUnit.Katal, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitName(unit, preferPlural));

    public string ToUnitValueSymbolString(CatalyticActivityUnit unit = CatalyticActivityUnit.Katal, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitSymbol(unit, preferUnicode));

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
