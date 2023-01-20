using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    // http://www.greenteapress.com/thinkstats/thinkstats.pdf

    /// <summary>Returns the rank of the keys in a histogram as a percentage of the histogram.</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, double> PercentRank<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, int> frequencySelector, double factor = 1)
      where TKey : notnull
    {
      var histogram = new System.Collections.Generic.SortedDictionary<TKey, double>();

      var totalFrequencies = 0;

      foreach (var item in source.ThrowIfNull())
      {
        var key = keySelector(item);
        var frequency = frequencySelector(item);

        totalFrequencies += frequency;

        histogram[key] = histogram.TryGetValue(key, out var currentFrequency) ? currentFrequency + frequency : frequency;
      }

      var pr = new System.Collections.Generic.SortedDictionary<TKey, double>();

      var cumulativeFrequencies = 0;

      foreach (var key in histogram.Keys)
      {
        var preCount = System.Convert.ToInt32(histogram[key]);

        pr[key] = cumulativeFrequencies / (double)(cumulativeFrequencies + (totalFrequencies - cumulativeFrequencies - 1)) * factor;

        cumulativeFrequencies += preCount;
      }

      return histogram;
    }

    /// <summary>Returns the rank of the keys in a histogram as a percentage of the histogram.</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, double> PercentRank<TKey>(this System.Collections.Generic.IDictionary<TKey, int> source, int? sumOfFrequencies = null, double factor = 1)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var pr = new System.Collections.Generic.SortedDictionary<TKey, double>();

      var sof = sumOfFrequencies ?? source.Values.Sum();

      var count = 0;

      foreach (var item in source.OrderBy(kvp => kvp.Key))
      {
        pr.Add(item.Key, count / (double)(count + (sof - count - 1)) * factor);

        count += item.Value;
      }

      return pr;
    }
  }
}
