namespace Flux.Quantities
{
  public enum PowerUnit
  {
    /// <summary>This is the default unit for <see cref="Power"/>.</summary>
    Watt,
    //KiloWatt,
    //MegaWatt,
  }

  /// <summary>Power unit of watt.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Power"/>
  public readonly record struct Power
    : System.IComparable, System.IComparable<Power>, System.IFormattable, ISiUnitValueQuantifiable<double, PowerUnit>
  {
    private readonly double m_value;

    public Power(double value, PowerUnit unit = PowerUnit.Watt) => m_value = ConvertFromUnit(unit, value);

    public Power(MetricPrefix prefix, double watt) => m_value = prefix.ConvertTo(watt, MetricPrefix.Unprefixed);

    /// <summary>Creates a new Power instance from the specified <paramref name="current"/> and <paramref name="voltage"/>.</summary>
    /// <param name="current"></param>
    /// <param name="voltage"></param>
    public Power(ElectricCurrent current, ElectricPotential voltage) : this(current.Value * voltage.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Power a, Power b) => a.CompareTo(b) < 0;
    public static bool operator >(Power a, Power b) => a.CompareTo(b) > 0;
    public static bool operator <=(Power a, Power b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Power a, Power b) => a.CompareTo(b) >= 0;

    public static Power operator -(Power v) => new(-v.m_value);
    public static Power operator *(Power a, Power b) => new(a.m_value * b.m_value);
    public static Power operator /(Power a, Power b) => new(a.m_value / b.m_value);
    public static Power operator %(Power a, Power b) => new(a.m_value % b.m_value);
    public static Power operator +(Power a, Power b) => new(a.m_value + b.m_value);
    public static Power operator -(Power a, Power b) => new(a.m_value - b.m_value);
    public static Power operator *(Power a, double b) => new(a.m_value * b);
    public static Power operator /(Power a, double b) => new(a.m_value / b);
    public static Power operator %(Power a, double b) => new(a.m_value % b);
    public static Power operator +(Power a, double b) => new(a.m_value + b);
    public static Power operator -(Power a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Power o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Power other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(PowerUnit.Watt, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(PowerUnit.Watt, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(PowerUnit unit, double value)
      => unit switch
      {
        PowerUnit.Watt => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(PowerUnit unit, double value)
      => unit switch
      {
        PowerUnit.Watt => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, PowerUnit from, PowerUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(PowerUnit unit)
      => unit switch
      {
        PowerUnit.Watt => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(PowerUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(PowerUnit unit, bool preferUnicode)
      => unit switch
      {
        PowerUnit.Watt => "W",

        //PowerUnit.KiloWatt => preferUnicode ? "\u33BE" : "kW",
        //PowerUnit.MegaWatt => preferUnicode ? "\u33BF" : "MW",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(PowerUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(PowerUnit unit = PowerUnit.Watt, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Power.Value"/> property is in <see cref="PowerUnit.Watt"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
