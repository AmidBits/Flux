using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    // http://www.greenteapress.com/thinkstats/thinkstats.pdf

    /// <summary>The CDF is the function that maps values to their percentile rank, in a probability range [0, 1], in a distribution.</summary>
    /// <remarks>For consistency, a discrete CDF should be called a cumulative mass function(CMF), but that seems just ignored.</remarks>
    public static System.Collections.Generic.SortedDictionary<TKey, double> PercentRank<TKey>(this System.Collections.Generic.IDictionary<TKey, int> source, int sumOfAllFrequencies, double factor = 1)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var pmf = new System.Collections.Generic.SortedDictionary<TKey, double>();

      var counter = 0;

      foreach (var kvp in source.OrderBy(kvp => kvp.Key))
      {
        pmf.Add(kvp.Key, (double)counter / (counter + (sumOfAllFrequencies - counter - 1)) * factor);

        counter += kvp.Value;
      }

      return pmf;
    }
  }
}
