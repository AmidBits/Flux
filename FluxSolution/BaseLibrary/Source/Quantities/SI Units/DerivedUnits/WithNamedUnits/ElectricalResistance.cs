namespace Flux.Quantities
{
  public enum ElectricalResistanceUnit
  {
    /// <summary>This is the default unit for <see cref="ElectricalResistance"/>.</summary>
    Ohm,
    KiloOhm,
    MegaOhm,
  }

  /// <summary>Electric resistance, unit of Ohm.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Electrical_resistance_and_conductance"/>
  public readonly record struct ElectricalResistance
    : System.IComparable, System.IComparable<ElectricalResistance>, System.IFormattable, ISiPrefixValueQuantifiable<double, ElectricalResistanceUnit>
  {
    public static ElectricalResistance VonKlitzingConstant => new(25812.80745); // 25812.80745;

    private readonly double m_value;

    public ElectricalResistance(double value, ElectricalResistanceUnit unit = ElectricalResistanceUnit.Ohm) => m_value = ConvertFromUnit(unit, value);

    public ElectricalConductance ToElectricalConductance() => new(1 / m_value);

    #region Static methods
    /// <summary>Creates a new ElectricResistance instance from the specified voltage and current.</summary>
    /// <param name="voltage"></param>
    /// <param name="current"></param>
    public static ElectricalResistance From(Voltage voltage, ElectricCurrent current)
      => new(voltage.Value / current.Value);

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
    public static bool operator <=(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) >= 0;

    public static ElectricalResistance operator -(ElectricalResistance v) => new(-v.m_value);
    public static ElectricalResistance operator +(ElectricalResistance a, double b) => new(a.m_value + b);
    public static ElectricalResistance operator +(ElectricalResistance a, ElectricalResistance b) => a + b.m_value;
    public static ElectricalResistance operator /(ElectricalResistance a, double b) => new(a.m_value / b);
    public static ElectricalResistance operator /(ElectricalResistance a, ElectricalResistance b) => a / b.m_value;
    public static ElectricalResistance operator *(ElectricalResistance a, double b) => new(a.m_value * b);
    public static ElectricalResistance operator *(ElectricalResistance a, ElectricalResistance b) => a * b.m_value;
    public static ElectricalResistance operator %(ElectricalResistance a, double b) => new(a.m_value % b);
    public static ElectricalResistance operator %(ElectricalResistance a, ElectricalResistance b) => a % b.m_value;
    public static ElectricalResistance operator -(ElectricalResistance a, double b) => new(a.m_value - b);
    public static ElectricalResistance operator -(ElectricalResistance a, ElectricalResistance b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricalResistance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricalResistance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixString(MetricPrefix.Unprefixed);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="ElectricalResistance.Value"/> property is in <see cref="ElectricalResistanceUnit.Ohm"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(ElectricalResistanceUnit.Ohm, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(ElectricalResistanceUnit.Ohm, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

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

    public static double GetUnitFactor(ElectricalResistanceUnit unit)
      => unit switch
      {
        ElectricalResistanceUnit.Ohm => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(ElectricalResistanceUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(ElectricalResistanceUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.ElectricalResistanceUnit.Ohm => preferUnicode ? "\u2126" : "ohm",
        //Quantities.ElectricalResistanceUnit.KiloOhm => preferUnicode ? "\u33C0" : "kiloohm",
        //Quantities.ElectricalResistanceUnit.MegaOhm => preferUnicode ? "\u33C1" : "megaohm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ElectricalResistanceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ElectricalResistanceUnit unit = ElectricalResistanceUnit.Ohm, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
