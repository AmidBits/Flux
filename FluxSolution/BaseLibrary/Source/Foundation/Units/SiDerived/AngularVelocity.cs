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

    public double SquareMeter
      => m_radianPerSecond;

    #region Static methods
    public static AngularVelocity Add(AngularVelocity left, AngularVelocity right)
      => new AngularVelocity(left.m_radianPerSecond + right.m_radianPerSecond);
    public static AngularVelocity Divide(AngularVelocity left, AngularVelocity right)
      => new AngularVelocity(left.m_radianPerSecond / right.m_radianPerSecond);
    public static AngularVelocity FromRectangule(double lengthInMeters, double widthInMeters)
      => new AngularVelocity(lengthInMeters * widthInMeters);
    public static AngularVelocity Multiply(AngularVelocity left, AngularVelocity right)
      => new AngularVelocity(left.m_radianPerSecond * right.m_radianPerSecond);
    public static AngularVelocity Negate(AngularVelocity value)
      => new AngularVelocity(-value.m_radianPerSecond);
    public static AngularVelocity Remainder(AngularVelocity dividend, AngularVelocity divisor)
      => new AngularVelocity(dividend.m_radianPerSecond % divisor.m_radianPerSecond);
    public static AngularVelocity Subtract(AngularVelocity left, AngularVelocity right)
      => new AngularVelocity(left.m_radianPerSecond - right.m_radianPerSecond);
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

    public static AngularVelocity operator +(AngularVelocity a, AngularVelocity b)
      => Add(a, b);
    public static AngularVelocity operator /(AngularVelocity a, AngularVelocity b)
      => Divide(a, b);
    public static AngularVelocity operator *(AngularVelocity a, AngularVelocity b)
      => Multiply(a, b);
    public static AngularVelocity operator -(AngularVelocity v)
      => Negate(v);
    public static AngularVelocity operator %(AngularVelocity a, AngularVelocity b)
      => Remainder(a, b);
    public static AngularVelocity operator -(AngularVelocity a, AngularVelocity b)
      => Subtract(a, b);
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
