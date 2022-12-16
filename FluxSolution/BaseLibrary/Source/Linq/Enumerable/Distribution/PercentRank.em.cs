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
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var keys = new System.Collections.Generic.HashSet<TKey>();

      var pr = new System.Collections.Generic.SortedDictionary<TKey, double>();

      var sumOfFrequencies = 0;

      foreach (var item in source)
      {
        var key = keySelector(item);

        keys.Add(key);

        var frequency = frequencySelector(item);

        sumOfFrequencies += frequency;

        pr[key] = pr.TryGetValue(key, out var currentFrequency) ? currentFrequency + frequency : frequency;
      }

      var count = 0;

      foreach (var key in keys.OrderBy(k => k))
      {
        var preCount = System.Convert.ToInt32(pr[key]);

        pr[key] = count / (double)(count + (sumOfFrequencies - count - 1)) * factor;

        count += preCount;
      }

      return pr;
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
