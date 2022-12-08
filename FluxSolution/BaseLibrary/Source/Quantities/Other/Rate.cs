namespace Flux
{
  namespace Quantities
  {
    /// <summary>A rate is the ratio between two related quantities that are measured with different units.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rate_(mathematics)"/>
    public readonly struct Rate<TNumerator, TDenominator>
    : System.IEquatable<Rate<TNumerator, TDenominator>>
    where TNumerator : IQuantifiable<double>
    where TDenominator : IQuantifiable<double>
    {
      private readonly TNumerator m_numerator;
      private readonly TDenominator m_denominator;

      public Rate(TNumerator numerator, TDenominator denominator)
      {
        m_numerator = numerator;
        m_denominator = denominator;
      }

      [System.Diagnostics.Contracts.Pure]
      public TNumerator Numerator
        => m_numerator;
      [System.Diagnostics.Contracts.Pure]
      public TDenominator Denominator
        => m_denominator;

      [System.Diagnostics.Contracts.Pure]
      public double Ratio
        => m_numerator.Value / m_denominator.Value;
      [System.Diagnostics.Contracts.Pure]
      public double InverseRatio
        => m_denominator.Value / m_numerator.Value;

      #region Overloaded operators
      [System.Diagnostics.Contracts.Pure]
      public static bool operator ==(Rate<TNumerator, TDenominator> a, Rate<TNumerator, TDenominator> b)
        => a.Equals(b);
      [System.Diagnostics.Contracts.Pure]
      public static bool operator !=(Rate<TNumerator, TDenominator> a, Rate<TNumerator, TDenominator> b)
        => !a.Equals(b);
      #endregion Overloaded operators

      #region Implemented interfaces
      // IEquatable<>
      [System.Diagnostics.Contracts.Pure]
      public bool Equals(Rate<TNumerator, TDenominator> other)
        => m_numerator.Equals(other.m_numerator) && m_denominator.Equals(other.m_denominator);
      #endregion Implemented interfaces

      #region Object overrides
      [System.Diagnostics.Contracts.Pure]
      public override bool Equals(object? obj)
        => obj is Rate<TNumerator, TDenominator> o && Equals(o);
      [System.Diagnostics.Contracts.Pure]
      public override int GetHashCode()
        => System.HashCode.Combine(m_numerator.Value, m_denominator.Value);
      [System.Diagnostics.Contracts.Pure]
      public override string ToString()
        => $"{GetType().Name} {{ Numerator = {m_numerator}, Denominator = {m_denominator} (Ratio = {Ratio}, Inverse = {InverseRatio}) }}";
      #endregion Object overrides
    }
  }
}
