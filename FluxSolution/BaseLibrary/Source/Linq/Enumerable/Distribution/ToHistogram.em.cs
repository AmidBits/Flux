namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.SortedDictionary<TKey, TFrequency> ToHistogram<TSource, TKey, TFrequency>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, TFrequency> frequencySelector, out TFrequency sumOfAllFrequencies, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
      where TFrequency : System.Numerics.INumber<TFrequency>
    {
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (frequencySelector is null) throw new System.ArgumentNullException(nameof(frequencySelector));

      var histogram = new System.Collections.Generic.SortedDictionary<TKey, TFrequency>(comparer ?? System.Collections.Generic.Comparer<TKey>.Default);

      var index = 0;

      sumOfAllFrequencies = TFrequency.Zero;

      foreach (var item in source.ThrowIfNull())
      {
        var frequency = frequencySelector(item, index);

        sumOfAllFrequencies += frequency;

        var key = keySelector(item, index);

        if (histogram.TryGetValue(key, out var value))
          frequency += value;

        histogram[key] = frequency;

        index++;
      }

      return histogram;
    }
  }
}
