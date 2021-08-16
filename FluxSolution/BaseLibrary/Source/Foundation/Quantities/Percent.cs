namespace Flux.Quantity
{
  /// <summary>Percent means parts per hundred.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Ratio"/>
  public struct Percent
    : System.IComparable<Percent>, System.IEquatable<Percent>, IValuedUnit
  {
    private readonly double m_value;

    /// <summary>Initialize with a per mille value, e.g. 0.05 for 5%.</summary>
    public Percent(double percent)
      => m_value = percent;

    public double Value
      => m_value;

    #region Static methods
    public static Percent FromPercentage(double percentage)
      => new Percent(percentage / 100);
    #endregion Static methods

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

    public static Percent operator -(Percent v)
      => new Percent(-v.m_value);
    public static Percent operator +(Percent a, double b)
      => new Percent(a.m_value + b);
    public static Percent operator +(Percent a, Percent b)
      => a + b.m_value;
    public static Percent operator /(Percent a, double b)
      => new Percent(a.m_value / b);
    public static Percent operator /(Percent a, Percent b)
      => a / b.m_value;
    public static Percent operator *(Percent a, double b)
      => new Percent(a.m_value * b);
    public static Percent operator *(Percent a, Percent b)
      => a * b.m_value;
    public static Percent operator %(Percent a, double b)
      => new Percent(a.m_value % b);
    public static Percent operator %(Percent a, Percent b)
      => a % b.m_value;
    public static Percent operator -(Percent a, double b)
      => new Percent(a.m_value - b);
    public static Percent operator -(Percent a, Percent b)
      => a - b.m_value;
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
      => $"<{GetType().Name}: {m_value * 100}\u0025>";
    #endregion Object overrides
  }
}
