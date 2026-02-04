namespace Flux.Units
{
  /// <summary>
  /// <para>Electric current. SI unit of ampere. This is a base quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Electric_current"/></para>
  /// </summary>
  public readonly record struct ElectricCurrent
    : System.IComparable, System.IComparable<ElectricCurrent>, System.IEquatable<ElectricCurrent>, System.IFormattable, ISiUnitValueQuantifiable<double, ElectricCurrentUnit>
  {
    private readonly double m_value;

    public ElectricCurrent(double value, ElectricCurrentUnit unit = ElectricCurrentUnit.Ampere) => m_value = ConvertFromUnit(unit, value);

    /// <summary>
    /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="ElectricCurrentUnit.Ampere"/>, e.g. <see cref="MetricPrefix.Milli"/> for milliamperes.</para>
    /// </summary>
    /// <param name="amperes"></param>
    /// <param name="prefix"></param>
    public ElectricCurrent(MetricPrefix prefix, double ampere) => m_value = prefix.ConvertPrefix(ampere, MetricPrefix.Unprefixed);

    #region Static methods

    /// <summary>Creates a new ElectricCurrent instance from power and voltage.</summary>
    /// <param name="power"></param>
    /// <param name="voltage"></param>
    public static ElectricCurrent From(Power power, ElectricPotential voltage) => new(power.Value / voltage.Value);

    /// <summary>Creates a new ElectricCurrent instance from voltage and resistance.</summary>
    /// <param name="voltage"></param>
    /// <param name="resistance"></param>
    public static ElectricCurrent From(ElectricPotential voltage, ElectricalResistance resistance) => new(voltage.Value / resistance.Value);

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) < 0;
    public static bool operator >(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) > 0;
    public static bool operator <=(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) <= 0;
    public static bool operator >=(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) >= 0;

    public static ElectricCurrent operator -(ElectricCurrent v) => new(-v.m_value);
    public static ElectricCurrent operator *(ElectricCurrent a, ElectricCurrent b) => new(a.m_value * b.m_value);
    public static ElectricCurrent operator /(ElectricCurrent a, ElectricCurrent b) => new(a.m_value / b.m_value);
    public static ElectricCurrent operator %(ElectricCurrent a, ElectricCurrent b) => new(a.m_value % b.m_value);
    public static ElectricCurrent operator +(ElectricCurrent a, ElectricCurrent b) => new(a.m_value + b.m_value);
    public static ElectricCurrent operator -(ElectricCurrent a, ElectricCurrent b) => new(a.m_value - b.m_value);
    public static ElectricCurrent operator *(ElectricCurrent a, double b) => new(a.m_value * b);
    public static ElectricCurrent operator /(ElectricCurrent a, double b) => new(a.m_value / b);
    public static ElectricCurrent operator %(ElectricCurrent a, double b) => new(a.m_value % b);
    public static ElectricCurrent operator +(ElectricCurrent a, double b) => new(a.m_value + b);
    public static ElectricCurrent operator -(ElectricCurrent a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricCurrent o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricCurrent other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiPrefixValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + ElectricCurrentUnit.Ampere.GetUnitSymbol();

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ElectricCurrentUnit unit, double value)
      => unit switch
      {
        ElectricCurrentUnit.Ampere => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(ElectricCurrentUnit unit, double value)
      => unit switch
      {
        ElectricCurrentUnit.Ampere => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, ElectricCurrentUnit from, ElectricCurrentUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(ElectricCurrentUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ElectricCurrentUnit unit = ElectricCurrentUnit.Ampere, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(Number.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="ElectricCurrent.Value"/> property is in <see cref="ElectricCurrentUnit.Ampere"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
