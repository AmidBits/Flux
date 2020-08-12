using System.Linq;

namespace Flux.Model
{
  public static partial class ExtensionsDouble
  {
    /// <summary>Computes the probability mass function (aka PMF, a probability histogram) from the (assumed) frequency histogram (the dictionary). A probability is a frequency expressed as a fraction of the sum of all frequencies in the sequence.</summary>
    /// <param name="source">Dictionary of values and frequencies.</param>
    /// <param name="factor">A normalizing factor. Defaults to 1.0.</param>
    public static System.Collections.Generic.IDictionary<TKey, double> ProbabilityMassFunction<TKey>(this System.Collections.Generic.IDictionary<TKey, int> source, double factor = 1.0)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      factor /= source.Values.Sum();

      return source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value * factor);
    }
  }
}