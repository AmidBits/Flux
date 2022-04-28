using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    // http://www.greenteapress.com/thinkstats/thinkstats.pdf

    /// <summary>Returns the rank of the keys in a histogram as a percentage of the histogram.</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, double> PercentRank<TKey>(this System.Collections.Generic.IDictionary<TKey, int> source, int frequencySum, double factor = 1)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var pr = new System.Collections.Generic.SortedDictionary<TKey, double>();

      var count = 0;

      foreach (var item in source.OrderBy(kvp => kvp.Key))
      {
        pr.Add(item.Key, (double)count / (count + (frequencySum - count - 1)) * factor);

        count += item.Value;
      }

      return pr;
    }

  }
}
