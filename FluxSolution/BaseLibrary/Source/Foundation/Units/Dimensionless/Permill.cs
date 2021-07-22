namespace Flux.Units
{
  /// <summary>Permill means parts per thousand.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Per_mille"/>
  public struct Permill
    : System.IComparable<Permill>, System.IEquatable<Permill>, IValuedUnit
  {
    private readonly double m_value;

    public Permill(double permill)
      => m_value = permill;

    public double Fraction
      => m_value / 1000;

    public double Value
      => m_value;

    public Percent ToPercent()
      => new Percent(m_value / 10);

    #region Overloaded operators
    public static explicit operator double(Permill v)
      => v.Value;

    public static bool operator <(Permill a, Permill b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Permill a, Permill b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Permill a, Permill b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Permill a, Permill b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Permill a, Permill b)
      => a.Equals(b);
    public static bool operator !=(Permill a, Permill b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Permill other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Permill other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Permill o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_value);
    public override string ToString()
      => $"<{GetType().Name}: {m_value} \u2030>";
    #endregion Object overrides
  }
}
