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

    public Flow(MetricPrefix prefix, double cubicMeterPerSecond) => m_value = prefix.ConvertPrefix(cubicMeterPerSecond, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix, 3);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + FlowUnit.CubicMeterPerSecond.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(FlowUnit unit, double value)
      => unit switch
      {
        FlowUnit.CubicMeterPerSecond => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(FlowUnit unit, double value)
      => unit switch
      {
        FlowUnit.CubicMeterPerSecond => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, FlowUnit from, FlowUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(FlowUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(FlowUnit unit = FlowUnit.CubicMeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(Number.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

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
