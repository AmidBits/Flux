namespace Flux
{
#if NET7_0_OR_GREATER
  /// <summary></summary>
  /// <see href="https://stackoverflow.com/questions/10657503/find-running-median-from-a-stream-of-integers"/>
  public sealed record class MedianOnline<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    private readonly DataStructures.IHeap<TSelf> m_maxHeap = new DataStructures.BinaryHeapMax<TSelf>();
    private readonly DataStructures.IHeap<TSelf> m_minHeap = new DataStructures.BinaryHeapMin<TSelf>();

    public void Add(TSelf value)
    {
      if (m_minHeap.Count is var minCount && minCount == 0)
        m_minHeap.Insert(value);
      else if (m_maxHeap.Count is var maxCount && maxCount == 0)
        m_maxHeap.Insert(value);
      else if (m_maxHeap.Peek() is var maxRoot && value < maxRoot)
      {
        m_maxHeap.Insert(value);

        if (maxCount - minCount > 1)
        {
          m_minHeap.Insert(maxRoot);
          m_maxHeap.Extract();
        }
      }
      else
      {
        m_minHeap.Insert(value);

        if (minCount - maxCount > 1)
        {
          m_maxHeap.Insert(m_minHeap.Peek());
          m_minHeap.Extract();
        }
      }
    }

    public void AddRange(System.Collections.Generic.IEnumerable<TSelf> values)
    {
      foreach (var value in values)
        Add(value);
    }

    public double EffectiveMedian()
    {
      var maxHeapCount = m_maxHeap.Count;
      var minHeapCount = m_minHeap.Count;

      if (maxHeapCount == 0 && minHeapCount == 0)
        return 0;
      else if (m_maxHeap.Peek() is var maxHeapPeek && maxHeapCount > minHeapCount) // Favor MaxHeap.
        return double.CreateChecked(maxHeapPeek);
      else if (m_minHeap.Peek() is var minHeapPeek && minHeapCount > maxHeapCount) // Favor MinHeap.
        return double.CreateChecked(minHeapPeek);
      else // Counts are equal and not zero. It's a tie with numbers available.
        return double.CreateChecked(maxHeapPeek + minHeapPeek) / 2.0;
    }

    public override string ToString() => EffectiveMedian().ToString();
  }

#else

  /// <summary></summary>
  /// <see href="https://stackoverflow.com/questions/10657503/find-running-median-from-a-stream-of-integers"/>
  public sealed record class MedianOnline
  {
    private readonly DataStructures.IHeap<double> m_maxHeap = new DataStructures.BinaryHeapMax<double>();
    private readonly DataStructures.IHeap<double> m_minHeap = new DataStructures.BinaryHeapMin<double>();

    public void Add(double value)
    {
      if (m_minHeap.Count is var minCount && minCount == 0)
        m_minHeap.Insert(value);
      else if (m_maxHeap.Count is var maxCount && maxCount == 0)
        m_maxHeap.Insert(value);
      else if (m_maxHeap.Peek() is var maxRoot && value < maxRoot)
      {
        m_maxHeap.Insert(value);

        if (maxCount - minCount > 1)
        {
          m_minHeap.Insert(maxRoot);
          m_maxHeap.Extract();
        }
      }
      else
      {
        m_minHeap.Insert(value);

        if (minCount - maxCount > 1)
        {
          m_maxHeap.Insert(m_minHeap.Peek());
          m_minHeap.Extract();
        }
      }
    }

    public void AddRange(System.Collections.Generic.IEnumerable<double> values)
    {
      foreach (var value in values)
        Add(value);
    }

    public double EffectiveMedian()
    {
      var maxHeapCount = m_maxHeap.Count;
      var minHeapCount = m_minHeap.Count;

      if (maxHeapCount == 0 && minHeapCount == 0)
        return 0;
      else if (m_maxHeap.Peek() is var maxHeapPeek && maxHeapCount > minHeapCount) // Favor MaxHeap.
        return maxHeapPeek;
      else if (m_minHeap.Peek() is var minHeapPeek && minHeapCount > maxHeapCount) // Favor MinHeap.
        return minHeapPeek;
      else // Counts are equal and not zero. It's a tie with numbers available.
        return (maxHeapPeek + minHeapPeek) / 2;
    }

    public override string ToString() => EffectiveMedian().ToString();
  }

#endif
}
