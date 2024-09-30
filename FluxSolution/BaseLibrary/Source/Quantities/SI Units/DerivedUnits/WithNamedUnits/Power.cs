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

    public Power(double value, PowerUnit unit = PowerUnit.Watt) => m_value = ConvertFromUnit(unit, value);

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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Power.Value"/> property is in <see cref="PowerUnit.Watt"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(PowerUnit.Watt, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(PowerUnit.Watt, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    public static double ConvertFromUnit(PowerUnit unit, double value)
      => unit switch
      {
        PowerUnit.Watt => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(PowerUnit unit, double value)
      => unit switch
      {
        PowerUnit.Watt => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double GetUnitFactor(PowerUnit unit)
      => unit switch
      {
        PowerUnit.Watt => 1,

        _ => throw new System.NotImplementedException()
      };

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

    public double GetUnitValue(PowerUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(PowerUnit unit = PowerUnit.Watt, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
