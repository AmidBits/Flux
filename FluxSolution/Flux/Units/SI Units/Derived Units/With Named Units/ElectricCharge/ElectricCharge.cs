namespace Flux.Units
{
  /// <summary>
  /// <para>Electric charge, unit of Coulomb.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Electric_charge"/></para>
  /// </summary>
  public readonly record struct ElectricCharge
    : System.IComparable, System.IComparable<ElectricCharge>, System.IFormattable, ISiUnitValueQuantifiable<double, ElectricChargeUnit>
  {
    /// <summary>
    /// <para>The elementary charge, usually denoted by e, is a fundamental physical constant, defined as the electric charge carried by a single proton (+1 e) or, equivalently, the magnitude of the negative electric charge carried by a single electron, which has charge -1 e.</para>
    /// <para>This is one of the fundamental physical constants of physics.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Elementary_charge"/></para>
    /// </summary>
    public const double ElementaryCharge = 1.602176634e-19;

    private readonly double m_value;

    public ElectricCharge(double value, ElectricChargeUnit unit = ElectricChargeUnit.Coulomb) => m_value = ConvertFromUnit(unit, value);

    public ElectricCharge(MetricPrefix prefix, double coulomb) => m_value = prefix.ChangePrefix(coulomb, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) < 0;
    public static bool operator >(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) > 0;
    public static bool operator <=(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) <= 0;
    public static bool operator >=(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) >= 0;

    public static ElectricCharge operator -(ElectricCharge v) => new(-v.m_value);
    public static ElectricCharge operator *(ElectricCharge a, ElectricCharge b) => new(a.m_value * b.m_value);
    public static ElectricCharge operator /(ElectricCharge a, ElectricCharge b) => new(a.m_value / b.m_value);
    public static ElectricCharge operator %(ElectricCharge a, ElectricCharge b) => new(a.m_value % b.m_value);
    public static ElectricCharge operator +(ElectricCharge a, ElectricCharge b) => new(a.m_value + b.m_value);
    public static ElectricCharge operator -(ElectricCharge a, ElectricCharge b) => new(a.m_value - b.m_value);
    public static ElectricCharge operator *(ElectricCharge a, double b) => new(a.m_value * b);
    public static ElectricCharge operator /(ElectricCharge a, double b) => new(a.m_value / b);
    public static ElectricCharge operator %(ElectricCharge a, double b) => new(a.m_value % b);
    public static ElectricCharge operator +(ElectricCharge a, double b) => new(a.m_value + b);
    public static ElectricCharge operator -(ElectricCharge a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricCharge o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricCharge other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ChangePrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + ElectricChargeUnit.Coulomb.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ElectricChargeUnit unit, double value)
      => unit switch
      {
        ElectricChargeUnit.Coulomb => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(ElectricChargeUnit unit, double value)
      => unit switch
      {
        ElectricChargeUnit.Coulomb => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, ElectricChargeUnit from, ElectricChargeUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(ElectricChargeUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ElectricChargeUnit unit = ElectricChargeUnit.Coulomb, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + Unicode.UnicodeSpacing.Space.ToSpacingString()
        + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

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
