namespace Flux.Units
{
  /// <summary>
  /// <para>Power, unit of watt.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Power"/></para>
  /// </summary>
  public readonly record struct Power
    : System.IComparable, System.IComparable<Power>, System.IFormattable, ISiUnitValueQuantifiable<double, PowerUnit>
  {
    private readonly double m_value;

    public Power(double value, PowerUnit unit = PowerUnit.Watt) => m_value = ConvertFromUnit(unit, value);

    public Power(MetricPrefix prefix, double watt) => m_value = prefix.ChangePrefix(watt, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + PowerUnit.Watt.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(PowerUnit unit, double value)
      => unit switch
      {
        PowerUnit.Watt => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(PowerUnit unit, double value)
      => unit switch
      {
        PowerUnit.Watt => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, PowerUnit from, PowerUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(PowerUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(PowerUnit unit = PowerUnit.Watt, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + Unicode.UnicodeSpacing.Space.ToSpacingString()
        + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

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
