namespace Flux.Quantities
{
  public enum ElectricalConductanceUnit
  {
    /// <summary>This is the default unit for <see cref="CurrentDensity"/>. Siemens = (1/ohm).</summary>
    Siemens,
  }

  /// <summary>Electrical conductance, unit of Siemens.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Electrical_resistance_and_conductance"/>
  public readonly record struct ElectricalConductance
    : System.IComparable, System.IComparable<ElectricalConductance>, System.IFormattable, ISiPrefixValueQuantifiable<double, ElectricalConductanceUnit>
  {
    private readonly double m_value;

    public ElectricalConductance(double value, ElectricalConductanceUnit unit = ElectricalConductanceUnit.Siemens)
      => m_value = unit switch
      {
        ElectricalConductanceUnit.Siemens => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public ElectricalResistance ToElectricResistance() => new(1 / m_value);

    #region Static methods
    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) >= 0;

    public static ElectricalConductance operator -(ElectricalConductance v) => new(-v.m_value);
    public static ElectricalConductance operator +(ElectricalConductance a, double b) => new(a.m_value + b);
    public static ElectricalConductance operator +(ElectricalConductance a, ElectricalConductance b) => a + b.m_value;
    public static ElectricalConductance operator /(ElectricalConductance a, double b) => new(a.m_value / b);
    public static ElectricalConductance operator /(ElectricalConductance a, ElectricalConductance b) => a / b.m_value;
    public static ElectricalConductance operator *(ElectricalConductance a, double b) => new(a.m_value * b);
    public static ElectricalConductance operator *(ElectricalConductance a, ElectricalConductance b) => a * b.m_value;
    public static ElectricalConductance operator %(ElectricalConductance a, double b) => new(a.m_value % b);
    public static ElectricalConductance operator %(ElectricalConductance a, ElectricalConductance b) => a % b.m_value;
    public static ElectricalConductance operator -(ElectricalConductance a, double b) => new(a.m_value - b);
    public static ElectricalConductance operator -(ElectricalConductance a, ElectricalConductance b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricalConductance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricalConductance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixValueSymbolString(MetricPrefix.Unprefixed);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="ElectricalConductance.Value"/> property is in <see cref="ElectricalConductanceUnit.Siemens"/>.</para>
    /// </summary>
    public double Value => m_value;

    // ISiPrefixValueQuantifiable<>
    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(ElectricalConductanceUnit.Siemens, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(ElectricalConductanceUnit.Siemens, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixValueNameString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    // IUnitQuantifiable<>
    public string GetUnitName(ElectricalConductanceUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(ElectricalConductanceUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.ElectricalConductanceUnit.Siemens => preferUnicode ? "\u2127" : "S",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ElectricalConductanceUnit unit)
      => unit switch
      {
        ElectricalConductanceUnit.Siemens => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitValueNameString(ElectricalConductanceUnit unit = ElectricalConductanceUnit.Siemens, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(ElectricalConductanceUnit unit = ElectricalConductanceUnit.Siemens, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
