namespace Flux.Units
{
  /// <summary>
  /// <para>Voltage, unit of volt.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Electric_potential"/></para>
  /// </summary>
  public readonly record struct ElectricPotential
    : System.IComparable, System.IComparable<ElectricPotential>, System.IFormattable, ISiUnitValueQuantifiable<double, ElectricPotentialUnit>
  {
    private readonly double m_value;

    public ElectricPotential(double value, ElectricPotentialUnit unit = ElectricPotentialUnit.Volt) => m_value = ConvertFromUnit(unit, value);

    public ElectricPotential(MetricPrefix prefix, double volt) => m_value = prefix.ChangePrefix(volt, MetricPrefix.Unprefixed);


    #region Static methods

    /// <summary>Creates a new Voltage instance from the specified current and resistance.</summary>
    /// <param name="current"></param>
    /// <param name="resistance"></param>
    public static ElectricPotential From(ElectricCurrent current, ElectricalResistance resistance) => new(current.Value * resistance.Value);

    /// <summary>Creates a new Voltage instance from the specified power and current.</summary>
    /// <param name="power"></param>
    /// <param name="current"></param>
    public static ElectricPotential From(Power power, ElectricCurrent current) => new(power.Value / current.Value);

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(ElectricPotential a, ElectricPotential b) => a.CompareTo(b) < 0;
    public static bool operator >(ElectricPotential a, ElectricPotential b) => a.CompareTo(b) > 0;
    public static bool operator <=(ElectricPotential a, ElectricPotential b) => a.CompareTo(b) <= 0;
    public static bool operator >=(ElectricPotential a, ElectricPotential b) => a.CompareTo(b) >= 0;

    public static ElectricPotential operator -(ElectricPotential v) => new(-v.m_value);
    public static ElectricPotential operator *(ElectricPotential a, ElectricPotential b) => new(a.m_value * b.m_value);
    public static ElectricPotential operator /(ElectricPotential a, ElectricPotential b) => new(a.m_value / b.m_value);
    public static ElectricPotential operator %(ElectricPotential a, ElectricPotential b) => new(a.m_value % b.m_value);
    public static ElectricPotential operator +(ElectricPotential a, ElectricPotential b) => new(a.m_value + b.m_value);
    public static ElectricPotential operator -(ElectricPotential a, ElectricPotential b) => new(a.m_value - b.m_value);
    public static ElectricPotential operator *(ElectricPotential a, double b) => new(a.m_value * b);
    public static ElectricPotential operator /(ElectricPotential a, double b) => new(a.m_value / b);
    public static ElectricPotential operator %(ElectricPotential a, double b) => new(a.m_value % b);
    public static ElectricPotential operator +(ElectricPotential a, double b) => new(a.m_value + b);
    public static ElectricPotential operator -(ElectricPotential a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricPotential o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricPotential other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode)
      => prefix switch
      {
        MetricPrefix.Kilo => preferUnicode ? "\u33B8" : "k" + ElectricPotentialUnit.Volt.GetUnitSymbol(preferUnicode),

        _ => prefix.GetMetricPrefixSymbol(preferUnicode) + ElectricPotentialUnit.Volt.GetUnitSymbol(preferUnicode),
      };

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + GetSiUnitSymbol(prefix, false);

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ElectricPotentialUnit unit, double value)
      => unit switch
      {
        ElectricPotentialUnit.Volt => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(ElectricPotentialUnit unit, double value)
      => unit switch
      {
        ElectricPotentialUnit.Volt => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, ElectricPotentialUnit from, ElectricPotentialUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(ElectricPotentialUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ElectricPotentialUnit unit = ElectricPotentialUnit.Volt, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + Unicode.UnicodeSpacing.Space.ToSpacingString()
        + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    ///  <para>The unit of the <see cref="ElectricPotential.Value"/> property is in <see cref="ElectricPotentialUnit.Volt"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
