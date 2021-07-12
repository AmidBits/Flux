namespace Flux.Units
{
  /// <summary>Ratio indicates how many times one number contains another.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Ratio"/>
  public struct RatioFraction
    : System.IComparable<RatioFraction>, System.IEquatable<RatioFraction>, IStandardizedScalar
  {
    private readonly long m_numerator;
    private readonly long m_denominator;

    public RatioFraction(long numerator, long denominator, bool reduceIfPossible)
    {
      if (reduceIfPossible && IsReducible(numerator, denominator, out var gcd))
      {
        m_numerator = numerator / gcd;
        m_denominator = denominator / gcd;
      }
      else
      {
        m_numerator = numerator;
        m_denominator = denominator;
      }
    }
    public RatioFraction(long numerator, long denominator)
      : this(numerator, denominator, true)
    { }

    public long Numerator
      => m_numerator;
    public long Denominator
      => m_denominator;

    public double Value
      => (double)m_numerator / (double)m_denominator;

    #region Static methods
    public static bool IsReducible(long numerator, long denominator, out long gcd)
    {
      gcd = Maths.GreatestCommonDivisor(numerator, denominator);

      return gcd > 1;
    }
    #endregion Static methods

    #region Overloaded operators
    public static bool operator <(RatioFraction a, RatioFraction b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(RatioFraction a, RatioFraction b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(RatioFraction a, RatioFraction b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(RatioFraction a, RatioFraction b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(RatioFraction a, RatioFraction b)
      => a.Equals(b);
    public static bool operator !=(RatioFraction a, RatioFraction b)
      => !a.Equals(b);

    public static RatioFraction operator -(RatioFraction v)
      => new RatioFraction(-v.m_numerator, -v.m_denominator, false);
    public static RatioFraction operator +(RatioFraction a, RatioFraction b)
    {
      var lcm = Maths.LeastCommonMultiple(a.m_denominator, b.m_denominator);

      var an = lcm / a.m_denominator * a.m_numerator;
      var bn = lcm / b.m_denominator * b.m_numerator;

      return new RatioFraction(an + bn, lcm);
    }
    public static RatioFraction operator /(RatioFraction a, RatioFraction b)
      => new RatioFraction(a.m_numerator * b.m_denominator, a.m_denominator * b.m_numerator);
    public static RatioFraction operator *(RatioFraction a, RatioFraction b)
      => new RatioFraction(a.m_numerator * b.m_numerator, a.m_denominator * b.m_denominator);
    public static RatioFraction operator -(RatioFraction a, RatioFraction b)
    {
      var lcm = Maths.LeastCommonMultiple(a.m_denominator, b.m_denominator);

      var an = lcm / a.m_denominator * a.m_numerator;
      var bn = lcm / b.m_denominator * b.m_numerator;

      return new RatioFraction(an - bn, lcm);
    }
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(RatioFraction other)
      => Value.CompareTo(other.Value);

    // IEquatable
    public bool Equals(RatioFraction other)
      => m_numerator == other.m_numerator && m_denominator == other.m_denominator;

    // IUnitStandardized
    public double GetScalar()
      => Value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is RatioFraction o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_numerator, m_denominator);
    public override string ToString()
      => $"<{GetType().Name}: {m_numerator}:{m_denominator} ({Value})>";
    #endregion Object overrides
  }
}
