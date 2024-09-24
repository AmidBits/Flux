namespace Flux.Quantities
{
  public enum PowerUnit
  {
    /// <summary>This is the default unit for <see cref="Power"/>.</summary>
    Watt,
    KiloWatt,
    MegaWatt,
  }

  /// <summary>Power unit of watt.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Power"/>
  public readonly record struct Power
    : System.IComparable, System.IComparable<Power>, System.IFormattable, ISiPrefixValueQuantifiable<double, PowerUnit>
  {
    private readonly double m_value;

    public Power(double value, PowerUnit unit = PowerUnit.Watt)
      => m_value = unit switch
      {
        PowerUnit.Watt => value,
        PowerUnit.KiloWatt => value * 1000,
        PowerUnit.MegaWatt => value * 1000000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    /// <summary>Creates a new Power instance from the specified <paramref name="current"/> and <paramref name="voltage"/>.</summary>
    /// <param name="current"></param>
    /// <param name="voltage"></param>
    public Power(ElectricCurrent current, Voltage voltage) : this(current.Value * voltage.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Power a, Power b) => a.CompareTo(b) < 0;
    public static bool operator <=(Power a, Power b) => a.CompareTo(b) <= 0;
    public static bool operator >(Power a, Power b) => a.CompareTo(b) > 0;
    public static bool operator >=(Power a, Power b) => a.CompareTo(b) >= 0;

    public static Power operator -(Power v) => new(-v.m_value);
    public static Power operator +(Power a, double b) => new(a.m_value + b);
    public static Power operator +(Power a, Power b) => a + b.m_value;
    public static Power operator /(Power a, double b) => new(a.m_value / b);
    public static Power operator /(Power a, Power b) => a / b.m_value;
    public static Power operator *(Power a, double b) => new(a.m_value * b);
    public static Power operator *(Power a, Power b) => a * b.m_value;
    public static Power operator %(Power a, double b) => new(a.m_value % b);
    public static Power operator %(Power a, Power b) => a % b.m_value;
    public static Power operator -(Power a, double b) => new(a.m_value - b);
    public static Power operator -(Power a, Power b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Power o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Power other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixValueSymbolString(MetricPrefix.Unprefixed);

    // ISiUnitValueQuantifiable<>
    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(PowerUnit.Watt, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(PowerUnit.Watt, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixValueNameString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="Power.Value"/> property is in <see cref="PowerUnit.Watt"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(PowerUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(PowerUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.PowerUnit.Watt => "W",
        Quantities.PowerUnit.KiloWatt => preferUnicode ? "\u33BE" : "kW",
        Quantities.PowerUnit.MegaWatt => preferUnicode ? "\u33BF" : "MW",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(PowerUnit unit)
      => unit switch
      {
        PowerUnit.Watt => m_value,
        PowerUnit.KiloWatt => m_value / 1000,
        PowerUnit.MegaWatt => m_value / 1000000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(PowerUnit unit = PowerUnit.Watt, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(PowerUnit unit = PowerUnit.Watt, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
