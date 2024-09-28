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

    public ElectricalResistance(double value, ElectricalResistanceUnit unit = ElectricalResistanceUnit.Ohm)
      => m_value = unit switch
      {
        ElectricalResistanceUnit.Ohm => value,
        ElectricalResistanceUnit.KiloOhm => value * 1000,
        ElectricalResistanceUnit.MegaOhm => value * 1000000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

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

    // ISiUnitValueQuantifiable<>
    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(ElectricalResistanceUnit.Ohm, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(ElectricalResistanceUnit.Ohm, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    //public string ToSiPrefixValueNameString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
    //  => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    //public string ToSiPrefixValueSymbolString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="ElectricalResistance.Value"/> property is in <see cref="ElectricalResistanceUnit.Ohm"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(ElectricalResistanceUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(ElectricalResistanceUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.ElectricalResistanceUnit.Ohm => preferUnicode ? "\u2126" : "ohm",
        Quantities.ElectricalResistanceUnit.KiloOhm => preferUnicode ? "\u33C0" : "kiloohm",
        Quantities.ElectricalResistanceUnit.MegaOhm => preferUnicode ? "\u33C1" : "megaohm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ElectricalResistanceUnit unit)
      => unit switch
      {
        ElectricalResistanceUnit.Ohm => m_value,
        ElectricalResistanceUnit.KiloOhm => m_value / 1000,
        ElectricalResistanceUnit.MegaOhm => m_value / 1000000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitString(ElectricalResistanceUnit unit = ElectricalResistanceUnit.Ohm, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    //public string ToUnitValueNameString(ElectricalResistanceUnit unit = ElectricalResistanceUnit.Ohm, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    //public string ToUnitValueSymbolString(ElectricalResistanceUnit unit = ElectricalResistanceUnit.Ohm, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
