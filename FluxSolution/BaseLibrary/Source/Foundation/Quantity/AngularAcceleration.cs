namespace Flux.Quantity
{
  /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
  public struct AngularAcceleration
    : System.IComparable<AngularAcceleration>, System.IEquatable<AngularAcceleration>, IValuedSiDerivedUnit
  {
    private readonly double m_value;

    public AngularAcceleration(double radianPerSecondSquare)
      => m_value = radianPerSecondSquare;

    public double Value
      => m_value;

    #region Overloaded operators
    public static explicit operator double(AngularAcceleration v)
      => v.m_value;
    public static explicit operator AngularAcceleration(double v)
      => new AngularAcceleration(v);

    public static bool operator <(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(AngularAcceleration a, AngularAcceleration b)
      => a.Equals(b);
    public static bool operator !=(AngularAcceleration a, AngularAcceleration b)
      => !a.Equals(b);

    public static AngularAcceleration operator -(AngularAcceleration v)
      => new AngularAcceleration(-v.m_value);
    public static AngularAcceleration operator +(AngularAcceleration a, double b)
      => new AngularAcceleration(a.m_value + b);
    public static AngularAcceleration operator +(AngularAcceleration a, AngularAcceleration b)
      => a + b.m_value;
    public static AngularAcceleration operator /(AngularAcceleration a, double b)
      => new AngularAcceleration(a.m_value / b);
    public static AngularAcceleration operator /(AngularAcceleration a, AngularAcceleration b)
      => a / b.m_value;
    public static AngularAcceleration operator *(AngularAcceleration a, double b)
      => new AngularAcceleration(a.m_value * b);
    public static AngularAcceleration operator *(AngularAcceleration a, AngularAcceleration b)
      => a * b.m_value;
    public static AngularAcceleration operator %(AngularAcceleration a, double b)
      => new AngularAcceleration(a.m_value % b);
    public static AngularAcceleration operator %(AngularAcceleration a, AngularAcceleration b)
      => a % b.m_value;
    public static AngularAcceleration operator -(AngularAcceleration a, double b)
      => new AngularAcceleration(a.m_value - b);
    public static AngularAcceleration operator -(AngularAcceleration a, AngularAcceleration b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AngularAcceleration other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(AngularAcceleration other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AngularAcceleration o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} rad/s²>";
    #endregion Object overrides
  }
}
