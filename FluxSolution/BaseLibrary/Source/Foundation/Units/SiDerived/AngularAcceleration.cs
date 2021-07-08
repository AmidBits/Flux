namespace Flux.Units
{
  /// <summary>Angular acceleration.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
  public struct AngularAcceleration
    : System.IComparable<AngularAcceleration>, System.IEquatable<AngularAcceleration>, IStandardizedScalar
  {
    private readonly double m_radianPerSecondSquare;

    public AngularAcceleration(double radianPerSecondSquare)
      => m_radianPerSecondSquare = radianPerSecondSquare;

    public double RadianPerSecondSquare
      => m_radianPerSecondSquare;

    #region Static methods
    public static AngularAcceleration Add(AngularAcceleration left, AngularAcceleration right)
      => new AngularAcceleration(left.m_radianPerSecondSquare + right.m_radianPerSecondSquare);
    public static AngularAcceleration Divide(AngularAcceleration left, AngularAcceleration right)
      => new AngularAcceleration(left.m_radianPerSecondSquare / right.m_radianPerSecondSquare);
    public static AngularAcceleration Multiply(AngularAcceleration left, AngularAcceleration right)
      => new AngularAcceleration(left.m_radianPerSecondSquare * right.m_radianPerSecondSquare);
    public static AngularAcceleration Negate(AngularAcceleration value)
      => new AngularAcceleration(-value.m_radianPerSecondSquare);
    public static AngularAcceleration Remainder(AngularAcceleration dividend, AngularAcceleration divisor)
      => new AngularAcceleration(dividend.m_radianPerSecondSquare % divisor.m_radianPerSecondSquare);
    public static AngularAcceleration Subtract(AngularAcceleration left, AngularAcceleration right)
      => new AngularAcceleration(left.m_radianPerSecondSquare - right.m_radianPerSecondSquare);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(AngularAcceleration v)
      => v.m_radianPerSecondSquare;
    public static explicit operator AngularAcceleration(double v)
      => new AngularAcceleration(v);

    public static bool operator <(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(AngularAcceleration a, AngularAcceleration b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(AngularAcceleration a, AngularAcceleration b)
      => a.Equals(b);
    public static bool operator !=(AngularAcceleration a, AngularAcceleration b)
      => !a.Equals(b);

    public static AngularAcceleration operator +(AngularAcceleration a, AngularAcceleration b)
      => Add(a, b);
    public static AngularAcceleration operator /(AngularAcceleration a, AngularAcceleration b)
      => Divide(a, b);
    public static AngularAcceleration operator *(AngularAcceleration a, AngularAcceleration b)
      => Multiply(a, b);
    public static AngularAcceleration operator -(AngularAcceleration v)
      => Negate(v);
    public static AngularAcceleration operator %(AngularAcceleration a, AngularAcceleration b)
      => Remainder(a, b);
    public static AngularAcceleration operator -(AngularAcceleration a, AngularAcceleration b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AngularAcceleration other)
      => m_radianPerSecondSquare.CompareTo(other.m_radianPerSecondSquare);

    // IEquatable
    public bool Equals(AngularAcceleration other)
      => m_radianPerSecondSquare == other.m_radianPerSecondSquare;

    // IUnitStandardized
    public double GetScalar()
      => m_radianPerSecondSquare;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AngularAcceleration o && Equals(o);
    public override int GetHashCode()
      => m_radianPerSecondSquare.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_radianPerSecondSquare} rad/s²>";
    #endregion Object overrides
  }
}
