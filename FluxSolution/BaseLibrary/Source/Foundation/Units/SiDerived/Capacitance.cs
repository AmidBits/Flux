namespace Flux.Units
{
  /// <summary>Electrical capacitance.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Capacitance"/>
  public struct Capacitance
    : System.IComparable<Capacitance>, System.IEquatable<Capacitance>, IStandardizedScalar
  {
    private readonly double m_farad;

    public Capacitance(double farad)
      => m_farad = farad;

    public double Farad
      => m_farad;

    #region Static methods
    public static Capacitance Add(Capacitance left, Capacitance right)
      => new Capacitance(left.m_farad + right.m_farad);
    public static Capacitance Divide(Capacitance left, Capacitance right)
      => new Capacitance(left.m_farad / right.m_farad);
    public static Capacitance Multiply(Capacitance left, Capacitance right)
      => new Capacitance(left.m_farad * right.m_farad);
    public static Capacitance Negate(Capacitance value)
      => new Capacitance(-value.m_farad);
    public static Capacitance Remainder(Capacitance dividend, Capacitance divisor)
      => new Capacitance(dividend.m_farad % divisor.m_farad);
    public static Capacitance Subtract(Capacitance left, Capacitance right)
      => new Capacitance(left.m_farad - right.m_farad);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Capacitance v)
      => v.m_farad;
    public static explicit operator Capacitance(double v)
      => new Capacitance(v);

    public static bool operator <(Capacitance a, Capacitance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Capacitance a, Capacitance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Capacitance a, Capacitance b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Capacitance a, Capacitance b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Capacitance a, Capacitance b)
      => a.Equals(b);
    public static bool operator !=(Capacitance a, Capacitance b)
      => !a.Equals(b);

    public static Capacitance operator +(Capacitance a, Capacitance b)
      => Add(a, b);
    public static Capacitance operator /(Capacitance a, Capacitance b)
      => Divide(a, b);
    public static Capacitance operator *(Capacitance a, Capacitance b)
      => Multiply(a, b);
    public static Capacitance operator -(Capacitance v)
      => Negate(v);
    public static Capacitance operator %(Capacitance a, Capacitance b)
      => Remainder(a, b);
    public static Capacitance operator -(Capacitance a, Capacitance b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Capacitance other)
      => m_farad.CompareTo(other.m_farad);

    // IEquatable
    public bool Equals(Capacitance other)
      => m_farad == other.m_farad;

    // IUnitStandardized
    public double GetScalar()
      => m_farad;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Capacitance o && Equals(o);
    public override int GetHashCode()
      => m_farad.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_farad} F>";
    #endregion Object overrides
  }
}
