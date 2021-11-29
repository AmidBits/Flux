namespace Flux.Quantity
{
  public enum AngularVelocityUnit
  {
    RadianPerSecond,
  }

  /// <summary>Angular velocity, unit of radians per second. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_velocity"/>
#if NET5_0
  public struct AngularVelocity
    : System.IComparable<AngularVelocity>, System.IEquatable<AngularVelocity>, IValuedUnit<double>
#else
  public record struct AngularVelocity
    : System.IComparable<AngularVelocity>, IValuedUnit<double>
#endif
  {
    private readonly double m_value;

    public AngularVelocity(double value, AngularVelocityUnit unit = AngularVelocityUnit.RadianPerSecond)
      => m_value = unit switch
      {
        AngularVelocityUnit.RadianPerSecond => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(AngularVelocityUnit unit = AngularVelocityUnit.RadianPerSecond)
      => unit switch
      {
        AngularVelocityUnit.RadianPerSecond => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    public static AngularVelocity From(Angle angle, Time time)
      => new(angle.Value / time.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(AngularVelocity v)
      => v.m_value;
    public static explicit operator AngularVelocity(double v)
      => new(v);

    public static bool operator <(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) >= 0;

#if NET5_0
    public static bool operator ==(AngularVelocity a, AngularVelocity b)
      => a.Equals(b);
    public static bool operator !=(AngularVelocity a, AngularVelocity b)
      => !a.Equals(b);
#endif

    public static AngularVelocity operator -(AngularVelocity v)
      => new(-v.m_value);
    public static AngularVelocity operator +(AngularVelocity a, AngularVelocity b)
      => new(a.m_value + b.m_value);
    public static AngularVelocity operator /(AngularVelocity a, AngularVelocity b)
      => new(a.m_value / b.m_value);
    public static AngularVelocity operator *(AngularVelocity a, AngularVelocity b)
      => new(a.m_value * b.m_value);
    public static AngularVelocity operator %(AngularVelocity a, AngularVelocity b)
      => new(a.m_value % b.m_value);
    public static AngularVelocity operator -(AngularVelocity a, AngularVelocity b)
      => new(a.m_value - b.m_value);

    public static AngularVelocity operator +(AngularVelocity a, double b)
      => new(a.m_value + b);
    public static AngularVelocity operator /(AngularVelocity a, double b)
      => new(a.m_value / b);
    public static AngularVelocity operator *(AngularVelocity a, double b)
      => new(a.m_value * b);
    public static AngularVelocity operator %(AngularVelocity a, double b)
      => new(a.m_value % b);
    public static AngularVelocity operator -(AngularVelocity a, double b)
      => new(a.m_value - b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AngularVelocity other)
      => m_value.CompareTo(other.m_value);

#if NET5_0
    // IEquatable
    public bool Equals(AngularVelocity other)
      => m_value == other.m_value;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is AngularVelocity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} rad/s }}";
    #endregion Object overrides
  }
}
