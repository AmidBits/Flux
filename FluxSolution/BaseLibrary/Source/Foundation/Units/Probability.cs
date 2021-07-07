namespace Flux.Units
{
  /// <summary>Probability is represented as a range [0, 1] of values where 0 indicates impossibility of an event and 1 indicates certainty.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Probability"/>
  public struct Probability
    : System.IComparable<Probability>, System.IEquatable<Probability>, IStandardizedScalar
  {
    public const double MaxValue = 1;
    public const double MinValue = 0;

    private readonly double m_ratio;

    public Probability(double ratio)
      => m_ratio = ratio >= MinValue && ratio <= MaxValue ? ratio : throw new System.ArgumentOutOfRangeException(nameof(ratio));

    public double Ratio
      => m_ratio;

    #region Static methods
    public static Probability Add(Probability left, Probability right)
      => new Probability(left.m_ratio + right.m_ratio);
    public static Probability Divide(Probability left, Probability right)
      => new Probability(left.m_ratio / right.m_ratio);
    public static Probability Multiply(Probability left, Probability right)
      => new Probability(left.m_ratio * right.m_ratio);
    public static Probability Negate(Probability value)
      => new Probability(-value.m_ratio);
    public static Probability Remainder(Probability dividend, Probability divisor)
      => new Probability(dividend.m_ratio % divisor.m_ratio);
    public static Probability Subtract(Probability left, Probability right)
      => new Probability(left.m_ratio - right.m_ratio);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Probability v)
      => v.m_ratio;
    public static explicit operator Probability(double v)
      => new Probability(v);

    public static bool operator <(Probability a, Probability b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Probability a, Probability b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Probability a, Probability b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Probability a, Probability b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Probability a, Probability b)
      => a.Equals(b);
    public static bool operator !=(Probability a, Probability b)
      => !a.Equals(b);

    public static Probability operator +(Probability a, Probability b)
      => Add(a, b);
    public static Probability operator /(Probability a, Probability b)
      => Divide(a, b);
    public static Probability operator *(Probability a, Probability b)
      => Multiply(a, b);
    public static Probability operator -(Probability v)
      => Negate(v);
    public static Probability operator %(Probability a, Probability b)
      => Remainder(a, b);
    public static Probability operator -(Probability a, Probability b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Probability other)
      => m_ratio.CompareTo(other.m_ratio);

    // IEquatable
    public bool Equals(Probability other)
      => m_ratio == other.m_ratio;

    // IUnitStandardized
    public double GetScalar()
      => m_ratio;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Probability o && Equals(o);
    public override int GetHashCode()
      => m_ratio.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_ratio}>";
    #endregion Object overrides
  }
}
