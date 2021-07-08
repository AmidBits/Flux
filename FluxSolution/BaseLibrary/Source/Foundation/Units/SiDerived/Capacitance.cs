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

    public static Capacitance operator -(Capacitance v)
      => new Capacitance(-v.m_farad);
    public static Capacitance operator +(Capacitance a, Capacitance b)
      => new Capacitance(a.m_farad + b.m_farad);
    public static Capacitance operator /(Capacitance a, Capacitance b)
      => new Capacitance(a.m_farad / b.m_farad);
    public static Capacitance operator %(Capacitance a, Capacitance b)
      => new Capacitance(a.m_farad % b.m_farad);
    public static Capacitance operator *(Capacitance a, Capacitance b)
      => new Capacitance(a.m_farad * b.m_farad);
    public static Capacitance operator -(Capacitance a, Capacitance b)
      => new Capacitance(a.m_farad - b.m_farad);
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
