namespace Flux.Quantities
{
  public enum LuminousIntensityUnit
  {
    Candela,
  }

  /// <summary>
  /// <para>Luminous intensity. SI unit of candela. This is a base quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Luminous_intensity"/></para>
  /// </summary>
  public readonly record struct LuminousIntensity
    : System.IComparable, System.IComparable<LuminousIntensity>, System.IFormattable, ISiPrefixValueQuantifiable<double, LuminousIntensityUnit>
  {
    private readonly double m_value;

    public LuminousIntensity(double value, LuminousIntensityUnit unit = LuminousIntensityUnit.Candela)
      => m_value = unit switch
      {
        LuminousIntensityUnit.Candela => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    /// <summary>
    /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="AmountOfSubstanceUnit.Mole"/>, e.g. <see cref="MetricPrefix.Mega"/> for megacandelas.</para>
    /// </summary>
    /// <param name="candelas"></param>
    /// <param name="prefix"></param>
    public LuminousIntensity(double candelas, MetricPrefix prefix) => m_value = prefix.Convert(candelas, MetricPrefix.NoPrefix);

    #region Static methods
    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) < 0;
    public static bool operator <=(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) <= 0;
    public static bool operator >(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) > 0;
    public static bool operator >=(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) >= 0;

    public static LuminousIntensity operator -(LuminousIntensity v) => new(-v.m_value);
    public static LuminousIntensity operator +(LuminousIntensity a, double b) => new(a.m_value + b);
    public static LuminousIntensity operator +(LuminousIntensity a, LuminousIntensity b) => a + b.m_value;
    public static LuminousIntensity operator /(LuminousIntensity a, double b) => new(a.m_value / b);
    public static LuminousIntensity operator /(LuminousIntensity a, LuminousIntensity b) => a / b.m_value;
    public static LuminousIntensity operator *(LuminousIntensity a, double b) => new(a.m_value * b);
    public static LuminousIntensity operator *(LuminousIntensity a, LuminousIntensity b) => a * b.m_value;
    public static LuminousIntensity operator %(LuminousIntensity a, double b) => new(a.m_value % b);
    public static LuminousIntensity operator %(LuminousIntensity a, LuminousIntensity b) => a % b.m_value;
    public static LuminousIntensity operator -(LuminousIntensity a, double b) => new(a.m_value - b);
    public static LuminousIntensity operator -(LuminousIntensity a, LuminousIntensity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is LuminousIntensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(LuminousIntensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitValueSymbolString(LuminousIntensityUnit.Candela, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="LuminousIntensity.Value"/> property is in <see cref="LuminousIntensityUnit.Candela"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetUnitName() + GetUnitName(LuminousIntensityUnit.Candela, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetUnitSymbol(preferUnicode) + GetUnitSymbol(LuminousIntensityUnit.Candela, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

    public string ToSiPrefixValueNameString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
      => GetSiPrefixValue(prefix).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetSiPrefixValue(prefix).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    public string GetUnitName(LuminousIntensityUnit unit, bool preferPlural) => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(LuminousIntensityUnit unit, bool preferUnicode)
      => unit switch
      {
        LuminousIntensityUnit.Candela => preferUnicode ? "\u33C5" : "cd",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(LuminousIntensityUnit unit)
      => unit switch
      {
        LuminousIntensityUnit.Candela => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(LuminousIntensityUnit unit = LuminousIntensityUnit.Candela, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(LuminousIntensityUnit unit = LuminousIntensityUnit.Candela, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
