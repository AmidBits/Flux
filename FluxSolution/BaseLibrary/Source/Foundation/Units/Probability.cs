namespace Flux.Units
{
  /// <summary>Probability.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Probability"/>
  public struct Probability
    : System.IComparable<Probability>, System.IEquatable<Probability>, IStandardizedScalar
  {
    public const double MaxValue = 1;
    public const double MinValue = 0;

    private readonly double m_percent;

    public Probability(double percent)
      => m_percent = percent >= MinValue && percent <= MaxValue ? percent : throw new System.ArgumentOutOfRangeException(nameof(percent));

    public double Percent
      => m_percent;

    #region Static methods
    public static Probability Add(Probability left, Probability right)
      => new Probability(left.m_percent + right.m_percent);
    public static Probability Divide(Probability left, Probability right)
      => new Probability(left.m_percent / right.m_percent);
    public static Probability Multiply(Probability left, Probability right)
      => new Probability(left.m_percent * right.m_percent);
    public static Probability Negate(Probability value)
      => new Probability(-value.m_percent);
    public static Probability Remainder(Probability dividend, Probability divisor)
      => new Probability(dividend.m_percent % divisor.m_percent);
    public static Probability Subtract(Probability left, Probability right)
      => new Probability(left.m_percent - right.m_percent);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(Probability v)
      => v.m_percent;
    public static implicit operator Probability(double v)
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
      => m_percent.CompareTo(other.m_percent);

    // IEquatable
    public bool Equals(Probability other)
      => m_percent == other.m_percent;

    // IUnitStandardized
    public double GetScalar()
      => m_percent;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Probability o && Equals(o);
    public override int GetHashCode()
      => m_percent.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_percent * 100}%>";
    #endregion Object overrides
  }
}
