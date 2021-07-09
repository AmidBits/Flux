namespace Flux.Units
{
  /// <summary>Energy.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Energy"/>
  public struct Energy
    : System.IComparable<Energy>, System.IEquatable<Energy>, IStandardizedScalar
  {
    private readonly double m_joule;

    public Energy(double joule)
      => m_joule = joule;

    public double Joule
      => m_joule;

    #region Overloaded operators
    public static explicit operator double(Energy v)
      => v.m_joule;
    public static explicit operator Energy(double v)
      => new Energy(v);

    public static bool operator <(Energy a, Energy b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Energy a, Energy b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Energy a, Energy b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Energy a, Energy b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Energy a, Energy b)
      => a.Equals(b);
    public static bool operator !=(Energy a, Energy b)
      => !a.Equals(b);

    public static Energy operator -(Energy v)
      => new Energy(-v.m_joule);
    public static Energy operator +(Energy a, Energy b)
      => new Energy(a.m_joule + b.m_joule);
    public static Energy operator /(Energy a, Energy b)
      => new Energy(a.m_joule / b.m_joule);
    public static Energy operator *(Energy a, Energy b)
      => new Energy(a.m_joule * b.m_joule);
    public static Energy operator %(Energy a, Energy b)
      => new Energy(a.m_joule % b.m_joule);
    public static Energy operator -(Energy a, Energy b)
      => new Energy(a.m_joule - b.m_joule);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Energy other)
      => m_joule.CompareTo(other.m_joule);

    // IEquatable
    public bool Equals(Energy other)
      => m_joule == other.m_joule;

    // IUnitStandardized
    public double GetScalar()
      => m_joule;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Energy o && Equals(o);
    public override int GetHashCode()
      => m_joule.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_joule} J>";
    #endregion Object overrides
  }
}
