namespace Flux.Units
{
  /// <summary>
  /// <para>Electric resistance, unit of Ohm.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Electrical_resistance_and_conductance"/></para>
  /// </summary>
  public readonly record struct ElectricalResistance
    : System.IComparable, System.IComparable<ElectricalResistance>, System.IFormattable, ISiUnitValueQuantifiable<double, ElectricalResistanceUnit>
  {
    /// <summary>
    /// <para>The Von Klitzing constant, in (base) unit of ohm.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Quantum_Hall_effect"/></para>
    /// </summary>
    public const double VonKlitzingConstant = 25812.80745;

    private readonly double m_value;

    public ElectricalResistance(double value, ElectricalResistanceUnit unit = ElectricalResistanceUnit.Ohm) => m_value = ConvertFromUnit(unit, value);

    public ElectricalResistance(MetricPrefix prefix, double ohm) => m_value = prefix.ChangePrefix(ohm, MetricPrefix.Unprefixed);

    public ElectricalConductance ToElectricalConductance() => new(1 / m_value);

    #region Static methods
    /// <summary>Creates a new ElectricResistance instance from the specified voltage and current.</summary>
    /// <param name="voltage"></param>
    /// <param name="current"></param>
    public static ElectricalResistance From(ElectricPotential voltage, ElectricCurrent current) => new(voltage.Value / current.Value);

    /// <summary>Converts resistor values as if in parallel configuration.</summary>
    public static ElectricalResistance FromParallelResistors(params double[] resistors)
    {
      var sum = 0.0;
      foreach (var resistor in resistors)
        sum += 1 / resistor;
      return new(1 / sum);
    }

    /// <summary>Converts resistor values as if in serial configuration.</summary>
    public static ElectricalResistance FromSerialResistors(params double[] resistors)
    {
      var sum = 0.0;
      foreach (var resistor in resistors)
        sum += resistor;
      return new(sum);
    }
    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) < 0;
    public static bool operator >(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) > 0;
    public static bool operator <=(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) <= 0;
    public static bool operator >=(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) >= 0;

    public static ElectricalResistance operator -(ElectricalResistance v) => new(-v.m_value);
    public static ElectricalResistance operator *(ElectricalResistance a, ElectricalResistance b) => new(a.m_value * b.m_value);
    public static ElectricalResistance operator /(ElectricalResistance a, ElectricalResistance b) => new(a.m_value / b.m_value);
    public static ElectricalResistance operator %(ElectricalResistance a, ElectricalResistance b) => new(a.m_value % b.m_value);
    public static ElectricalResistance operator +(ElectricalResistance a, ElectricalResistance b) => new(a.m_value + b.m_value);
    public static ElectricalResistance operator -(ElectricalResistance a, ElectricalResistance b) => new(a.m_value - b.m_value);
    public static ElectricalResistance operator *(ElectricalResistance a, double b) => new(a.m_value * b);
    public static ElectricalResistance operator /(ElectricalResistance a, double b) => new(a.m_value / b);
    public static ElectricalResistance operator %(ElectricalResistance a, double b) => new(a.m_value % b);
    public static ElectricalResistance operator +(ElectricalResistance a, double b) => new(a.m_value + b);
    public static ElectricalResistance operator -(ElectricalResistance a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricalResistance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricalResistance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(ElectricalResistanceUnit.Ohm, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(ElectricalResistanceUnit.Ohm, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetSiUnitValue(prefix);

      return value.ToSiFormattedString(format, formatProvider)
        + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString()
        + (fullName ? GetSiUnitName(prefix, value.IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));
    }

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ElectricalResistanceUnit unit, double value)
      => unit switch
      {
        ElectricalResistanceUnit.Ohm => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(ElectricalResistanceUnit unit, double value)
      => unit switch
      {
        ElectricalResistanceUnit.Ohm => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, ElectricalResistanceUnit from, ElectricalResistanceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(ElectricalResistanceUnit unit)
      => unit switch
      {
        ElectricalResistanceUnit.Ohm => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(ElectricalResistanceUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(ElectricalResistanceUnit unit, bool preferUnicode)
      => unit switch
      {
        Units.ElectricalResistanceUnit.Ohm => preferUnicode ? "\u2126" : "ohm",
        //Quantities.ElectricalResistanceUnit.KiloOhm => preferUnicode ? "\u33C0" : "kiloohm",
        //Quantities.ElectricalResistanceUnit.MegaOhm => preferUnicode ? "\u33C1" : "megaohm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ElectricalResistanceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ElectricalResistanceUnit unit = ElectricalResistanceUnit.Ohm, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + Unicode.UnicodeSpacing.Space.ToSpacingString()
        + (fullName ? GetUnitName(unit, value.IsConsideredPlural()) : GetUnitSymbol(unit, false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="ElectricalResistance.Value"/> property is in <see cref="ElectricalResistanceUnit.Ohm"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
