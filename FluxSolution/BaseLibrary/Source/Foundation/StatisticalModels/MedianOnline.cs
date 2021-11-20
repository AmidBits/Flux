namespace Flux.Numerics
{
  /// <summary></summary>
  /// <see cref="https://stackoverflow.com/questions/10657503/find-running-median-from-a-stream-of-integers"/>
  public sealed class MedianOnline
  {
    private readonly DataStructures.IHeap<double> m_maxHeap = new DataStructures.BinaryHeapMax<double>();
    private readonly DataStructures.IHeap<double> m_minHeap = new DataStructures.BinaryHeapMin<double>();

    //public int MaxCount
    //	=> m_maxHeap.Count;
    //public int MinCount
    //	=> m_minHeap.Count;

    public void Add(double value)
    {
      if (m_minHeap.Count is var minCount && minCount == 0)
      {
        m_minHeap.Insert(value);
      }
      else if (m_maxHeap.Count is var maxCount && maxCount == 0)
      {
        m_maxHeap.Insert(value);
      }
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
    public void AddRange(params double[] values)
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
      else if (m_maxHeap.Peek() is var maxHeapPeek && maxHeapCount > minHeapCount)
        return maxHeapPeek;
      else if (m_minHeap.Peek() is var minHeapPeek && minHeapCount > maxHeapCount)
        return minHeapPeek;
      else // Counts are equal and not zero.
        return (maxHeapPeek + minHeapPeek) / 2;
    }

    public override string ToString()
      => $"{GetType().Name} {{ {EffectiveMedian()} [{m_minHeap.Count}:{m_maxHeap.Count}] }}";
  }
}
