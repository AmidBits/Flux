namespace Flux.Units
{
  /// <summary>
  /// <para>Energy, unit of Joule.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Energy"/></para>
  /// </summary>
  public readonly record struct Energy
    : System.IComparable, System.IComparable<Energy>, System.IFormattable, ISiUnitValueQuantifiable<double, EnergyUnit>
  {
    private readonly double m_value;

    public Energy(double value, EnergyUnit unit = EnergyUnit.Joule) => m_value = ConvertFromUnit(unit, value);

    public Energy(MetricPrefix prefix, double joule) => m_value = prefix.ConvertPrefix(joule, MetricPrefix.Unprefixed);

    public Energy(Pressure pressure, Volume volumn) => m_value = pressure.Value * volumn.Value;

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Energy a, Energy b) => a.CompareTo(b) < 0;
    public static bool operator >(Energy a, Energy b) => a.CompareTo(b) > 0;
    public static bool operator <=(Energy a, Energy b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Energy a, Energy b) => a.CompareTo(b) >= 0;

    public static Energy operator -(Energy v) => new(-v.m_value);
    public static Energy operator *(Energy a, Energy b) => new(a.m_value * b.m_value);
    public static Energy operator /(Energy a, Energy b) => new(a.m_value / b.m_value);
    public static Energy operator %(Energy a, Energy b) => new(a.m_value % b.m_value);
    public static Energy operator +(Energy a, Energy b) => new(a.m_value + b.m_value);
    public static Energy operator -(Energy a, Energy b) => new(a.m_value - b.m_value);
    public static Energy operator *(Energy a, double b) => new(a.m_value * b);
    public static Energy operator /(Energy a, double b) => new(a.m_value / b);
    public static Energy operator %(Energy a, double b) => new(a.m_value % b);
    public static Energy operator +(Energy a, double b) => new(a.m_value + b);
    public static Energy operator -(Energy a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Energy o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Energy other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + EnergyUnit.Joule.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(EnergyUnit unit, double value)
      => unit switch
      {
        EnergyUnit.Joule => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(EnergyUnit unit, double value)
      => unit switch
      {
        EnergyUnit.Joule => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, EnergyUnit from, EnergyUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(EnergyUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(EnergyUnit unit = EnergyUnit.Joule, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider)
        + spacing.ToSpacingString()
        + (fullName ? unit.GetUnitName(Number.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Energy.Value"/> property is in <see cref="EnergyUnit.Joule"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
