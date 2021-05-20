namespace Flux
{
  public static partial class SystemCollectionsGenericEm
  {
    public static System.Collections.Generic.IDictionary<TValue, int> Histogram<TValue>(this System.Collections.Generic.IEnumerable<TValue> source, out int sumOfAllFrequencies)
      where TValue : notnull
    {
      var histogram = new System.Collections.Generic.SortedDictionary<TValue, int>();

      sumOfAllFrequencies = 0;

      foreach (var value in source)
      {
        var frequency = 1;

        if (histogram.TryGetValue(value, out var result))
          frequency += result;

        histogram[value] = frequency;

        sumOfAllFrequencies++;
      }

      return histogram;
    }

    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary, using the specified key selector. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.IDictionary<TKey, int> Histogram<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, out int sumOfAllFrequencies, System.Collections.Generic.IComparer<TKey> comparer)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));

      var histogram = new System.Collections.Generic.SortedDictionary<TKey, int>(comparer);

      sumOfAllFrequencies = 0;

      foreach (var item in source)
      {
        var key = keySelector(item, sumOfAllFrequencies++);
        var frequency = 1;

        if (histogram.TryGetValue(key, out var value))
          frequency += value;

        histogram[key] = frequency;
      }

      return histogram;
    }
    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary, using the specified key selector. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.IDictionary<TKey, int> Histogram<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, out int sumOfAllFrequencies)
      where TKey : notnull
      => Histogram(source, keySelector, out sumOfAllFrequencies, System.Collections.Generic.Comparer<TKey>.Default);

    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.IDictionary<TKey, int> Histogram<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, int> frequencySelector, out int sumOfAllFrequencies, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (keySelector is null) throw new System.ArgumentNullException(nameof(keySelector));
      if (frequencySelector is null) throw new System.ArgumentNullException(nameof(frequencySelector));

      var histogram = new System.Collections.Generic.SortedDictionary<TKey, int>(comparer);

      var index = 0;
      sumOfAllFrequencies = 0;

      foreach (var item in source)
      {
        var key = keySelector(item, index);
        var frequency = frequencySelector(item, index++);
        sumOfAllFrequencies += frequency;

        if (histogram.TryGetValue(key, out var value))
          frequency += value;

        histogram[key] = frequency;
      }

      return histogram;
    }
    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the default comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.IDictionary<TKey, int> Histogram<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, int> frequencySelector, out int sumOfAllFrequencies)
      where TKey : notnull
      => Histogram(source, keySelector, frequencySelector, out sumOfAllFrequencies, System.Collections.Generic.Comparer<TKey>.Default);
  }
}
