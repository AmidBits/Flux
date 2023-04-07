namespace Flux
{
  namespace Quantities
  {
    /// <summary>A rate is the ratio between two related quantities that are measured with different units.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rate_(mathematics)"/>
    public readonly record struct Rate<TNumerator, TDenominator>
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

      public TNumerator Numerator { get => m_numerator; init => m_numerator = value; }
      public TDenominator Denominator { get => m_denominator; init => m_denominator = value; }

      public double Ratio => m_numerator.Value / m_denominator.Value;

      public double InverseRatio => m_denominator.Value / m_numerator.Value;

      public override string ToString() => $"{m_numerator.ToQuantityString(null, false, false)} / {m_denominator.ToQuantityString(null, false, false)}";
    }
  }
}
