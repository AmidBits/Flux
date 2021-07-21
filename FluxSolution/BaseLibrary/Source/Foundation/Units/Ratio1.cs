namespace Flux.Units
{
  /// <summary>A ratio indicates how many times one number contains another. This struct simply keeps the quotient of the ratio.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Ratio"/>
  public struct Ratio1
    : System.IComparable<Ratio1>, System.IEquatable<Ratio1>, IValuedUnit
  {
    private readonly double m_value;

    public Ratio1(double quotient)
      => m_value = quotient;

    public double Value
      => m_value;

    #region Overloaded operators
    public static explicit operator double(Ratio1 v)
      => v.Value;

    public static bool operator <(Ratio1 a, Ratio1 b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Ratio1 a, Ratio1 b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Ratio1 a, Ratio1 b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Ratio1 a, Ratio1 b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Ratio1 a, Ratio1 b)
      => a.Equals(b);
    public static bool operator !=(Ratio1 a, Ratio1 b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Ratio1 other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Ratio1 other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Ratio1 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_value);
    public override string ToString()
      => $"<{GetType().Name}: {m_value}>";
    #endregion Object overrides
  }
}
