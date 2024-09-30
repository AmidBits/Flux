namespace Flux.Quantities
{
  public enum ElectricCurrentUnit
  {
    /// <summary>This is the default unit for <see cref="ElectricCurrent"/>.</summary>
    Ampere,
    Milliampere,
  }

  /// <summary>
  /// <para>Electric current. SI unit of ampere. This is a base quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Electric_current"/></para>
  /// </summary>
  public readonly record struct ElectricCurrent
    : System.IComparable, System.IComparable<ElectricCurrent>, System.IFormattable, ISiPrefixValueQuantifiable<double, ElectricCurrentUnit>
  {
    private readonly double m_value;

    public ElectricCurrent(double value, ElectricCurrentUnit unit = ElectricCurrentUnit.Ampere) => m_value = ConvertFromUnit(unit, value);

    /// <summary>
    /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="ElectricCurrentUnit.Ampere"/>, e.g. <see cref="MetricPrefix.Milli"/> for milliamperes.</para>
    /// </summary>
    /// <param name="amperes"></param>
    /// <param name="prefix"></param>
    public ElectricCurrent(double amperes, MetricPrefix prefix) => m_value = prefix.ConvertTo(amperes, MetricPrefix.Unprefixed);

    #region Static methods

    /// <summary>Creates a new ElectricCurrent instance from power and voltage.</summary>
    /// <param name="power"></param>
    /// <param name="voltage"></param>
    public static ElectricCurrent From(Power power, Voltage voltage) => new(power.Value / voltage.Value);

    /// <summary>Creates a new ElectricCurrent instance from voltage and resistance.</summary>
    /// <param name="voltage"></param>
    /// <param name="resistance"></param>
    public static ElectricCurrent From(Voltage voltage, ElectricalResistance resistance) => new(voltage.Value / resistance.Value);

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) >= 0;

    public static ElectricCurrent operator -(ElectricCurrent v) => new(-v.m_value);
    public static ElectricCurrent operator +(ElectricCurrent a, double b) => new(a.m_value + b);
    public static ElectricCurrent operator +(ElectricCurrent a, ElectricCurrent b) => a + b.m_value;
    public static ElectricCurrent operator /(ElectricCurrent a, double b) => new(a.m_value / b);
    public static ElectricCurrent operator /(ElectricCurrent a, ElectricCurrent b) => a / b.m_value;
    public static ElectricCurrent operator *(ElectricCurrent a, double b) => new(a.m_value * b);
    public static ElectricCurrent operator *(ElectricCurrent a, ElectricCurrent b) => a * b.m_value;
    public static ElectricCurrent operator %(ElectricCurrent a, double b) => new(a.m_value % b);
    public static ElectricCurrent operator %(ElectricCurrent a, ElectricCurrent b) => a % b.m_value;
    public static ElectricCurrent operator -(ElectricCurrent a, double b) => new(a.m_value - b);
    public static ElectricCurrent operator -(ElectricCurrent a, ElectricCurrent b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricCurrent o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricCurrent other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="ElectricCurrent.Value"/> property is in <see cref="ElectricCurrentUnit.Ampere"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiPrefixValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(ElectricCurrentUnit.Ampere, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(ElectricCurrentUnit.Ampere, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitQuantifiable<>

    public static double ConvertFromUnit(ElectricCurrentUnit unit, double value)
      => unit switch
      {
        ElectricCurrentUnit.Ampere => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(ElectricCurrentUnit unit, double value)
      => unit switch
      {
        ElectricCurrentUnit.Ampere => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double GetUnitFactor(ElectricCurrentUnit unit)
      => unit switch
      {
        ElectricCurrentUnit.Ampere => 1,
        ElectricCurrentUnit.Milliampere => 0.001,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(ElectricCurrentUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(ElectricCurrentUnit unit, bool preferUnicode)
      => unit switch
      {
        ElectricCurrentUnit.Ampere => "A",
        ElectricCurrentUnit.Milliampere => preferUnicode ? "\u3383" : "mA",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ElectricCurrentUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ElectricCurrentUnit unit = ElectricCurrentUnit.Ampere, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
