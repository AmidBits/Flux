namespace Flux.Units
{
  /// <summary>
  /// <para>Electrical conductance, unit of Siemens.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Electrical_resistance_and_conductance"/></para>
  /// </summary>
  public readonly record struct ElectricalConductance
    : System.IComparable, System.IComparable<ElectricalConductance>, System.IFormattable, ISiUnitValueQuantifiable<double, ElectricalConductanceUnit>
  {
    private readonly double m_siemens;

    public ElectricalConductance(double value, ElectricalConductanceUnit unit = ElectricalConductanceUnit.Siemens) => m_siemens = ConvertFromUnit(unit, value);

    public ElectricalConductance(MetricPrefix prefix, double siemens) => m_siemens = prefix.ConvertPrefix(siemens, MetricPrefix.Unprefixed);

    public ElectricalResistance ToElectricResistance() => new(1 / m_siemens);

    #region Static methods
    #endregion Static methods

    #region Overloaded operators

    public static explicit operator ElectricalConductance(double value) => new(value);
    public static implicit operator double(ElectricalConductance value) => value.m_siemens;

    public static bool operator <(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) < 0;
    public static bool operator >(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) > 0;
    public static bool operator <=(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) <= 0;
    public static bool operator >=(ElectricalConductance a, ElectricalConductance b) => a.CompareTo(b) >= 0;

    public static ElectricalConductance operator -(ElectricalConductance v) => new(-v.m_siemens);
    public static ElectricalConductance operator *(ElectricalConductance a, ElectricalConductance b) => new(a.m_siemens * b.m_siemens);
    public static ElectricalConductance operator /(ElectricalConductance a, ElectricalConductance b) => new(a.m_siemens / b.m_siemens);
    public static ElectricalConductance operator %(ElectricalConductance a, ElectricalConductance b) => new(a.m_siemens % b.m_siemens);
    public static ElectricalConductance operator +(ElectricalConductance a, ElectricalConductance b) => new(a.m_siemens + b.m_siemens);
    public static ElectricalConductance operator -(ElectricalConductance a, ElectricalConductance b) => new(a.m_siemens - b.m_siemens);
    public static ElectricalConductance operator *(ElectricalConductance a, double b) => new(a.m_siemens * b);
    public static ElectricalConductance operator /(ElectricalConductance a, double b) => new(a.m_siemens / b);
    public static ElectricalConductance operator %(ElectricalConductance a, double b) => new(a.m_siemens % b);
    public static ElectricalConductance operator +(ElectricalConductance a, double b) => new(a.m_siemens + b);
    public static ElectricalConductance operator -(ElectricalConductance a, double b) => new(a.m_siemens - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricalConductance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricalConductance other) => m_siemens.CompareTo(other.m_siemens);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiPrefixValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_siemens, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + ElectricalConductanceUnit.Siemens.GetUnitSymbol();

    #endregion // ISiPrefixValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ElectricalConductanceUnit unit, double value)
      => unit switch
      {
        ElectricalConductanceUnit.Siemens => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(ElectricalConductanceUnit unit, double value)
      => unit switch
      {
        ElectricalConductanceUnit.Siemens => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, ElectricalConductanceUnit from, ElectricalConductanceUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(ElectricalConductanceUnit unit) => ConvertToUnit(unit, m_siemens);

    public string ToUnitString(ElectricalConductanceUnit unit = ElectricalConductanceUnit.Siemens, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(Number.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="ElectricalConductance.Value"/> property is in <see cref="ElectricalConductanceUnit.Siemens"/>.</para>
    /// </summary>
    public double Value => m_siemens;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
