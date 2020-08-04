namespace Flux
{
  public static partial class XtensionsCollections
  {
    public static System.Collections.Generic.IDictionary<int, int> AsHistogram(this System.Collections.Generic.IEnumerable<int> source)
      => source.ToSortedDictionary((e, i) => i, (e, i) => e);

    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.IDictionary<TKey, int> Histogram<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
      => source.Histogram(keySelector, (e, i) => 1, comparer);

    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static System.Collections.Generic.SortedDictionary<TKey, int> Histogram<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, int> countSelector, System.Collections.Generic.IComparer<TKey>? comparer = null)
      where TKey : notnull
    {
      var histogram = new System.Collections.Generic.SortedDictionary<TKey, int>(comparer);

      var index = 0;
      foreach (var item in source)
      {
        var key = keySelector(item, index);
        var count = countSelector(item, index++);

        if (histogram.ContainsKey(key)) histogram[key] += count;
        else histogram.Add(key, count);
      }

      return histogram;
    }

    /// <summary>Generates a histogram.</summary>
    /// <param name="source">The sequence of objects from which to build a histogram.</param>
    /// <param name="binSelector">Which bin to add the extracted frequency to.</param>
    /// <param name="frequencySelector">Extract the frequency for this <typeparamref name="TSource"/>.</param>
    /// <returns></returns>
    public static System.Collections.Generic.IList<int> Histogram<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, int> binSelector, System.Func<TSource, int, int> frequencySelector, out int totalFrequency)
    {
      totalFrequency = 0;

      var histogram = new System.Collections.Generic.List<int>();

      var index = 0;

      foreach (var item in source)
      {
        var bin = binSelector(item, index);
        var frequency = frequencySelector(item, index);

        totalFrequency += frequency;

        while (histogram.Count <= bin)
        {
          histogram.Add(0);
        }

        histogram[bin] += frequency;

        index++;
      }

      return histogram;
    }
  }
}
