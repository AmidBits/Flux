namespace Flux.Units
{
  /// <summary>Power.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Power"/>
  public struct Radioactivity
    : System.IComparable<Radioactivity>, System.IEquatable<Radioactivity>, IStandardizedScalar
  {
    private readonly double m_becquerel;

    public Radioactivity(double becquerel)
      => m_becquerel = becquerel;

    public double Becquerel
      => m_becquerel;

    #region Overloaded operators
    public static explicit operator double(Radioactivity v)
      => v.m_becquerel;
    public static explicit operator Radioactivity(double v)
      => new Radioactivity(v);

    public static bool operator <(Radioactivity a, Radioactivity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Radioactivity a, Radioactivity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Radioactivity a, Radioactivity b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Radioactivity a, Radioactivity b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Radioactivity a, Radioactivity b)
      => a.Equals(b);
    public static bool operator !=(Radioactivity a, Radioactivity b)
      => !a.Equals(b);

    public static Radioactivity operator -(Radioactivity v)
      => new Radioactivity(-v.m_becquerel);
    public static Radioactivity operator +(Radioactivity a, Radioactivity b)
      => new Radioactivity(a.m_becquerel + b.m_becquerel);
    public static Radioactivity operator /(Radioactivity a, Radioactivity b)
      => new Radioactivity(a.m_becquerel / b.m_becquerel);
    public static Radioactivity operator *(Radioactivity a, Radioactivity b)
      => new Radioactivity(a.m_becquerel * b.m_becquerel);
    public static Radioactivity operator %(Radioactivity a, Radioactivity b)
      => new Radioactivity(a.m_becquerel % b.m_becquerel);
    public static Radioactivity operator -(Radioactivity a, Radioactivity b)
      => new Radioactivity(a.m_becquerel - b.m_becquerel);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Radioactivity other)
      => m_becquerel.CompareTo(other.m_becquerel);

    // IEquatable
    public bool Equals(Radioactivity other)
      => m_becquerel == other.m_becquerel;

    // IUnitStandardized
    public double GetScalar()
      => m_becquerel;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Radioactivity o && Equals(o);
    public override int GetHashCode()
      => m_becquerel.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_becquerel} Bq>";
    #endregion Object overrides
  }
}
