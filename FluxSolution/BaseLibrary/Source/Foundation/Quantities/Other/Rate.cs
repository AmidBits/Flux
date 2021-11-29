namespace Flux.Quantity
{
  /// <summary>A rate is the ratio between two related quantities that are measured with different units.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Rate_(mathematics)"/>
#if NET5_0
  public struct Rate<TNumerator, TDenominator>
    : System.IEquatable<Rate<TNumerator, TDenominator>>
    where TNumerator : IValuedUnit<double>
    where TDenominator : IValuedUnit<double>
#elif NET6_0_OR_GREATER
  public record struct Rate<TNumerator, TDenominator>
    where TNumerator : IValuedUnit<double>
    where TDenominator : IValuedUnit<double>
#endif
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

    public double Ratio
      => m_numerator.Value / m_denominator.Value;
    public double InverseRatio
      => m_denominator.Value / m_numerator.Value;

    #region Overloaded operators
#if NET5_0
    public static bool operator ==(Rate<TNumerator, TDenominator> a, Rate<TNumerator, TDenominator> b)
      => a.Equals(b);
    public static bool operator !=(Rate<TNumerator, TDenominator> a, Rate<TNumerator, TDenominator> b)
      => !a.Equals(b);
#endif
    #endregion Overloaded operators

    #region Implemented interfaces
#if NET5_0
    // IEquatable
    public bool Equals(Rate<TNumerator, TDenominator> other)
      => m_numerator.Equals(other.m_numerator) && m_denominator.Equals(other.m_denominator);
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is Rate<TNumerator, TDenominator> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_numerator.Value, m_denominator.Value);
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Numerator = {m_numerator}, Denominator = {m_denominator} (Ratio = {Ratio}, Inverse = {InverseRatio}) }}";
    #endregion Object overrides
  }
}
