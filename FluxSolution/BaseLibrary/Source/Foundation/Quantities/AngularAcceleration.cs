namespace Flux.Quantity
{
  public enum AngularAccelerationUnit
  {
    RadianPerSecondSquare,
  }

  /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
#if NET5_0
  public struct AngularAcceleration
    : System.IComparable<AngularAcceleration>, System.IEquatable<AngularAcceleration>, IValuedUnit<double>
#else
  public record struct AngularAcceleration
    : System.IComparable<AngularAcceleration>, IValuedUnit<double>
#endif
  {
    private readonly double m_value;

    public AngularAcceleration(double value, AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquare)
      => m_value = unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquare => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquare)
      => unit switch
      {
        AngularAccelerationUnit.RadianPerSecondSquare => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(AngularAcceleration v)
      => v.m_value;
    public static explicit operator AngularAcceleration(double v)
      => new(v);

    public static bool operator <(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) >= 0;

#if NET5_0
    public static bool operator ==(AngularAcceleration a, AngularAcceleration b)
      => a.Equals(b);
    public static bool operator !=(AngularAcceleration a, AngularAcceleration b)
      => !a.Equals(b);
#endif

    public static AngularAcceleration operator -(AngularAcceleration v)
      => new(-v.m_value);
    public static AngularAcceleration operator +(AngularAcceleration a, double b)
      => new(a.m_value + b);
    public static AngularAcceleration operator +(AngularAcceleration a, AngularAcceleration b)
      => a + b.m_value;
    public static AngularAcceleration operator /(AngularAcceleration a, double b)
      => new(a.m_value / b);
    public static AngularAcceleration operator /(AngularAcceleration a, AngularAcceleration b)
      => a / b.m_value;
    public static AngularAcceleration operator *(AngularAcceleration a, double b)
      => new(a.m_value * b);
    public static AngularAcceleration operator *(AngularAcceleration a, AngularAcceleration b)
      => a * b.m_value;
    public static AngularAcceleration operator %(AngularAcceleration a, double b)
      => new(a.m_value % b);
    public static AngularAcceleration operator %(AngularAcceleration a, AngularAcceleration b)
      => a % b.m_value;
    public static AngularAcceleration operator -(AngularAcceleration a, double b)
      => new(a.m_value - b);
    public static AngularAcceleration operator -(AngularAcceleration a, AngularAcceleration b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AngularAcceleration other)
      => m_value.CompareTo(other.m_value);

#if NET5_0
    // IEquatable
    public bool Equals(AngularAcceleration other)
      => m_value == other.m_value;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is AngularAcceleration o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} rad/s² }}";
    #endregion Object overrides
  }
}
