namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.FlowUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.FlowUnit.CubicMeterPerSecond => "m�/s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum FlowUnit
    {
      /// <summary>This is the default unit for <see cref="Flow"/>.</summary>
      CubicMeterPerSecond,
    }

    /// <summary>Volumetric flow, unit of cubic meters per second, is the rate of change of volume with respect to time.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Flow"/>
    public readonly record struct Flow
      : System.IComparable, System.IComparable<Flow>, System.IFormattable, IUnitValueQuantifiable<double, FlowUnit>
    {
      private readonly double m_value;

      public Flow(double value, FlowUnit unit = FlowUnit.CubicMeterPerSecond)
        => m_value = unit switch
        {
          FlowUnit.CubicMeterPerSecond => value,
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
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(FlowUnit.CubicMeterPerSecond, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Flow.Value"/> property is in <see cref="FlowUnit.CubicMeterPerSecond"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(FlowUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(useFullName);

      public double GetUnitValue(FlowUnit unit)
        => unit switch
        {
          FlowUnit.CubicMeterPerSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(FlowUnit unit = FlowUnit.CubicMeterPerSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}