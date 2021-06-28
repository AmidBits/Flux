namespace Flux.Units
{
  /// <summary>Electrical inductance.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Inductance"/>
  public struct Inductance
    : System.IComparable<Inductance>, System.IEquatable<Inductance>, IStandardizedScalar
  {
    private readonly double m_henry;

    public Inductance(double henry)
      => m_henry = henry;

    public double Henry
      => m_henry;

    #region Static methods
    public static Inductance Add(Inductance left, Inductance right)
      => new Inductance(left.m_henry + right.m_henry);
    public static Inductance Divide(Inductance left, Inductance right)
      => new Inductance(left.m_henry / right.m_henry);
    public static Inductance Multiply(Inductance left, Inductance right)
      => new Inductance(left.m_henry * right.m_henry);
    public static Inductance Negate(Inductance value)
      => new Inductance(-value.m_henry);
    public static Inductance Remainder(Inductance dividend, Inductance divisor)
      => new Inductance(dividend.m_henry % divisor.m_henry);
    public static Inductance Subtract(Inductance left, Inductance right)
      => new Inductance(left.m_henry - right.m_henry);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(Inductance v)
      => v.m_henry;
    public static implicit operator Inductance(double v)
      => new Inductance(v);

    public static bool operator <(Inductance a, Inductance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Inductance a, Inductance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Inductance a, Inductance b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Inductance a, Inductance b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Inductance a, Inductance b)
      => a.Equals(b);
    public static bool operator !=(Inductance a, Inductance b)
      => !a.Equals(b);

    public static Inductance operator +(Inductance a, Inductance b)
      => Add(a, b);
    public static Inductance operator /(Inductance a, Inductance b)
      => Divide(a, b);
    public static Inductance operator *(Inductance a, Inductance b)
      => Multiply(a, b);
    public static Inductance operator -(Inductance v)
      => Negate(v);
    public static Inductance operator %(Inductance a, Inductance b)
      => Remainder(a, b);
    public static Inductance operator -(Inductance a, Inductance b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Inductance other)
      => m_henry.CompareTo(other.m_henry);

    // IEquatable
    public bool Equals(Inductance other)
      => m_henry == other.m_henry;

    // IUnitStandardized
    public double GetScalar()
      => m_henry;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Inductance o && Equals(o);
    public override int GetHashCode()
      => m_henry.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_henry} H>";
    #endregion Object overrides
  }
}
