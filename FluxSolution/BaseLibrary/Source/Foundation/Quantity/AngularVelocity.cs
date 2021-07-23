namespace Flux.Quantity
{
  /// <summary>Angular velocity, unit of radians per second. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_velocity"/>
  public struct AngularVelocity
    : System.IComparable<AngularVelocity>, System.IEquatable<AngularVelocity>, IValuedSiDerivedUnit
  {
    private readonly double m_value;

    public AngularVelocity(double radianPerSecond)
      => m_value = radianPerSecond;

    public double Value
      => m_value;

    #region Static methods
    public static AngularVelocity From(Angle angle, Time time)
      => new AngularVelocity(angle.Value / time.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(AngularVelocity v)
      => v.m_value;
    public static explicit operator AngularVelocity(double v)
      => new AngularVelocity(v);

    public static bool operator <(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(AngularVelocity a, AngularVelocity b)
      => a.Equals(b);
    public static bool operator !=(AngularVelocity a, AngularVelocity b)
      => !a.Equals(b);

    public static AngularVelocity operator -(AngularVelocity v)
      => new AngularVelocity(-v.m_value);
    public static AngularVelocity operator +(AngularVelocity a, AngularVelocity b)
      => new AngularVelocity(a.m_value + b.m_value);
    public static AngularVelocity operator /(AngularVelocity a, AngularVelocity b)
      => new AngularVelocity(a.m_value / b.m_value);
    public static AngularVelocity operator *(AngularVelocity a, AngularVelocity b)
      => new AngularVelocity(a.m_value * b.m_value);
    public static AngularVelocity operator %(AngularVelocity a, AngularVelocity b)
      => new AngularVelocity(a.m_value % b.m_value);
    public static AngularVelocity operator -(AngularVelocity a, AngularVelocity b)
      => new AngularVelocity(a.m_value - b.m_value);

    public static AngularVelocity operator +(AngularVelocity a, double b)
      => new AngularVelocity(a.m_value + b);
    public static AngularVelocity operator /(AngularVelocity a, double b)
      => new AngularVelocity(a.m_value / b);
    public static AngularVelocity operator *(AngularVelocity a, double b)
      => new AngularVelocity(a.m_value * b);
    public static AngularVelocity operator %(AngularVelocity a, double b)
      => new AngularVelocity(a.m_value % b);
    public static AngularVelocity operator -(AngularVelocity a, double b)
      => new AngularVelocity(a.m_value - b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AngularVelocity other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(AngularVelocity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AngularVelocity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} rad/s>";
    #endregion Object overrides
  }
}
