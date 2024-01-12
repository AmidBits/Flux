namespace Flux
{
  public static partial class Em
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.FlowUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Units.FlowUnit.CubicMeterPerSecond => "m³/s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum FlowUnit
    {
      CubicMeterPerSecond,
    }

    /// <summary>Volumetric flow, unit of cubic meters per second, is the rate of change of volume with respect to time.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Flow"/>
    public readonly record struct Flow
      : System.IComparable, System.IComparable<Flow>, System.IFormattable, IUnitValueQuantifiable<double, FlowUnit>
    {
      public const FlowUnit DefaultUnit = FlowUnit.CubicMeterPerSecond;

      private readonly double m_value;

      public Flow(double value, FlowUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          FlowUnit.CubicMeterPerSecond => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static Flow From(Volume volume, Time time)
        => new(volume.Value / time.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Flow v) => v.m_value;
      public static explicit operator Flow(double v) => new(v);

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
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(FlowUnit unit)
        => unit switch
        {
          FlowUnit.CubicMeterPerSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(FlowUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
