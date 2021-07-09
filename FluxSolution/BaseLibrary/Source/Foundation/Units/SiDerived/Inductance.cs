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

    #region Overloaded operators
    public static explicit operator double(Inductance v)
      => v.m_henry;
    public static explicit operator Inductance(double v)
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

    public static Inductance operator -(Inductance v)
      => new Inductance(-v.m_henry);
    public static Inductance operator +(Inductance a, Inductance b)
       => new Inductance(a.m_henry + b.m_henry);
    public static Inductance operator /(Inductance a, Inductance b)
      => new Inductance(a.m_henry / b.m_henry);
    public static Inductance operator *(Inductance a, Inductance b)
      => new Inductance(a.m_henry * b.m_henry);
    public static Inductance operator %(Inductance a, Inductance b)
      => new Inductance(a.m_henry % b.m_henry);
    public static Inductance operator -(Inductance a, Inductance b)
      => new Inductance(a.m_henry - b.m_henry);
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
