namespace Flux.Units
{
  /// <summary>
  /// <para>Volumetric flow, unit of cubic meters per second, is the rate of change of volume with respect to time.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Volumetric_flow_rate"/></para>
  /// </summary>
  public readonly record struct Flow
    : System.IComparable, System.IComparable<Flow>, System.IFormattable, ISiUnitValueQuantifiable<double, FlowUnit>
  {
    private readonly double m_value;

    public Flow(double value, FlowUnit unit = FlowUnit.CubicMeterPerSecond) => m_value = ConvertFromUnit(unit, value);

    public Flow(MetricPrefix prefix, double cubicMeterPerSecond) => m_value = prefix.ConvertTo(cubicMeterPerSecond, MetricPrefix.Unprefixed);

    public Flow(Volume volume, Time time) : this(volume.Value / time.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Flow a, Flow b) => a.CompareTo(b) < 0;
    public static bool operator >(Flow a, Flow b) => a.CompareTo(b) > 0;
    public static bool operator <=(Flow a, Flow b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Flow a, Flow b) => a.CompareTo(b) >= 0;

    public static Acceleration operator -(Flow v) => new(-v.m_value);
    public static Flow operator *(Flow a, Flow b) => new(a.m_value * b.m_value);
    public static Flow operator /(Flow a, Flow b) => new(a.m_value / b.m_value);
    public static Flow operator %(Flow a, Flow b) => new(a.m_value % b.m_value);
    public static Flow operator +(Flow a, Flow b) => new(a.m_value + b.m_value);
    public static Flow operator -(Flow a, Flow b) => new(a.m_value - b.m_value);
    public static Flow operator *(Flow a, double b) => new(a.m_value * b);
    public static Flow operator /(Flow a, double b) => new(a.m_value / b);
    public static Flow operator %(Flow a, double b) => new(a.m_value % b);
    public static Flow operator +(Flow a, double b) => new(a.m_value + b);
    public static Flow operator -(Flow a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Flow o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Flow other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(FlowUnit.CubicMeterPerSecond, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => GetUnitName(FlowUnit.CubicMeterPerSecond, preferPlural).Insert(5, prefix.GetMetricPrefixName());

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(FlowUnit.CubicMeterPerSecond, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix, 3);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(FlowUnit unit, double value)
      => unit switch
      {
        FlowUnit.CubicMeterPerSecond => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(FlowUnit unit, double value)
      => unit switch
      {
        FlowUnit.CubicMeterPerSecond => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, FlowUnit from, FlowUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(FlowUnit unit)
      => unit switch
      {
        FlowUnit.CubicMeterPerSecond => 1,

        FlowUnit.USGallonPerMinute => 15850.323074494,
        FlowUnit.Sverdrup => 1000000,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(FlowUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(FlowUnit unit, bool preferUnicode)
      => unit switch
      {
        FlowUnit.CubicMeterPerSecond => "m�/s",

        FlowUnit.USGallonPerMinute => "gpm (US)",
        FlowUnit.Sverdrup => "Sv",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(FlowUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(FlowUnit unit = FlowUnit.CubicMeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Flow.Value"/> property is in <see cref="FlowUnit.CubicMeterPerSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
