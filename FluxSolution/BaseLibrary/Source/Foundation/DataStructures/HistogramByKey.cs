namespace Flux.DataStructures.Statistics
{
  //public class HistogramByKey<TKey>
  //{
  //  private System.Collections.Generic.IDictionary<TKey, int> m_data;
  //  private int m_frequencySum;

  //  public HistogramByKey(System.Collections.Generic.IEnumerable<TKey> collection, System.Collections.Generic.IComparer<TKey> comparer)
  //  {
  //    m_data = new System.Collections.Generic.SortedDictionary<TKey, int>(comparer);

  //    Add(collection, (e, i) => e, (e, i) => 1);

  //    m_frequencySum = 0;
  //  }
  //  public HistogramByKey(System.Collections.Generic.IEnumerable<TKey> collection)
  //    : this(collection, System.Collections.Generic.Comparer<TKey>.Default) { }
  //  public HistogramByKey()
  //    : this(System.Linq.Enumerable.Empty<TKey>()) { }

  //  public System.Collections.Generic.IReadOnlyDictionary<TKey, int> Data
  //    => (System.Collections.Generic.IReadOnlyDictionary<TKey, int>)m_data;
  //  public int FrequencySum
  //    => m_frequencySum;

  //  public void AddValues<TSource>(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, int, TKey> keySelector, System.Func<TSource, int, int> frequencySelector)
  //  {
  //    if (source is null) throw new System.ArgumentNullException(nameof(source));

  //    var index = 0;

  //    foreach (var item in source)
  //    {
  //      var key = keySelector(item, index);
  //      var frequency = frequencySelector(item, index);

  //      m_frequencySum += frequency;

  //      if (m_data.TryGetValue(key, out var value))
  //        frequency += value;

  //      m_data[key] = frequency;

  //      index++;
  //    }
  //  }
  //  public void AddValues(System.Collections.Generic.IEnumerable<TKey> source)
  //    => AddValues(source, (e, i) => e, (e, i) => 1);
  //}
}
