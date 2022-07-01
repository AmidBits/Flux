namespace Flux.DataStructures
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Heap_(data_structure)"/>
  public sealed class BinaryHeapMax<T>
    : IHeap<T>, System.ICloneable
    where T : System.IComparable<T>
  {
    private readonly System.Collections.Generic.List<T> m_data = new();

    public BinaryHeapMax() { }
    public BinaryHeapMax(System.Collections.Generic.IEnumerable<T> collection)
    {
      if (collection is null) throw new System.ArgumentNullException(nameof(collection));

      foreach (var item in collection)
        Insert(item);
    }

    // IHeap<T>
    public int Count
      => m_data.Count;
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
    public bool IsEmpty
      => m_data.Count == 0;
    public T Peek()
      => m_data[0];

    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
      => ((IHeap<T>)Clone()).ExtractAll().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

    // IClonable<T>
    public object Clone()
      => new BinaryHeapMax<T>(m_data);

    public System.Collections.Generic.IEnumerable<int> GetIndicesOfDescendantsBFS(int index, int maxIndex)
    {
      for (int baseParentIndex = (index << 1) + 1, ordinalLevel = 1; baseParentIndex <= maxIndex; baseParentIndex = (baseParentIndex << 1) + 1, ordinalLevel++)
      {
        for (int childIndex = baseParentIndex, maxChildIndex = baseParentIndex + (1 << ordinalLevel); childIndex < maxChildIndex && maxChildIndex <= maxIndex; childIndex++)
        {
          yield return childIndex;
        }
      }
    }

    public bool IsConsistent()
    {
      if (m_data.Count == 0)
        return true;

      int m1 = 0, z = 0, p1 = 0;

      foreach (var index in GetIndicesOfDescendantsBFS(1, m_data.Count))
      {
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
      }

      return m1 == 0 || p1 == 0;
    }

    private void HeapifyDown(int index)
    {
      for (var smallerIndex = index; true; index = smallerIndex)
      {
        if ((index << 1) + 1 is var childIndexL && childIndexL < m_data.Count)
        {
          if (m_data[childIndexL].CompareTo(m_data[smallerIndex]) > 0)
            smallerIndex = childIndexL;
        }
        else break;

        if (childIndexL + 1 is var childIndexR && childIndexR < m_data.Count)
          if (m_data[childIndexR].CompareTo(m_data[smallerIndex]) > 0)
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

        if (m_data[parentIndex].CompareTo(m_data[index]) >= 0)
          break;

        m_data.Swap(index, parentIndex);

        index = parentIndex;
      }
    }

    public override string ToString()
      => $"{GetType().Name} {{ Count = {m_data.Count} }}";
  }
}
