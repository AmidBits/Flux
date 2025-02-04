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
    : System.IComparable, System.IComparable<ElectricalConductance>, System.IFormattable, ISiUnitValueQuantifiable<double, ElectricalConductanceUnit>
  {
    private readonly double m_value;

    public ElectricalConductance(double value, ElectricalConductanceUnit unit = ElectricalConductanceUnit.Siemens) => m_value = ConvertFromUnit(unit, value);

    public ElectricalConductance(MetricPrefix prefix, double siemens) => m_value = prefix.ConvertTo(siemens, MetricPrefix.Unprefixed);

    public ElectricalResistance ToElectricResistance() => new(1 / m_value);

    #region Static methods
    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) < 0;
    public static bool operator >(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) > 0;
    public static bool operator <=(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) <= 0;
    public static bool operator >=(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) >= 0;

    public static ElectricalConductance operator -(ElectricalConductance v) => new(-v.m_value);
    public static ElectricalConductance operator *(ElectricalConductance a, ElectricalConductance b) => new(a.m_value * b.m_value);
    public static ElectricalConductance operator /(ElectricalConductance a, ElectricalConductance b) => new(a.m_value / b.m_value);
    public static ElectricalConductance operator %(ElectricalConductance a, ElectricalConductance b) => new(a.m_value % b.m_value);
    public static ElectricalConductance operator +(ElectricalConductance a, ElectricalConductance b) => new(a.m_value + b.m_value);
    public static ElectricalConductance operator -(ElectricalConductance a, ElectricalConductance b) => new(a.m_value - b.m_value);
    public static ElectricalConductance operator *(ElectricalConductance a, double b) => new(a.m_value * b);
    public static ElectricalConductance operator /(ElectricalConductance a, double b) => new(a.m_value / b);
    public static ElectricalConductance operator %(ElectricalConductance a, double b) => new(a.m_value % b);
    public static ElectricalConductance operator +(ElectricalConductance a, double b) => new(a.m_value + b);
    public static ElectricalConductance operator -(ElectricalConductance a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricalConductance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricalConductance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiPrefixValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(ElectricalConductanceUnit.Siemens, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(ElectricalConductanceUnit.Siemens, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ElectricalConductanceUnit unit, double value)
      => unit switch
      {
        ElectricalConductanceUnit.Siemens => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(ElectricalConductanceUnit unit, double value)
      => unit switch
      {
        ElectricalConductanceUnit.Siemens => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, ElectricalConductanceUnit from, ElectricalConductanceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(ElectricalConductanceUnit unit)
      => unit switch
      {
        ElectricalConductanceUnit.Siemens => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(ElectricalConductanceUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(ElectricalConductanceUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.ElectricalConductanceUnit.Siemens => preferUnicode ? "\u2127" : "S",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ElectricalConductanceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ElectricalConductanceUnit unit = ElectricalConductanceUnit.Siemens, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="ElectricalConductance.Value"/> property is in <see cref="ElectricalConductanceUnit.Siemens"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
