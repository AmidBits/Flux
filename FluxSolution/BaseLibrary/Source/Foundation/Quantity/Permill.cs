namespace Flux.Quantity
{
  /// <summary>Permill means parts per thousand.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Per_mille"/>
  public struct Permill
    : System.IComparable<Permill>, System.IEquatable<Permill>, IValuedUnit
  {
    private readonly double m_value;

    /// <summary>Initialize with a per mille value, e.g. 0.005 for 5‰.</summary>
    public Permill(double permill)
      => m_value = permill;

    public double Value
      => m_value;

    #region Static methods
    public static Permill FromPermillage(double permillage)
      => new Permill(permillage / 1000);
    #endregion Static methods

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

    public static Permill operator -(Permill v)
      => new Permill(-v.m_value);
    public static Permill operator +(Permill a, double b)
      => new Permill(a.m_value + b);
    public static Permill operator +(Permill a, Permill b)
      => a + b.m_value;
    public static Permill operator /(Permill a, double b)
      => new Permill(a.m_value / b);
    public static Permill operator /(Permill a, Permill b)
      => a / b.m_value;
    public static Permill operator *(Permill a, double b)
      => new Permill(a.m_value * b);
    public static Permill operator *(Permill a, Permill b)
      => a * b.m_value;
    public static Permill operator %(Permill a, double b)
      => new Permill(a.m_value % b);
    public static Permill operator %(Permill a, Permill b)
      => a % b.m_value;
    public static Permill operator -(Permill a, double b)
      => new Permill(a.m_value - b);
    public static Permill operator -(Permill a, Permill b)
      => a - b.m_value;
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
      => $"<{GetType().Name}: {m_value * 1000}\u2030>";
    #endregion Object overrides
  }
}
