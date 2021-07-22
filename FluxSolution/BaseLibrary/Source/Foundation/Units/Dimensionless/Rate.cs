namespace Flux.Units
{
  /// <summary>A rate is the ratio between two related quantities in different units.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Rate_(mathematics)"/>
  public struct Rate<TNumerator, TDenominator>
    : System.IComparable<Rate<TNumerator, TDenominator>>, System.IEquatable<Rate<TNumerator, TDenominator>>, IValuedUnit
    where TNumerator : IValuedUnit
    where TDenominator : IValuedUnit
  {
    private readonly TNumerator m_numerator;
    private readonly TDenominator m_denominator;

    public Rate(TNumerator numerator, TDenominator denominator)
    {
      m_numerator = numerator;
      m_denominator = denominator;
    }

    public TNumerator Numerator
      => m_numerator;
    public TDenominator Denominator
      => m_denominator;

    public double Value
      => m_numerator.Value / m_denominator.Value;

    #region Overloaded operators
    public static explicit operator double(Rate<TNumerator, TDenominator> v)
      => v.Value;

    public static bool operator <(Rate<TNumerator, TDenominator> a, Rate<TNumerator, TDenominator> b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Rate<TNumerator, TDenominator> a, Rate<TNumerator, TDenominator> b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Rate<TNumerator, TDenominator> a, Rate<TNumerator, TDenominator> b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Rate<TNumerator, TDenominator> a, Rate<TNumerator, TDenominator> b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Rate<TNumerator, TDenominator> a, Rate<TNumerator, TDenominator> b)
      => a.Equals(b);
    public static bool operator !=(Rate<TNumerator, TDenominator> a, Rate<TNumerator, TDenominator> b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Rate<TNumerator, TDenominator> other)
      => Value.CompareTo(other.Value);

    // IEquatable
    public bool Equals(Rate<TNumerator, TDenominator> other)
      => Value.Equals(other.Value);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Rate<TNumerator, TDenominator> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_numerator.Value, m_denominator.Value);
    public override string ToString()
      => $"<{GetType().Name}: {m_numerator}:{m_denominator}>";
    #endregion Object overrides
  }
}
