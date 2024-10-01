namespace Flux.Quantities
{
  public enum FlowUnit
  {
    /// <summary>This is the default unit for <see cref="Flow"/>.</summary>
    CubicMeterPerSecond,
    /// <summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sverdrup"/>
    /// </summary>
    Sverdrup,
    USGallonPerMinute,
  }

  /// <summary>Volumetric flow, unit of cubic meters per second, is the rate of change of volume with respect to time.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Volumetric_flow_rate"/>
  public readonly record struct Flow
    : System.IComparable, System.IComparable<Flow>, System.IFormattable, IUnitValueQuantifiable<double, FlowUnit>
  {
    private readonly double m_value;

    public Flow(double value, FlowUnit unit = FlowUnit.CubicMeterPerSecond) => m_value = ConvertFromUnit(unit, value);

    public Flow(Volume volume, Time time) : this(volume.Value / time.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Flow a, Flow b) => a.CompareTo(b) < 0;
    public static bool operator <=(Flow a, Flow b) => a.CompareTo(b) <= 0;
    public static bool operator >(Flow a, Flow b) => a.CompareTo(b) > 0;
    public static bool operator >=(Flow a, Flow b) => a.CompareTo(b) >= 0;

    public static Flow operator -(Flow v) => new(-v.m_value);
    public static Flow operator +(Flow a, double b) => new(a.m_value + b);
    public static Flow operator +(Flow a, Flow b) => a + b.m_value;
    public static Flow operator /(Flow a, double b) => new(a.m_value / b);
    public static Flow operator /(Flow a, Flow b) => a / b.m_value;
    public static Flow operator *(Flow a, double b) => new(a.m_value * b);
    public static Flow operator *(Flow a, Flow b) => a * b.m_value;
    public static Flow operator %(Flow a, double b) => new(a.m_value % b);
    public static Flow operator %(Flow a, Flow b) => a % b.m_value;
    public static Flow operator -(Flow a, double b) => new(a.m_value - b);
    public static Flow operator -(Flow a, Flow b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Flow o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Flow other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(FlowUnit.CubicMeterPerSecond, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Flow.Value"/> property is in <see cref="FlowUnit.CubicMeterPerSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region IUnitQuantifiable<>

    private static double ConvertFromUnit(FlowUnit unit, double value)
      => unit switch
      {
        FlowUnit.CubicMeterPerSecond => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(FlowUnit unit, double value)
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

    public string GetUnitName(FlowUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(FlowUnit unit, bool preferUnicode)
      => unit switch
      {
        FlowUnit.CubicMeterPerSecond => "m³/s",
        FlowUnit.USGallonPerMinute => "gpm (US)",
        FlowUnit.Sverdrup => "Sv",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(FlowUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(FlowUnit unit = FlowUnit.CubicMeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
