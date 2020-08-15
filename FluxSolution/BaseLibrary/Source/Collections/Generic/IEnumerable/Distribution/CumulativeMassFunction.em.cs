namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>The Cumulative Distribution Function (CDF) is the function that maps values to their percentile rank in a distribution.</summary>
    /// <returns>Basically the percentile rank but as a probability in the range [0, 1], rather than a percentile rank which is in the range [0, 100].</returns>
    /// <seealso cref="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/>
    public static double CumulativeMassFunction<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, TSource value, System.Collections.Generic.IComparer<TSource>? comparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.Comparer<TSource>.Default;

      var countTotal = 0;

      var countLessOrEqual = 0;

      foreach (var item in source)
      {
        countTotal++;

        if (comparer.Compare(item, value) <= 0) countLessOrEqual++;
      }

      return (double)countLessOrEqual / (double)countTotal;
    }

    /// <summary>The cumulative mass function (CMF), more commonly known as CDF (cumulative distribution function), is the function that maps values to their percentile rank in a distribution. This version allows for a factor (divisor) to scale the results.</summary>
    /// <param name="source">A sequence of System.Collections.Generic.KeyValuePair<TKey, TSource>.</param>
    /// <param name="frequencySelector">Selector of the frequency for each <typeparam name="TSource"/> in <paramref name="source"/>.</param>
    /// <param name="factor">Scale value for the resulting percentile rank.</param>
    /// <returns>A list of CMF values corresponding to the <paramref name="source"/>.</returns>
    /// <seealso cref="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, double>> CumulativeMassFunction<TKey, TSource>(this System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TSource>> source, System.Func<TKey, TSource, int, int> frequencySelector, out int sumOfFrequencies, double factor = 1.0)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (frequencySelector is null) throw new System.ArgumentNullException(nameof(frequencySelector));

      if (factor <= 0) throw new System.ArgumentOutOfRangeException(nameof(factor));

      var cmf = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<TKey, double>>();

      sumOfFrequencies = 0;

      var index = 0;

      foreach (var kvp in source)
      {
        sumOfFrequencies += frequencySelector(kvp.Key, kvp.Value, index++);

        cmf.Add(new System.Collections.Generic.KeyValuePair<TKey, double>(kvp.Key, sumOfFrequencies));
      }

      while (--index >= 0)
      {
        var kvp = cmf[index];

        cmf[index] = new System.Collections.Generic.KeyValuePair<TKey, double>(kvp.Key, kvp.Value / sumOfFrequencies * factor);
      }

      return cmf;
    }

    /// <summary>The cumulative mass function (CMF), more commonly known as CDF (cumulative distribution function), is the function that maps values to their percentile rank in a distribution. This version allows for a factor (divisor) to scale the results.</summary>
    /// <param name="source">A sequence of <typeparamref name="TSource"/>.</param>
    /// <param name="frequencySelector">Selector of the frequency for each <typeparam name="TSource"/> in <paramref name="source"/>.</param>
    /// <param name="factor">Scale value for the resulting percentile rank.</param>
    /// <returns>A list of CMF values corresponding to the <paramref name="source"/>.</returns>
    /// <seealso cref="http://www.greenteapress.com/thinkstats/thinkstats.pdf"/>
    public static System.Collections.Generic.IList<double> CumulativeMassFunction<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, int> frequencySelector, out int sumOfFrequencies, double factor = 1.0)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (frequencySelector is null) throw new System.ArgumentNullException(nameof(frequencySelector));

      if (factor <= 0) throw new System.ArgumentOutOfRangeException(nameof(factor));

      var cmf = new System.Collections.Generic.List<double>();

      sumOfFrequencies = 0;

      var index = 0;

      foreach (var item in source)
      {
        sumOfFrequencies += frequencySelector(item, index++);

        cmf.Add(sumOfFrequencies);
      }

      while (--index >= 0)
      {
        cmf[index] = cmf[index] / sumOfFrequencies * factor;
      }

      return cmf;
    }
  }
}
