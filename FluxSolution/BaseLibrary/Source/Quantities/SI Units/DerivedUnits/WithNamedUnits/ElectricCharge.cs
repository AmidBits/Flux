namespace Flux.Quantities
{
  public enum ElectricChargeUnit
  {
    /// <summary>This is the default unit for <see cref="ElectricCharge"/>.</summary>
    Coulomb,
  }

  /// <summary>Electric charge, unit of Coulomb.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Electric_charge"/>
  public readonly record struct ElectricCharge
    : System.IComparable, System.IComparable<ElectricCharge>, System.IFormattable, ISiUnitValueQuantifiable<double, ElectricChargeUnit>
  {
    public static ElectricCharge ElementaryCharge => new(1.602176634e-19);

    private readonly double m_value;

    public ElectricCharge(double value, ElectricChargeUnit unit = ElectricChargeUnit.Coulomb) => m_value = ConvertFromUnit(unit, value);

    public ElectricCharge(MetricPrefix prefix, double coulomb) => m_value = prefix.ConvertTo(coulomb, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) >= 0;

    public static ElectricCharge operator -(ElectricCharge v) => new(-v.m_value);
    public static ElectricCharge operator +(ElectricCharge a, double b) => new(a.m_value + b);
    public static ElectricCharge operator +(ElectricCharge a, ElectricCharge b) => a + b.m_value;
    public static ElectricCharge operator /(ElectricCharge a, double b) => new(a.m_value / b);
    public static ElectricCharge operator /(ElectricCharge a, ElectricCharge b) => a / b.m_value;
    public static ElectricCharge operator *(ElectricCharge a, double b) => new(a.m_value * b);
    public static ElectricCharge operator *(ElectricCharge a, ElectricCharge b) => a * b.m_value;
    public static ElectricCharge operator %(ElectricCharge a, double b) => new(a.m_value % b);
    public static ElectricCharge operator %(ElectricCharge a, ElectricCharge b) => a % b.m_value;
    public static ElectricCharge operator -(ElectricCharge a, double b) => new(a.m_value - b);
    public static ElectricCharge operator -(ElectricCharge a, ElectricCharge b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricCharge o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricCharge other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(ElectricChargeUnit.Coulomb, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(ElectricChargeUnit.Coulomb, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(ElectricChargeUnit unit, double value)
      => unit switch
      {
        ElectricChargeUnit.Coulomb => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(ElectricChargeUnit unit, double value)
      => unit switch
      {
        ElectricChargeUnit.Coulomb => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, ElectricChargeUnit from, ElectricChargeUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(ElectricChargeUnit unit)
      => unit switch
      {
        ElectricChargeUnit.Coulomb => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(ElectricChargeUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(ElectricChargeUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.ElectricChargeUnit.Coulomb => "C",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ElectricChargeUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ElectricChargeUnit unit = ElectricChargeUnit.Coulomb, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="ElectricCharge.Value"/> property is in <see cref="ElectricChargeUnit.Ohm"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
