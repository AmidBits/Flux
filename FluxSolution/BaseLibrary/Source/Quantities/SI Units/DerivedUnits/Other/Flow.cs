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

    public Flow(double value, FlowUnit unit = FlowUnit.CubicMeterPerSecond)
      => m_value = unit switch
      {
        FlowUnit.CubicMeterPerSecond => value,
        FlowUnit.Sverdrup => 1000000 * value,
        FlowUnit.USGallonPerMinute => value / 15850.323074494,

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

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

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="Flow.Value"/> property is in <see cref="FlowUnit.CubicMeterPerSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
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

    public double GetUnitValue(FlowUnit unit)
      => unit switch
      {
        FlowUnit.CubicMeterPerSecond => m_value,
        FlowUnit.USGallonPerMinute => m_value * 15850.323074494,
        FlowUnit.Sverdrup => m_value / 1000000,

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitString(FlowUnit unit = FlowUnit.CubicMeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    //public string ToUnitValueNameString(FlowUnit unit = FlowUnit.CubicMeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    //public string ToUnitValueSymbolString(FlowUnit unit = FlowUnit.CubicMeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
