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

    public static AngularAcceleration operator -(AngularAcceleration v)
      => new AngularAcceleration(-v.m_radianPerSecondSquare);
    public static AngularAcceleration operator +(AngularAcceleration a, AngularAcceleration b)
      => new AngularAcceleration(a.m_radianPerSecondSquare + b.m_radianPerSecondSquare);
    public static AngularAcceleration operator /(AngularAcceleration a, AngularAcceleration b)
      => new AngularAcceleration(a.m_radianPerSecondSquare / b.m_radianPerSecondSquare);
    public static AngularAcceleration operator *(AngularAcceleration a, AngularAcceleration b)
      => new AngularAcceleration(a.m_radianPerSecondSquare * b.m_radianPerSecondSquare);
    public static AngularAcceleration operator %(AngularAcceleration a, AngularAcceleration b)
      => new AngularAcceleration(a.m_radianPerSecondSquare % b.m_radianPerSecondSquare);
    public static AngularAcceleration operator -(AngularAcceleration a, AngularAcceleration b)
      => new AngularAcceleration(a.m_radianPerSecondSquare - b.m_radianPerSecondSquare);
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
