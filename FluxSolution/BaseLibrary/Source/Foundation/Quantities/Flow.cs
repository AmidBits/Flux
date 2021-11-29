namespace Flux.Quantity
{
  public enum FlowUnit
  {
    CubicMetersPerSecond,
  }

  /// <summary>Volumetric flow, unit of cubic meters per second.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Flow"/>
#if NET5_0
  public struct Flow
    : System.IComparable<Flow>, System.IEquatable<Flow>, IValuedUnit<double>
#else
  public record struct Flow
    : System.IComparable<Flow>, IValuedUnit<double>
#endif
  {
    private readonly double m_value;

    public Flow(double value, FlowUnit unit = FlowUnit.CubicMetersPerSecond)
      => m_value = unit switch
      {
        FlowUnit.CubicMetersPerSecond => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(FlowUnit unit = FlowUnit.CubicMetersPerSecond)
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

#if NET5_0
    public static bool operator ==(Flow a, Flow b)
      => a.Equals(b);
    public static bool operator !=(Flow a, Flow b)
      => !a.Equals(b);
#endif

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

#if NET5_0
    // IEquatable
    public bool Equals(Flow other)
      => m_value == other.m_value;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is Flow o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
#endif
    public override string ToString()
      => $"{GetType().Name} {{ {m_value} m³/s }}";
    #endregion Object overrides
  }
}
