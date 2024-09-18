namespace Flux.Quantities
{
  public enum EnergyUnit
  {
    /// <summary>This is the default unit for <see cref="Energy"/>.</summary>
    Joule,
    ElectronVolt,
    Calorie,
    WattHour,
    KilowattHour
  }

  /// <summary>Energy unit of Joule.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Energy"/>
  public readonly record struct Energy
    : System.IComparable, System.IComparable<Energy>, System.IFormattable, ISiPrefixValueQuantifiable<double, EnergyUnit>
  {
    private readonly double m_value;

    public Energy(double value, EnergyUnit unit = EnergyUnit.Joule)
      => m_value = unit switch
      {
        EnergyUnit.Joule => value,
        EnergyUnit.ElectronVolt => value / 1.602176634e-19,
        EnergyUnit.Calorie => value / 4.184,
        EnergyUnit.WattHour => value / 3.6e3,
        EnergyUnit.KilowattHour => value / 3.6e6,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Energy a, Energy b) => a.CompareTo(b) < 0;
    public static bool operator <=(Energy a, Energy b) => a.CompareTo(b) <= 0;
    public static bool operator >(Energy a, Energy b) => a.CompareTo(b) > 0;
    public static bool operator >=(Energy a, Energy b) => a.CompareTo(b) >= 0;

    public static Energy operator -(Energy v) => new(-v.m_value);
    public static Energy operator +(Energy a, double b) => new(a.m_value + b);
    public static Energy operator +(Energy a, Energy b) => a + b.m_value;
    public static Energy operator /(Energy a, double b) => new(a.m_value / b);
    public static Energy operator /(Energy a, Energy b) => a / b.m_value;
    public static Energy operator *(Energy a, double b) => new(a.m_value * b);
    public static Energy operator *(Energy a, Energy b) => a * b.m_value;
    public static Energy operator %(Energy a, double b) => new(a.m_value % b);
    public static Energy operator %(Energy a, Energy b) => a % b.m_value;
    public static Energy operator -(Energy a, double b) => new(a.m_value - b);
    public static Energy operator -(Energy a, Energy b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Energy o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Energy other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitValueSymbolString(EnergyUnit.Joule, format, formatProvider);

    // ISiUnitValueQuantifiable<>
    public (MetricPrefix Prefix, EnergyUnit Unit) GetSiPrefixUnit(MetricPrefix prefix) => (prefix, EnergyUnit.Joule);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetUnitSymbol(preferUnicode) + GetUnitSymbol(GetSiPrefixUnit(prefix).Unit, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

    public string ToSiPrefixValueNameString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
      => GetSiPrefixValue(prefix).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + prefix.GetUnitName() + GetUnitName(GetSiPrefixUnit(prefix).Unit, preferPlural);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetSiPrefixValue(prefix).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="Energy.Value"/> property is in <see cref="EnergyUnit.Joule"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(EnergyUnit unit, bool preferPlural) => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(EnergyUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.EnergyUnit.Joule => "J",
        Quantities.EnergyUnit.ElectronVolt => "eV",
        Quantities.EnergyUnit.Calorie => preferUnicode ? "\u3388" : "cal",
        Quantities.EnergyUnit.WattHour => "W\u22C5h",
        Quantities.EnergyUnit.KilowattHour => "kW\u22C5h",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(EnergyUnit unit)
      => unit switch
      {
        EnergyUnit.Joule => m_value,
        EnergyUnit.ElectronVolt => m_value * 1.602176634e-19,
        EnergyUnit.Calorie => m_value * 4.184,
        EnergyUnit.WattHour => m_value * 3.6e3,
        EnergyUnit.KilowattHour => m_value * 3.6e6,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(EnergyUnit unit = EnergyUnit.Joule, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(EnergyUnit unit = EnergyUnit.Joule, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
