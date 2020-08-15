using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>The Cumulative Distribution Function (CDF) is the function that maps values to their percentile rank in a distribution.</summary>
    /// <returns>Basically the percentile rank but as a probability in the range [0, 1], rather than a percentile rank which is in the range [0, 100].</returns>
    /// <seealso cref="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/>
    public static double ProbabilityMassFunction<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, TSource value, out int countLessOrEqual, out int countTotal, System.Collections.Generic.IComparer<TSource>? comparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.Comparer<TSource>.Default;

      countLessOrEqual = 0;
      countTotal = 0;

      foreach (var item in source)
      {
        if (comparer.Compare(item, value) <= 0) countLessOrEqual++;

        countTotal++;
      }

      return (double)countLessOrEqual / (double)countTotal;
    }

    /// <summary>The probability mass function (PMF), is the function that maps values to their proportional rank in a distribution. This version allows for a factor (divisor) to scale the results.</summary>
    /// <typeparam name="TKey">The key associated with each bin.</typeparam>
    /// <typeparam name="TSource">Source object for each frequency bin.</typeparam>
    /// <param name="source">The sequence of <typeparam name="TSource"/> objects to apply PMF to.</param>
    /// <param name="frequencySelector">Selector of the frequency for each <typeparam name="TSource"/> in <paramref name="source"/>.</param>
    /// <param name="factor">Scale value for the resulting percentile rank.</param>
    /// <returns>A sequence of key-value-pair where the value is the PMF corresponding to the key in the <paramref name="source"/>.</returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, double>> ProbabilityMassFunction<TKey, TSource>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TSource>> source, System.Func<TKey, TSource, int, int> frequencySelector, out int sumOfFrequencies, double factor = 1.0)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (frequencySelector is null) throw new System.ArgumentNullException(nameof(frequencySelector));

      if (factor <= 0) throw new System.ArgumentOutOfRangeException(nameof(factor));

      var pmf = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<TKey, double>>();

      sumOfFrequencies = 0;

      var index = 0;

      foreach (var kvp in source)
      {
        var count = frequencySelector(kvp.Key, kvp.Value, index++);

        sumOfFrequencies += count;

        pmf.Add(new System.Collections.Generic.KeyValuePair<TKey, double>(kvp.Key, count));
      }

      while (--index >= 0)
      {
        var kvp = pmf[index];

        pmf[index] = new System.Collections.Generic.KeyValuePair<TKey, double>(kvp.Key, kvp.Value / sumOfFrequencies * factor);
      }

      return pmf;
    }

    /// <summary>The probability mass function (PMF), is the function that maps values to their proportional rank in a distribution. This version allows for a factor (divisor) to scale the results.</summary>
    /// <typeparam name="TSource">Source object for each frequency bin.</typeparam>
    /// <param name="source">The sequence of <typeparam name="TSource"/> objects to apply PMF to.</param>
    /// <param name="frequencySelector">Selector of the frequency for each <typeparam name="TSource"/> in <paramref name="source"/>.</param>
    /// <param name="factor">Scale value for the resulting percentile rank.</param>
    /// <returns>A list of PMF values corresponding to the order of <paramref name="source"/>.</returns>
    /// <seealso cref="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/>
    public static System.Collections.Generic.IList<double> ProbabilityMassFunction<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, int> frequencySelector, out int sumOfFrequencies, double factor = 1.0)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (frequencySelector is null) throw new System.ArgumentNullException(nameof(frequencySelector));

      if (factor <= 0) throw new System.ArgumentOutOfRangeException(nameof(factor));

      var cmf = new System.Collections.Generic.List<double>();

      sumOfFrequencies = 0;

      var index = 0;

      foreach (var item in source)
      {
        var count = frequencySelector(item, index++);

        sumOfFrequencies += count;

        cmf.Add(count);
      }

      while (--index >= 0)
      {
        cmf[index] = cmf[index] / sumOfFrequencies * factor;
      }

      return cmf;
    }
  }
}
