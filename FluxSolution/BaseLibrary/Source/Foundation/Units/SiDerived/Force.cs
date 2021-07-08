namespace Flux.Units
{
  /// <summary>Force.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Force"/>
  public struct Force
    : System.IComparable<Force>, System.IEquatable<Force>, IStandardizedScalar
  {
    private readonly double m_newton;

    public Force(double newton)
      => m_newton = newton;

    public double Newton
      => m_newton;

    #region Overloaded operators
    public static explicit operator double(Force v)
      => v.m_newton;
    public static explicit operator Force(double v)
      => new Force(v);

    public static bool operator <(Force a, Force b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Force a, Force b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Force a, Force b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Force a, Force b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Force a, Force b)
      => a.Equals(b);
    public static bool operator !=(Force a, Force b)
      => !a.Equals(b);

    public static Force operator -(Force v)
      => new Force(-v.m_newton);
    public static Force operator +(Force a, Force b)
      => new Force(a.m_newton + b.m_newton);
    public static Force operator /(Force a, Force b)
      => new Force(a.m_newton / b.m_newton);
    public static Force operator %(Force a, Force b)
      => new Force(a.m_newton % b.m_newton);
    public static Force operator *(Force a, Force b)
      => new Force(a.m_newton * b.m_newton);
    public static Force operator -(Force a, Force b)
      => new Force(a.m_newton - b.m_newton);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Force other)
      => m_newton.CompareTo(other.m_newton);

    // IEquatable
    public bool Equals(Force other)
      => m_newton == other.m_newton;

    // IUnitStandardized
    public double GetScalar()
      => m_newton;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Force o && Equals(o);
    public override int GetHashCode()
      => m_newton.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_newton} N>";
    #endregion Object overrides
  }
}
