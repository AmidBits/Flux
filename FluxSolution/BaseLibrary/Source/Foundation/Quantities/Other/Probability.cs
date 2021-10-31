namespace Flux.Quantity
{
  /// <summary>Probability is a ratio, represented as a range [0, 1] of values where 0 indicates impossibility of an event and 1 indicates certainty.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Probability"/>
  public struct Probability
    : System.IComparable<Probability>, System.IEquatable<Probability>, IValuedUnit
  {
    public const double MaxValue = 1;
    public const double MinValue = 0;

    private readonly double m_probability;

    public Probability(double ratio)
      => m_probability = ratio >= MinValue && ratio <= MaxValue ? ratio : throw new System.ArgumentOutOfRangeException(nameof(ratio));

    public double Value
      => m_probability;

    #region Overloaded operators
    public static explicit operator double(Probability v)
      => v.m_probability;
    public static explicit operator Probability(double v)
      => new Probability(v);

    public static bool operator <(Probability a, Probability b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Probability a, Probability b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Probability a, Probability b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Probability a, Probability b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Probability a, Probability b)
      => a.Equals(b);
    public static bool operator !=(Probability a, Probability b)
      => !a.Equals(b);

    public static Probability operator -(Probability v)
      => new Probability(-v.m_probability);
    public static Probability operator +(Probability a, double b)
      => new Probability(a.m_probability + b);
    public static Probability operator +(Probability a, Probability b)
      => a + b.m_probability;
    public static Probability operator /(Probability a, double b)
      => new Probability(a.m_probability / b);
    public static Probability operator /(Probability a, Probability b)
      => a / b.m_probability;
    public static Probability operator *(Probability a, double b)
      => new Probability(a.m_probability * b);
    public static Probability operator *(Probability a, Probability b)
      => a * b.m_probability;
    public static Probability operator %(Probability a, double b)
      => new Probability(a.m_probability % b);
    public static Probability operator %(Probability a, Probability b)
      => a % b.m_probability;
    public static Probability operator -(Probability a, double b)
      => new Probability(a.m_probability - b);
    public static Probability operator -(Probability a, Probability b)
      => a - b.m_probability;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Probability other)
      => m_probability.CompareTo(other.m_probability);

    // IEquatable
    public bool Equals(Probability other)
      => m_probability == other.m_probability;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Probability o && Equals(o);
    public override int GetHashCode()
      => m_probability.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_probability}>";
    #endregion Object overrides
  }
}
