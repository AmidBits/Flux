namespace Flux.Units
{
  /// <summary>Percent means parts per hundred.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Ratio"/>
  public struct Percent
    : System.IComparable<Percent>, System.IEquatable<Percent>, IValuedUnit
  {
    private readonly double m_value;

    public Percent(double percent)
      => m_value = percent;

    public double Value
      => m_value;

    public Permill ToPermill()
      => new Permill(m_value * 10);

    #region Overloaded operators
    public static explicit operator double(Percent v)
      => v.Value;

    public static bool operator <(Percent a, Percent b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Percent a, Percent b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Percent a, Percent b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Percent a, Percent b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Percent a, Percent b)
      => a.Equals(b);
    public static bool operator !=(Percent a, Percent b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Percent other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Percent other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Percent o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_value);
    public override string ToString()
      => $"<{GetType().Name}: {m_value} \u0025>";
    #endregion Object overrides
  }
}
