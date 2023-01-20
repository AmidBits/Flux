namespace Flux
{
  public static partial class Enumerable
  {
    // http://www.greenteapress.com/thinkstats/thinkstats.pdf

    /// <summary>The CDF is the function that maps values to their percentile rank, in a probability range [0, 1], in a distribution.</summary>
    /// <remarks>For consistency, a discrete CDF should be called a cumulative mass function(CMF), but that seems just ignored.</remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.SortedDictionary<TKey, TFactor> CumulativeMassFunction<TSource, TKey, TFrequency, TFactor>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TFrequency> frequencySelector, TFactor factor)
      where TKey : notnull
      where TFrequency : System.Numerics.INumber<TFrequency>
      where TFactor : System.Numerics.IFloatingPoint<TFactor>
    {
      var histogram = new System.Collections.Generic.SortedDictionary<TKey, TFrequency>();

      var totalFrequencies = TFrequency.Zero;

      foreach (var item in source.ThrowIfNull())
      {
        var key = keySelector(item);
        var frequency = frequencySelector(item);

        totalFrequencies += frequency;

        histogram[key] = histogram.TryGetValue(key, out var currentFrequency) ? currentFrequency + frequency : frequency;
      }

      var cmf = new System.Collections.Generic.SortedDictionary<TKey, TFactor>();

      var cumulativeFrequencies = TFactor.Zero;

      foreach (var key in histogram.Keys)
      {
        cumulativeFrequencies += TFactor.CreateChecked(histogram[key]);

        cmf[key] = cumulativeFrequencies / TFactor.CreateChecked(totalFrequencies) * factor;
      }

      return cmf;
    }

    /// <summary>The CDF is the function that maps values to their percentile rank, within the probability range [0, 1], in a distribution. Uses the specified comparer.</summary>
    /// <remarks>This function maps </remarks>
    /// <exception cref="System.ArgumentNullException"/>
    public static TFactor CumulativeMassFunction<TSource, TValue, TFactor>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, TValue value, TFactor factor)
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
