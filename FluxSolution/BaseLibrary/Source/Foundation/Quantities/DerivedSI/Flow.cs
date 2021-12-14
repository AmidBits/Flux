namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Flow Create(this FlowUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this FlowUnit unit)
      => unit switch
      {
        FlowUnit.CubicMetersPerSecond => @" m³/s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum FlowUnit
  {
    CubicMetersPerSecond,
  }

  /// <summary>Volumetric flow, unit of cubic meters per second.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Flow"/>
  public struct Flow
    : System.IComparable<Flow>, System.IEquatable<Flow>, IValueGeneralizedUnit<double>, IValueSiDerivedUnit<double>
  {
    public const FlowUnit DefaultUnit = FlowUnit.CubicMetersPerSecond;

    private readonly double m_value;

    public Flow(double value, FlowUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        FlowUnit.CubicMetersPerSecond => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(FlowUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(FlowUnit unit = DefaultUnit)
      => unit switch
      {
        FlowUnit.CubicMetersPerSecond => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    public static Flow From(Volume volume, Time time)
      => new(volume.Value / time.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Flow v)
      => v.m_value;
    public static explicit operator Flow(double v)
      => new(v);

    public static bool operator <(Flow a, Flow b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Flow a, Flow b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Flow a, Flow b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Flow a, Flow b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Flow a, Flow b)
      => a.Equals(b);
    public static bool operator !=(Flow a, Flow b)
      => !a.Equals(b);

    public static Flow operator -(Flow v)
      => new(-v.m_value);
    public static Flow operator +(Flow a, double b)
      => new(a.m_value + b);
    public static Flow operator +(Flow a, Flow b)
      => a + b.m_value;
    public static Flow operator /(Flow a, double b)
      => new(a.m_value / b);
    public static Flow operator /(Flow a, Flow b)
      => a / b.m_value;
    public static Flow operator *(Flow a, double b)
      => new(a.m_value * b);
    public static Flow operator *(Flow a, Flow b)
      => a * b.m_value;
    public static Flow operator %(Flow a, double b)
      => new(a.m_value % b);
    public static Flow operator %(Flow a, Flow b)
      => a % b.m_value;
    public static Flow operator -(Flow a, double b)
      => new(a.m_value - b);
    public static Flow operator -(Flow a, Flow b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Flow other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Flow other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Flow o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ {ToUnitString()} }}";
    #endregion Object overrides
  }
}
