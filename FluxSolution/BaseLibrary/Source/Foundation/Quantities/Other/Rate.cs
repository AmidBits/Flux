namespace Flux
{
  /// <summary>A rate is the ratio between two related quantities that are measured with different units.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Rate_(mathematics)"/>
  public struct Rate<TNumerator, TDenominator>
    : System.IEquatable<Rate<TNumerator, TDenominator>>
    where TNumerator : IValueGeneralizedUnit<double>
    where TDenominator : IValueGeneralizedUnit<double>
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
      => m_numerator.GeneralUnitValue / m_denominator.GeneralUnitValue;
    public double InverseRatio
      => m_denominator.GeneralUnitValue / m_numerator.GeneralUnitValue;

    #region Overloaded operators
    public static bool operator ==(Rate<TNumerator, TDenominator> a, Rate<TNumerator, TDenominator> b)
      => a.Equals(b);
    public static bool operator !=(Rate<TNumerator, TDenominator> a, Rate<TNumerator, TDenominator> b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Rate<TNumerator, TDenominator> other)
      => m_numerator.Equals(other.m_numerator) && m_denominator.Equals(other.m_denominator);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Rate<TNumerator, TDenominator> o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_numerator.GeneralUnitValue, m_denominator.GeneralUnitValue);
    public override string ToString()
      => $"{GetType().Name} {{ Numerator = {m_numerator}, Denominator = {m_denominator} (Ratio = {Ratio}, Inverse = {InverseRatio}) }}";
    #endregion Object overrides
  }
}
