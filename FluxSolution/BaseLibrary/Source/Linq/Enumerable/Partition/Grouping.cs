namespace Flux
{
  /// <summary>Class used to group adjacent elements in a sequence. Derives from System.Linq.IGrouping<TKey, TSource>.</summary>
  public sealed class Grouping<TKey, TElement>
    : System.Linq.IGrouping<TKey, TElement>
    , System.Collections.Generic.IList<TElement>
    , System.Collections.Generic.IEnumerable<TElement>
    where TKey : notnull
  {
    private readonly TKey m_key;
    private readonly System.Collections.Generic.List<TElement> m_elements = new();

    public Grouping(TKey key) => m_key = key;
    public Grouping(TKey key, TElement source) : this(key) => m_elements.Add(source);

    #region Implemented interfaces

    // IGrouping
    public TKey Key => m_key;

    // IList
    public TElement this[int index] { get => m_elements[index]; set => m_elements[index] = value; }
    public void Add(TElement item) => m_elements.Add(item);
    public void Clear() => m_elements.Clear();
    public bool Contains(TElement item) => m_elements.Contains(item);
    public void CopyTo(TElement[] array, int arrayIndex) => m_elements.CopyTo(array, arrayIndex);
    public int Count => m_elements.Count;
    public int IndexOf(TElement item) => m_elements.IndexOf(item);
    public void Insert(int index, TElement item) => m_elements.Insert(index, item);
    public bool IsReadOnly => false;
    public bool Remove(TElement item) => m_elements.Remove(item);
    public void RemoveAt(int index) => m_elements.RemoveAt(index);

    // IEnumerable
    public System.Collections.Generic.IEnumerator<TElement> GetEnumerator() => m_elements.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => m_elements.GetEnumerator();

    #endregion // Implemented interfaces
  }
}
