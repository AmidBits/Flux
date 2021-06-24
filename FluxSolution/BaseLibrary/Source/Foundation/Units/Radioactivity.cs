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

    #region Static methods
    public static Radioactivity Add(Radioactivity left, Radioactivity right)
      => new Radioactivity(left.m_becquerel + right.m_becquerel);
    public static Radioactivity Divide(Radioactivity left, Radioactivity right)
      => new Radioactivity(left.m_becquerel / right.m_becquerel);
    public static Radioactivity Multiply(Radioactivity left, Radioactivity right)
      => new Radioactivity(left.m_becquerel * right.m_becquerel);
    public static Radioactivity Negate(Radioactivity value)
      => new Radioactivity(-value.m_becquerel);
    public static Radioactivity Remainder(Radioactivity dividend, Radioactivity divisor)
      => new Radioactivity(dividend.m_becquerel % divisor.m_becquerel);
    public static Radioactivity Subtract(Radioactivity left, Radioactivity right)
      => new Radioactivity(left.m_becquerel - right.m_becquerel);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(Radioactivity v)
      => v.m_becquerel;
    public static implicit operator Radioactivity(double v)
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

    public static Radioactivity operator +(Radioactivity a, Radioactivity b)
      => Add(a, b);
    public static Radioactivity operator /(Radioactivity a, Radioactivity b)
      => Divide(a, b);
    public static Radioactivity operator *(Radioactivity a, Radioactivity b)
      => Multiply(a, b);
    public static Radioactivity operator -(Radioactivity v)
      => Negate(v);
    public static Radioactivity operator %(Radioactivity a, Radioactivity b)
      => Remainder(a, b);
    public static Radioactivity operator -(Radioactivity a, Radioactivity b)
      => Subtract(a, b);
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
