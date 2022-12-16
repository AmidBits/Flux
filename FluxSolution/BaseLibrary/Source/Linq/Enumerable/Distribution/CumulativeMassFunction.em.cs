using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    // http://www.greenteapress.com/thinkstats/thinkstats.pdf

    /// <summary>The CDF is the function that maps values to their percentile rank, in a probability range [0, 1], in a distribution.</summary>
    /// <remarks>For consistency, a discrete CDF should be called a cumulative mass function(CMF), but that seems just ignored.</remarks>
    public static System.Collections.Generic.SortedDictionary<TKey, double> CumulativeMassFunction<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, int> frequencySelector, double factor = 1)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var keys = new System.Collections.Generic.HashSet<TKey>();

      var cmf = new System.Collections.Generic.SortedDictionary<TKey, double>();

      var sumOfFrequencies = 0;

      foreach (var item in source)
      {
        var key = keySelector(item);

        keys.Add(key);

        var frequency = frequencySelector(item);

        sumOfFrequencies += frequency;

        cmf[key] = cmf.TryGetValue(key, out var currentFrequency) ? currentFrequency + frequency : frequency;
      }

      var cumulativeFrequencies = 0.0;

      foreach (var key in keys.OrderBy(k => k))
      {
        cumulativeFrequencies += cmf[key];

        cmf[key] = cumulativeFrequencies / sumOfFrequencies * factor;
      }

      return cmf;
    }

    /// <summary>The CDF is the function that maps values to their percentile rank, in a probability range [0, 1], in a distribution.</summary>
    /// <remarks>For consistency, a discrete CDF should be called a cumulative mass function(CMF), but that seems just ignored.</remarks>
    public static System.Collections.Generic.SortedDictionary<TKey, double> CumulativeMassFunction<TKey>(this System.Collections.Generic.IDictionary<TKey, int> source, int? sumOfFrequencies = null, double factor = 1)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var cmf = new System.Collections.Generic.SortedDictionary<TKey, double>();

      var sof = sumOfFrequencies ?? source.Values.Sum();

      var cumulativeFrequencies = 0;

      foreach (var kvp in source.OrderBy(kvp => kvp.Key))
      {
        cumulativeFrequencies += kvp.Value;

        cmf.Add(kvp.Key, cumulativeFrequencies / (double)sof * factor);
      }

      return cmf;
    }

    /// <summary>The CDF is the function that maps values to their percentil rank, in a probability range [0, 1], in a distribution. Uses the specified comparer.</summary>
    public static double CumulativeMassFunction<TValue>(this System.Collections.Generic.IEnumerable<TValue> source, TValue value, System.Collections.Generic.IComparer<TValue> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var countTotal = 0;
      var countLessOrEqual = 0;

      foreach (var item in source)
      {
        countTotal++;
        if (comparer.Compare(item, value) <= 0)
          countLessOrEqual++;
      }

      return (double)countLessOrEqual / (double)countTotal;
    }

    /// <summary>The CDF is the function that maps values to their percentil rank, in a probability range [0, 1], in a distribution. Uses the default comparer.</summary>
    public static double CumulativeMassFunction<TValue>(this System.Collections.Generic.IEnumerable<TValue> source, TValue value)
      => CumulativeMassFunction(source, value, System.Collections.Generic.Comparer<TValue>.Default);
  }
}
