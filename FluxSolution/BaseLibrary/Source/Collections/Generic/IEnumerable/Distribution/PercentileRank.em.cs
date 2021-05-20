using System.Linq;

namespace Flux
{
  public static partial class SystemCollectionsGenericEm
  {
    // https://en.wikipedia.org/wiki/Percentile_rank

    /// <summary>Computes the percentile rank of the specified value within the source distribution. The percentile rank of a score is the percentage of scores in its frequency distribution that are equal to or lower than it. Uses the specified comparer.</summary>
    public static double PercentileRank<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Collections.Generic.IComparer<T> comparer)
      => 100.0 * CumulativeMassFunction(source, value, comparer);
    /// <summary>Computes the percentile rank of the specified value within the source distribution. The percentile rank of a score is the percentage of scores in its frequency distribution that are equal to or lower than it. Uses the default comparer.</summary>
    public static double PercentileRank<T>(this System.Collections.Generic.IEnumerable<T> source, T value)
      => PercentileRank(source, value, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Computes the percentile ranks of all values in the source distribution. The percentile rank of a score is the percentage of scores in its frequency distribution that are equal to or lower than it.</summary>
    public static System.Collections.Generic.IDictionary<TValue, double> PercentileRank<TValue>(this System.Collections.Generic.IDictionary<TValue, int> source, int sumOfAllFrequencies)
      where TValue : notnull
      => CumulativeMassFunction(source, sumOfAllFrequencies, 100);

    ///// <summary>The percentile rank (PR), is the function that maps values to their percentile rank in a distribution.</summary>
    ///// <param name="source">A sequence of System.Collections.Generic.KeyValuePair<TKey, TSource>.</param>
    ///// <param name="frequencySelector">Selector of the frequency for each <typeparam name="TSource"/> in <paramref name="source"/>.</param>
    ///// <returns>A list of PR values corresponding to the <paramref name="source"/>.</returns>
    ///// <seealso cref="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/>
    //public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, int>> PercentileRank<TKey, TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, int> frequencySelector)
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));
    //  if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
    //  if (frequencySelector is null) throw new System.ArgumentNullException(nameof(frequencySelector));

    //  var pr = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<TKey, int>>();

    //  var sumOfFrequencies = 0;

    //  var index = 0;

    //  foreach (var kvp in source)
    //  {
    //    sumOfFrequencies += frequencySelector(kvp, index);
    //    pr.Add(new System.Collections.Generic.KeyValuePair<TKey, int>(keySelector(kvp, index), 100 * sumOfFrequencies));
    //    index++;
    //  }

    //  while (--index >= 0)
    //  {
    //    var kvp = pr[index];
    //    pr[index] = new System.Collections.Generic.KeyValuePair<TKey, int>(kvp.Key, System.Math.Min(100, System.Convert.ToInt32(System.Math.Ceiling((double)kvp.Value / (double)sumOfFrequencies))));
    //  }

    //  return pr;
    //}

    ///// <summary>The percentile rank (PR), is the function that maps values to their percentile rank in a distribution.</summary>
    ///// <param name="source">A sequence of <typeparamref name="TSource"/>.</param>
    ///// <param name="frequencySelector">Selector of the frequency for each <typeparam name="TSource"/> in <paramref name="source"/>.</param>
    ///// <returns>A list of PR values corresponding to the <paramref name="source"/>.</returns>
    ///// <seealso cref="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/>
    //public static System.Collections.Generic.IList<int> PercentileRank<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, int> frequencySelector)
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));
    //  if (frequencySelector is null) throw new System.ArgumentNullException(nameof(frequencySelector));

    //  var pr = new System.Collections.Generic.List<int>();

    //  var sumOfFrequencies = 0;

    //  var index = 0;

    //  foreach (var item in source)
    //  {
    //    sumOfFrequencies += frequencySelector(item, index++);
    //    pr.Add(100 * sumOfFrequencies);
    //  }

    //  while (--index >= 0)
    //    pr[index] = System.Math.Min(100, System.Convert.ToInt32(System.Math.Ceiling((double)pr[index] / (double)sumOfFrequencies)));

    //  return pr;
    //}
  }
}
