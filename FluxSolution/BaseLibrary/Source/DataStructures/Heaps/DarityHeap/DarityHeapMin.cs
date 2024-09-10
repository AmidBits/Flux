namespace Flux.DataStructures.Heaps
{
  /// <summary>
  /// <para></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Heap_(data_structure)"/></para>
  /// </summary>
  public sealed class DarityHeapMin<TValue>
    : IHeap<TValue>, System.ICloneable, System.Collections.Generic.IReadOnlyCollection<TValue>
    where TValue : System.IComparable<TValue>
  {
    private readonly int m_arity;

    private System.Collections.Generic.List<TValue> m_data = new();

    public DarityHeapMin(int arity)
      => m_arity = arity;
    public DarityHeapMin(int arity, System.Collections.Generic.IEnumerable<TValue> collection)
      : this(arity)
    {
      foreach (var t in collection.ThrowOnNull())
        Insert(t);
    }

    private static System.Collections.Generic.IEnumerable<int> GetIndicesOfDescendantsBFS(int index, int maxIndex)
    {
      for (int baseChildIndex = (index << 1) + 1, ordinalLevel = 1; baseChildIndex <= maxIndex; baseChildIndex = (baseChildIndex << 1) + 1, ordinalLevel++)
        for (int childIndex = baseChildIndex, maxChildIndex = baseChildIndex + (1 << ordinalLevel); childIndex < maxChildIndex && maxChildIndex <= maxIndex; childIndex++)
          yield return childIndex;
    }

    private void HeapifyDown(int index)
    {
      for (var smallerIndex = index; true; index = smallerIndex)
      {
        if ((index << 1) + 1 is var childIndexL && childIndexL < m_data.Count)
        {
          if (m_data[childIndexL].CompareTo(m_data[smallerIndex]) < 0)
            smallerIndex = childIndexL;
        }
        else break;

        if (childIndexL + 1 is var childIndexR && childIndexR < m_data.Count)
          if (m_data[childIndexR].CompareTo(m_data[smallerIndex]) < 0)
            smallerIndex = childIndexR;

        if (smallerIndex == index) break;

        (m_data[smallerIndex], m_data[index]) = (m_data[index], m_data[smallerIndex]);
      }
    }
    private void HeapifyUp(int index)
    {
      while (index > 0)
      {
        var parentIndex = (index - 1) >> 1;

        if (m_data[parentIndex].CompareTo(m_data[index]) <= 0) break;

        (m_data[parentIndex], m_data[index]) = (m_data[index], m_data[parentIndex]);

        index = parentIndex;
      }
    }

    public bool IsConsistent()
    {
      if (m_data.Count == 0)
        return true;

      int m1 = 0, z = 0, p1 = 0;

      foreach (var index in GetIndicesOfDescendantsBFS(1, m_data.Count))
        switch (m_data[(index - 1) >> 1].CompareTo(m_data[index]))
        {
          case -1:
            m1++;
            break;
          case 0:
            z++;
            break;
          case 1:
            p1++;
            break;
        }

      return (m1 == 0 || p1 == 0);
    }

    // IDarityHeap<T>
    public int Count => m_data.Count;
    public bool IsEmpty => m_data.Count == 0;
    public void Clear() => m_data.Clear();
    public bool Contains(TValue item) => m_data.Contains(item);
    public TValue Extract()
    {
      var min = m_data[0];

      m_data[0] = m_data[^1];
      m_data.RemoveAt(m_data.Count - 1);

      HeapifyDown(0);

      return min;
    }
    public void Insert(TValue item)
    {
      m_data ??= new();

      m_data.Add(item);

      HeapifyUp(m_data.Count - 1);
    }
    public TValue Peek() => m_data[0];

    // ICloneable
    public object Clone() => new DarityHeapMin<TValue>(m_arity, m_data);

    // IReadOnlyCollection<T>
    public System.Collections.Generic.IEnumerator<TValue> GetEnumerator() => ((IHeap<TValue>)Clone()).ExtractAll().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => $"{GetType().Name} {{ Arity = {m_arity}, Count = {m_data.Count} }}";
  }
}
