namespace Flux
{
  namespace Units
  {
    /// <summary>A rate is the ratio between two related quantities that are measured with different units.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Rate_(mathematics)"/>
    public readonly record struct PerUnit<TAmount, TDistribution>
      : System.IFormattable
      where TAmount : IValueQuantifiable<double>
      where TDistribution : IValueQuantifiable<double>
    {
      private readonly TAmount m_amount;
      private readonly TDistribution m_distribution;

      public PerUnit(TAmount amount, TDistribution distribution)
      {
        m_amount = amount;
        m_distribution = distribution;
      }

      public TAmount Amount { get => m_amount; init => m_amount = value; }
      public TDistribution Distribution { get => m_distribution; init => m_distribution = value; }

      public string ToString(string? format, System.IFormatProvider? formatProvider) => $"{m_amount.ToString(format, formatProvider)} per unit {m_distribution.ToString(format, formatProvider)} = {m_amount.Value / m_distribution.Value} {m_amount.ToString(format, formatProvider).Split(' ')[1]}/{m_distribution.ToString(format, formatProvider).Split(' ')[1]}";
    }
  }
}
