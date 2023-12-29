namespace Flux.DataStructures
{
  /// <summary>
  /// <para>A probability mass function (PMF) dictionary based on <see cref="SortedDictionary{TKey, double}"/>.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
  /// <para><seealso href="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TProbability"></typeparam>
  /// <remarks>If there's a need to add a key with a POST NORMALIZED probability, divide the probability by the probability remainder, e.g. Add(key, 0.2) is pre-normalized, and Add(key, 0.2 / 0.8) is post-normalized.</remarks>
  public sealed class ProbabilityMassFunction<TKey, TProbability>
      : System.Collections.Generic.SortedDictionary<TKey, TProbability>
      where TKey : System.Numerics.INumber<TKey>
      where TProbability : System.Numerics.IFloatingPointIeee754<TProbability>
  {
    /// <summary>Yields the total sum of all probabilities in the PMF and equates to 1 if normalized.</summary>
    public TProbability Total
      => Values.Sum();

    /// <summary>Determines if the PMF is normalized.</summary>
    public bool IsNormalized()
      => Total == TProbability.One;

    public TProbability Mean()
    {
      var mean = TProbability.Zero;

      foreach (var kvp in this)
        mean += kvp.Value * TProbability.CreateChecked(kvp.Key);

      return mean;
    }

    public TProbability Variance(TProbability mu)
    {
      var variance = TProbability.Zero;

      foreach (var kvp in this)
      {
        var vMmu = TProbability.CreateChecked(kvp.Key) - mu;

        variance += kvp.Value * vMmu * vMmu;
      }

      return variance;
    }

    /// <summary>Creates a new PMF with all values normalized.</summary>
    public void Normalize()
    {
      var factor = TProbability.One / Total;

      foreach (var key in Keys.ToList())
        this[key] *= factor;
    }

    /// <summary>Get the PMF (probability) of the <paramref name="key"/> if it exists, otherwise zero.</summary>
    /// <param name="key">The key to lookup probability for.</param>
    /// <returns>The probability of the <paramref name="key"/>, or zero if not found.</returns>
    public TProbability Pmf(TKey key)
      => TryGetValue(key, out var probability) ? probability : TProbability.Zero;

    public CumulativeDistributionFunction<TKey, TProbability> ToCumulativeDistributionFunction()
    {
      var cdf = new CumulativeDistributionFunction<TKey, TProbability>();

      var accumulation = TProbability.Zero;

      foreach (var key in Keys.ToList())
      {
        accumulation += this[key];

        cdf.Add(key, accumulation);
      }

      return cdf;
    }
  }
}
