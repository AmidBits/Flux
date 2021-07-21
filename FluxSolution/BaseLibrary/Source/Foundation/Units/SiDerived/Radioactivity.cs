namespace Flux.Units
{
  /// <summary>Radioactivity unit of becquerel.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Power"/>
  public struct Radioactivity
    : System.IComparable<Radioactivity>, System.IEquatable<Radioactivity>, IValuedUnit
  {
    private readonly double m_value;

    public Radioactivity(double becquerel)
      => m_value = becquerel;

    public double Value
      => m_value;

    #region Overloaded operators
    public static explicit operator double(Radioactivity v)
      => v.m_value;
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
      => new Radioactivity(-v.m_value);
    public static Radioactivity operator +(Radioactivity a, Radioactivity b)
      => new Radioactivity(a.m_value + b.m_value);
    public static Radioactivity operator /(Radioactivity a, Radioactivity b)
      => new Radioactivity(a.m_value / b.m_value);
    public static Radioactivity operator *(Radioactivity a, Radioactivity b)
      => new Radioactivity(a.m_value * b.m_value);
    public static Radioactivity operator %(Radioactivity a, Radioactivity b)
      => new Radioactivity(a.m_value % b.m_value);
    public static Radioactivity operator -(Radioactivity a, Radioactivity b)
      => new Radioactivity(a.m_value - b.m_value);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Radioactivity other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Radioactivity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Radioactivity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} Bq>";
    #endregion Object overrides
  }
}
