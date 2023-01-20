namespace Flux
{
  public static partial class Enumerable
  {
    // http://www.greenteapress.com/thinkstats/thinkstats.pdf

    ///// <summary>The PMF is a function that maps from values (frequencies) to probabilities.</summary>
    ///// <exception cref="System.ArgumentNullException"/>
    //public static System.Collections.Generic.SortedDictionary<TKey, double> ProbabilityMassFunction<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, int> frequencySelector, double factor = 1)
    //  where TKey : notnull
    //{
    //  var keys = new System.Collections.Generic.HashSet<TKey>();

    //  var pmf = new System.Collections.Generic.SortedDictionary<TKey, double>();

    //  var sumOfFrequencies = 0;

    //  foreach (var item in source.ThrowIfNull())
    //  {
    //    var key = keySelector(item);

    //    keys.Add(key);

    //    var frequency = frequencySelector(item);

    //    sumOfFrequencies += frequency;

    //    pmf[key] = pmf.TryGetValue(key, out var currentFrequency) ? currentFrequency + frequency : frequency; // If this key already exist, add the frequency.
    //  }

    //  foreach (var key in keys.OrderBy(k => k))
    //    pmf[key] = pmf[key] / sumOfFrequencies * factor;

    //  return pmf;
    //}

    ///// <summary>The PMF is a function that maps from values (frequencies) to probabilities.</summary>
    ///// <exception cref="System.ArgumentNullException"/>
    //public static System.Collections.Generic.SortedDictionary<TKey, double> ProbabilityMassFunction<TKey>(this System.Collections.Generic.IDictionary<TKey, int> source, int? sumOfFrequencies = null, double factor = 1)
    //  where TKey : notnull
    //{
    //  var pmf = new System.Collections.Generic.SortedDictionary<TKey, double>();

    //  var sof = sumOfFrequencies ?? source.Values.Sum();

    //  foreach (var kvp in source.ThrowIfNull())
    //    pmf.Add(kvp.Key, kvp.Value / (double)sof * factor);

    //  return pmf;
    //}

    ///// <summary>The PMF is a function that maps from values to probabilities. Uses the specified comparer.</summary>
    ///// <exception cref="System.ArgumentNullException"/>
    //public static double ProbabilityMassFunction<TValue>(this System.Collections.Generic.IEnumerable<TValue> source, TValue value, System.Collections.Generic.IComparer<TValue>? comparer = null)
    //{
    //  comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

    //  var countTotal = 0;
    //  var countLessOrEqual = 0;

    //  foreach (var item in source.ThrowIfNull())
    //  {
    //    countTotal++;
    //    if (comparer.Compare(item, value) <= 0)
    //      countLessOrEqual++;
    //  }

    //  return (double)countLessOrEqual / (double)countTotal;
    //}

    /// <summary>The PMF is the function that maps from values to probabilities, each in the probability range [0, 1].</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, TFactor> ToProbabilityMassFunction<TKey, TFrequency, TFactor>(this System.Collections.Generic.SortedDictionary<TKey, TFrequency> source, TFactor factor, TFrequency sumOfAllFrequencies)
      where TKey : notnull
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
      where TFactor : System.Numerics.IFloatingPoint<TFactor>
    {
      var pmf = new System.Collections.Generic.SortedDictionary<TKey, TFactor>(source.Comparer);

      foreach (var key in source.Keys)
        pmf[key] = TFactor.CreateChecked(source[key]) / TFactor.CreateChecked(sumOfAllFrequencies) * factor;

      return pmf;
    }

    /// <summary>The PMF is the function that maps from values to probabilities, each in the probability range [0, 1].</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, TFactor> ToProbabilityMassFunction<TKey, TFrequency, TFactor>(this System.Collections.Generic.SortedDictionary<TKey, TFrequency> source, TFactor factor)
      where TKey : notnull
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
      where TFactor : System.Numerics.IFloatingPoint<TFactor>
      => ToProbabilityMassFunction(source, factor, source.Values.Sum());

    /// <summary>The PMF is the function that maps from values to probabilities, each in the probability range [0, 1].</summary>
    public static System.Collections.Generic.SortedDictionary<TKey, TFactor> ToProbabilityMassFunction<TSource, TKey, TFrequency, TFactor>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TFrequency> frequencySelector, TFactor factor, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
      where TFactor : System.Numerics.IFloatingPoint<TFactor>
    {
      var histogram = ToHistogram(source, keySelector, frequencySelector, out var sumOfAllFrequencies, comparer);

      return ToProbabilityMassFunction(histogram, factor, sumOfAllFrequencies);
    }

    /// <summary>The PMF is a function that maps from values to probabilities. Uses the specified comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static TFactor ToProbabilityMassFunction<TSource, TValue, TFactor>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, TValue value, TFactor factor)
      where TValue : System.Numerics.INumber<TValue>
      where TFactor : System.Numerics.IFloatingPoint<TFactor>
    {
      var countTotal = TFactor.Zero;
      var countLessOrEqual = TFactor.Zero;

      foreach (var item in source.ThrowIfNull())
      {
        countTotal++;
        if (valueSelector(item) <= value)
          countLessOrEqual++;
      }

      return countLessOrEqual / countTotal * factor;
    }
  }
}
