namespace Flux.Quantities
{
  public enum ElectricPotentialUnit
  {
    /// <summary>This is the default unit for <see cref="ElectricPotential"/>.</summary>
    Volt,
  }

  /// <summary>Voltage unit of volt.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Electric_potential"/>
  public readonly record struct ElectricPotential
    : System.IComparable, System.IComparable<ElectricPotential>, System.IFormattable, ISiUnitValueQuantifiable<double, ElectricPotentialUnit>
  {
    private readonly double m_value;

    public ElectricPotential(double value, ElectricPotentialUnit unit = ElectricPotentialUnit.Volt) => m_value = ConvertFromUnit(unit, value);

    public ElectricPotential(MetricPrefix prefix, double volt) => m_value = prefix.ConvertTo(volt, MetricPrefix.Unprefixed);


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

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(ElectricPotentialUnit.Volt, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix switch
    {
      MetricPrefix.Kilo => preferUnicode ? "\u33B8" : "k" + GetUnitSymbol(ElectricPotentialUnit.Volt, preferUnicode),

      _ => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(ElectricPotentialUnit.Volt, preferUnicode),
    };

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(ElectricPotentialUnit unit, double value)
      => unit switch
      {
        ElectricPotentialUnit.Volt => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(ElectricPotentialUnit unit, double value)
      => unit switch
      {
        ElectricPotentialUnit.Volt => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, ElectricPotentialUnit from, ElectricPotentialUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(ElectricPotentialUnit unit)
      => unit switch
      {
        ElectricPotentialUnit.Volt => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(ElectricPotentialUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural);

    public static string GetUnitSymbol(ElectricPotentialUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.ElectricPotentialUnit.Volt => "V",

        //Quantities.VoltageUnit.KiloVolt => preferUnicode ? "\u33B8" : "kV",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };


    public double GetUnitValue(ElectricPotentialUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ElectricPotentialUnit unit = ElectricPotentialUnit.Volt, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

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
