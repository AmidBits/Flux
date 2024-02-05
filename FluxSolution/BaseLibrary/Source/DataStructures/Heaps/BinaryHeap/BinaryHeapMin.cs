namespace Flux.DataStructures
{
  /// <summary>
  /// <para></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Heap_(data_structure)"/></para>
  /// </summary>
  public sealed class BinaryHeapMin<T>
    : IHeap<T>, System.ICloneable, System.Collections.Generic.IReadOnlyCollection<T>
    where T : System.IComparable<T>
  {
    private readonly System.Collections.Generic.List<T> m_data = new();

    public BinaryHeapMin() { }
    public BinaryHeapMin(System.Collections.Generic.IEnumerable<T> collection)
    {
      System.ArgumentNullException.ThrowIfNull(collection);

      foreach (var item in collection)
        Insert(item);
    }

    public System.Collections.Generic.IEnumerable<int> GetIndicesOfDescendantsBFS(int index, int maxIndex)
    {
      for (int baseParentIndex = (index << 1) + 1, ordinalLevel = 1; baseParentIndex <= maxIndex; baseParentIndex = (baseParentIndex << 1) + 1, ordinalLevel++)
        for (int childIndex = baseParentIndex, maxChildIndex = baseParentIndex + (1 << ordinalLevel); childIndex < maxChildIndex && maxChildIndex <= maxIndex; childIndex++)
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

        if (smallerIndex == index)
          break;

        m_data.Swap(index, smallerIndex);
      }
    }
    private void HeapifyUp(int index)
    {
      while (index > 0)
      {
        var parentIndex = (index - 1) >> 1;

        if (m_data[parentIndex].CompareTo(m_data[index]) <= 0)
          break;

        m_data.Swap(index, parentIndex);

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

      return m1 == 0 || p1 == 0;
    }

    // IHeap<T>
    public bool IsEmpty => m_data.Count == 0;
    public T Extract()
    {
      var min = m_data[0];

      m_data[0] = m_data[^1];
      m_data.RemoveAt(m_data.Count - 1);

      HeapifyDown(0);

      return min;
    }
    public void Insert(T item)
    {
      m_data.Add(item);

      HeapifyUp(m_data.Count - 1);
    }
    public T Peek() => m_data[0];

    // IClonable<T>
    public object Clone() => new BinaryHeapMin<T>(m_data);

    // IReadOnlyCollection<>
    public int Count => m_data.Count;
    public System.Collections.Generic.IEnumerator<T> GetEnumerator() => ((IHeap<T>)Clone()).ExtractAll().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => $"{GetType().Name} {{ Count = {m_data.Count} }}";
  }
}
