namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.SortedDictionary<TKey, int> ToHistogram<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, int> frequencySelector, out int sumOfAllFrequencies, System.Collections.Generic.IComparer<TKey> comparer)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (frequencySelector is null) throw new System.ArgumentNullException(nameof(frequencySelector));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var histogram = new System.Collections.Generic.SortedDictionary<TKey, int>(comparer);

      var index = 0;
      sumOfAllFrequencies = 0;

      foreach (var item in source)
      {
        var key = keySelector(item, index);
        var frequency = frequencySelector(item, index);

        index++;
        sumOfAllFrequencies += frequency;

        if (histogram.TryGetValue(key, out var value))
          frequency += value;

        histogram[key] = frequency;
      }

      return histogram;
    }

    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.SortedDictionary<TKey, int> ToHistogram<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, int> frequencySelector, out int sumOfAllFrequencies)
      where TKey : notnull
      => ToHistogram(source, keySelector, frequencySelector, out sumOfAllFrequencies, System.Collections.Generic.Comparer<TKey>.Default);

    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.SortedDictionary<TKey, int> ToHistogram<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, out int sumOfAllFrequencies, System.Collections.Generic.IComparer<TKey> comparer)
      where TKey : notnull
      => ToHistogram(source, keySelector, (e, i) => 1, out sumOfAllFrequencies, comparer);

    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.SortedDictionary<TKey, int> ToHistogram<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, out int sumOfAllFrequencies)
      where TKey : notnull
      => ToHistogram(source, keySelector, (e, i) => 1, out sumOfAllFrequencies, System.Collections.Generic.Comparer<TKey>.Default);

    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.SortedDictionary<TKey, int> ToHistogram<TKey>(this System.Collections.Generic.IEnumerable<TKey> source, out int sumOfAllFrequencies, System.Collections.Generic.IComparer<TKey> comparer)
      where TKey : notnull
      => ToHistogram(source, (e, i) => e, (e, i) => 1, out sumOfAllFrequencies, comparer);

    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.SortedDictionary<TKey, int> ToHistogram<TKey>(this System.Collections.Generic.IEnumerable<TKey> source, out int sumOfAllFrequencies)
      where TKey : notnull
      => ToHistogram(source, (e, i) => e, (e, i) => 1, out sumOfAllFrequencies, System.Collections.Generic.Comparer<TKey>.Default);
  }
}
