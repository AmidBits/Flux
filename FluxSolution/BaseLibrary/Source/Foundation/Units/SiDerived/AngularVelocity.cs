namespace Flux.Units
{
  /// <summary>Angular velocity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_velocity"/>
  public struct AngularVelocity
    : System.IComparable<AngularVelocity>, System.IEquatable<AngularVelocity>, IStandardizedScalar
  {
    private readonly double m_radianPerSecond;

    public AngularVelocity(double radianPerSecond)
      => m_radianPerSecond = radianPerSecond;

    public double RadianPerSecond
      => m_radianPerSecond;

    public (Angle angle, Time time) ToParts(AngleUnit angleUnit, TimeUnit timeUnit)
      => (Angle.FromUnitValue(AngleUnit.Radian, m_radianPerSecond), Time.FromUnitValue(TimeUnit.Second, 1));

    #region Static methods
    /// <summary>Creates a new AngularVelocity instance from the specified angle and time.</summary>
    /// <param name="angle"></param>
    /// <param name="time"></param>
    public AngularVelocity From(Angle angle, Time time)
      => new AngularVelocity(angle.Radian / time.Second);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(AngularVelocity v)
      => v.m_radianPerSecond;
    public static explicit operator AngularVelocity(double v)
      => new AngularVelocity(v);

    public static bool operator <(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(AngularVelocity a, AngularVelocity b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(AngularVelocity a, AngularVelocity b)
      => a.Equals(b);
    public static bool operator !=(AngularVelocity a, AngularVelocity b)
      => !a.Equals(b);

    public static AngularVelocity operator -(AngularVelocity v)
      => new AngularVelocity(-v.m_radianPerSecond);
    public static AngularVelocity operator +(AngularVelocity a, AngularVelocity b)
      => new AngularVelocity(a.m_radianPerSecond + b.m_radianPerSecond);
    public static AngularVelocity operator /(AngularVelocity a, AngularVelocity b)
      => new AngularVelocity(a.m_radianPerSecond / b.m_radianPerSecond);
    public static AngularVelocity operator *(AngularVelocity a, AngularVelocity b)
      => new AngularVelocity(a.m_radianPerSecond * b.m_radianPerSecond);
    public static AngularVelocity operator %(AngularVelocity a, AngularVelocity b)
      => new AngularVelocity(a.m_radianPerSecond % b.m_radianPerSecond);
    public static AngularVelocity operator -(AngularVelocity a, AngularVelocity b)
      => new AngularVelocity(a.m_radianPerSecond - b.m_radianPerSecond);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AngularVelocity other)
      => m_radianPerSecond.CompareTo(other.m_radianPerSecond);

    // IEquatable
    public bool Equals(AngularVelocity other)
      => m_radianPerSecond == other.m_radianPerSecond;

    // IUnitStandardized
    public double GetScalar()
      => m_radianPerSecond;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AngularVelocity o && Equals(o);
    public override int GetHashCode()
      => m_radianPerSecond.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_radianPerSecond} rad/s>";
    #endregion Object overrides
  }
}
