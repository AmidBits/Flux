namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Computes a frequency histogram from the elements in the sequence into a dictionary. This version can be used for semi-aggregate data sources, hence the frequency selector. Uses the specified comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <see cref="https://en.wikipedia.org/wiki/Histogram"/>
    public static DataStructures.Histogram<TKey, TFrequency> ToHistogram<TSource, TKey, TFrequency>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TKey> keySelector, System.Func<TSource, TFrequency> frequencySelector)
      where TKey : notnull
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
      => DataStructures.Histogram<TKey, TFrequency>.Create(source, keySelector, frequencySelector);
  }

  namespace DataStructures
  {
    public class Histogram<TKey, TFrequency>
      : System.Collections.Generic.IReadOnlyDictionary<TKey, TFrequency>
      where TKey : notnull
      where TFrequency : System.Numerics.IBinaryInteger<TFrequency>
    {
      private System.Collections.Generic.SortedDictionary<TKey, TFrequency> m_data = new System.Collections.Generic.SortedDictionary<TKey, TFrequency>();

      private TFrequency m_totalFrequency = TFrequency.Zero;

      private Histogram() { }

      public TFrequency TotalFrequency => m_totalFrequency;

      #region Static methods.
      public static Histogram<TKey, TFrequency> Create<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Func<TSource, TKey> keySelector, System.Func<TSource, TFrequency> frequencySelector)
      {
        var histogram = new Histogram<TKey, TFrequency>();

        foreach (var item in collection.ThrowIfNull())
        {
          var key = keySelector(item);
          var frequency = frequencySelector(item);

          histogram.m_totalFrequency += frequency;

          if (histogram.TryGetValue(key, out var storedFrequency))
            frequency += storedFrequency;

          histogram.m_data[key] = frequency;
        }

        return histogram;
      }
      #endregion Static methods.

      #region Implemented interfaces.
      public TFrequency this[TKey key] => m_data[key];

      public System.Collections.Generic.IEnumerable<TKey> Keys => m_data.Keys;

      public System.Collections.Generic.IEnumerable<TFrequency> Values => m_data.Values;

      public int Count => m_data.Count;

      public bool ContainsKey(TKey key) => m_data.ContainsKey(key);

      public System.Collections.Generic.IEnumerator<KeyValuePair<TKey, TFrequency>> GetEnumerator() => m_data.GetEnumerator();

      public bool TryGetValue(TKey key, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out TFrequency value) => m_data.TryGetValue(key, out value);

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
      #endregion Implemented interfaces.
    }
  }
}
