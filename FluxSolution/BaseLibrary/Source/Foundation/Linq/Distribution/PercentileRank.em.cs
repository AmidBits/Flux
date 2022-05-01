namespace Flux
{
  public static partial class Enumerable
  {
    // https://en.wikipedia.org/wiki/Percentile_rank

    public enum Variant
    {
      Matlab,
      PercentileInc,
      PercentileExc,
    }

    /// <summary>Matlab percent rank.</summary>
    //public static double PercentRankMatlab(int index, int count)
    //  => 100.0 / count * (index - 0.5) / 100.0;
    /// <summary>Excel percent rank (inc). Noted as an alternative by NIST.</summary>
    public static double PercentRankInc(this double percentile, int count)
      => percentile * (count - 1) + 1;
    /// <summary>Excel percent rank (exc). Primary variant recommended by NIST.</summary>
    public static double PercentRankExc(this double percentile, int count)
      => percentile * (count + 1);

    /// <summary>Matlab percentile value (prctile).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Percentile#First_variant,_C_=_1/2"/>
    //public static System.Collections.Generic.SortedDictionary<TKey, double> PercentileValueMatlab<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, int> frequencySelector)
    //  where TKey : notnull
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

    //  var keys = new System.Collections.Generic.HashSet<TKey>();

    //  var pr1 = new System.Collections.Generic.SortedDictionary<TKey, double>();

    //  var sumOfFrequencies = 0;

    //  foreach (var item in source)
    //  {
    //    var key = keySelector(item);

    //    keys.Add(key);

    //    var frequency = frequencySelector(item);

    //    sumOfFrequencies += frequency;

    //    pr1[key] = pr1.TryGetValue(key, out var currentFrequency) ? currentFrequency + frequency : frequency;
    //  }

    //  var index = 1;

    //  foreach (var key in keys.OrderBy(k => k))
    //  {
    //    pr1[key] = PercentRankMatlab(index, sumOfFrequencies);

    //    index++;
    //  }

    //  return pr1;
    //}

    /// <summary>Excel percentile value (inc). Noted as an alternative by NIST.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Percentile#Second_variant,_C_=_1"/>
    public static double PercentileValueInc(this System.Collections.Generic.IEnumerable<double> source, double percentile)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (percentile < 0 || percentile > 1) throw new System.ArgumentOutOfRangeException(nameof(percentile));

      var x = PercentRankInc(percentile, source.Count());
      var m = x % 1;

      var i = System.Convert.ToInt32(System.Math.Floor(x));

      var v3 = source.ElementAt(System.Math.Clamp(i, 0, source.Count() - 1));
      var v2 = source.ElementAt(System.Math.Clamp(i - 1, 0, source.Count() - 1));

      return v2 + m * (v3 - v2);
    }

    /// <summary>Excel percentile value (exc). The primary variant recommended by NIST.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Percentile#Third_variant,_C_=_0"/>
    public static double PercentileValueExc(this System.Collections.Generic.IEnumerable<double> source, double percentile)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (percentile < 0 || percentile > 1) throw new System.ArgumentOutOfRangeException(nameof(percentile));

      var count = source.Count();

      var x = PercentRankExc(percentile, count);
      var m = x % 1;

      var i = System.Convert.ToInt32(System.Math.Floor(x));

      var v3 = source.ElementAt(System.Math.Clamp(i, 0, count - 1));
      var v2 = source.ElementAt(System.Math.Clamp(i - 1, 0, count - 1));

      return v2 + m * (v3 - v2);
    }

    //public static System.Collections.Generic.IEnumerable<(double percentile, double percentileValue)> PercentRanksV3<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, int> frequencySelector)
    //  where TKey : notnull
    //{
    //  if (source is null) throw new System.ArgumentNullException(nameof(source));

    //  var keys = new System.Collections.Generic.HashSet<TKey>();

    //  var pr1 = new System.Collections.Generic.SortedDictionary<TKey, double>();

    //  var sumOfFrequencies = 0;

    //  foreach (var item in source)
    //  {
    //    var key = keySelector(item);

    //    keys.Add(key);

    //    var frequency = frequencySelector(item);

    //    sumOfFrequencies += frequency;

    //    pr1[key] = pr1.TryGetValue(key, out var currentFrequency) ? currentFrequency + frequency : frequency;
    //  }

    //  var index = 1;

    //  foreach (var percentile in System.Linq.Enumerable.Range(0, 101))
    //  {
    //    var x = PercentRankExc(percentile / 100.0, sumOfFrequencies);
    //    //if (x < 1)
    //    //{
    //    //  x = 1;
    //    //}
    //    //else if (x > pr1.Count)
    //    //{
    //    //  x = pr1.Count;
    //    //}
    //    var i3 = System.Convert.ToInt32(System.Math.Floor(x));
    //    var i2 = i3 - 1;
    //    i3 = System.Math.Clamp(i3, 0, 4);
    //    i2 = System.Math.Clamp(i2, 0, 4);
    //    //if (i3 <= 0) i3++;
    //    //var i2 = i3 - 1;
    //    //if (i3 >= 5) i3 = 4;
    //    //if (i2 >= 5) i2 = 4;

    //    var v3 = System.Convert.ToInt32(pr1.ElementAt(i3).Key.ToString());
    //    var v2 = System.Convert.ToInt32(pr1.ElementAt(i2).Key.ToString());
    //    var m = x % 1;

    //    yield return (percentile, v2 + m * (v3 - v2));

    //    //index++;
    //  }
    //}

    /// <summary>Computes the percentile rank of the specified value within the source distribution. The percentile rank of a score is the percentage of scores in its frequency distribution that are equal to or lower than it. Uses the specified comparer.</summary>
    public static double PercentileRank<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Collections.Generic.IComparer<T> comparer)
      => 100.0 * CumulativeMassFunction(source, value, comparer);
    /// <summary>Computes the percentile rank of the specified value within the source distribution. The percentile rank of a score is the percentage of scores in its frequency distribution that are equal to or lower than it. Uses the default comparer.</summary>
    public static double PercentileRank<T>(this System.Collections.Generic.IEnumerable<T> source, T value)
      => PercentileRank(source, value, System.Collections.Generic.Comparer<T>.Default);

    /// <summary>Computes the percentile ranks of all values in the source distribution. The percentile rank of a score is the percentage of scores in its frequency distribution that are equal to or lower than it.</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, double> PercentileRank<TKey>(this System.Collections.Generic.IDictionary<TKey, int> source, int sumOfAllFrequencies)
      where TKey : notnull
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
