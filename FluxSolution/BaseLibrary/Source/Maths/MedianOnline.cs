namespace Flux
{
  /// <summary></summary>
  /// <see cref="https://stackoverflow.com/questions/10657503/find-running-median-from-a-stream-of-integers"/>
  public class MedianOnline
  {
    private Flux.Collections.Generic.IHeapMax<double> m_maxHeap = new Flux.Collections.Generic.BinaryHeapMax<double>();
    private Flux.Collections.Generic.IHeapMin<double> m_minHeap = new Flux.Collections.Generic.BinaryHeapMin<double>();

    public int MaxCount => m_maxHeap.Count;
    public int MinCount => m_minHeap.Count;

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
          m_maxHeap.ExtractMax();
        }
      }
      else
      {
        m_minHeap.Insert(value);

        if (minCount - maxCount > 1)
        {
          m_maxHeap.Insert(m_minHeap.Peek());
          m_minHeap.ExtractMin();
        }
      }
    }
    public void AddRange(params double[] values)
    {
      foreach (var value in values)
      {
        Add(value);
      }
    }

    public double EffectiveMedian()
      => m_maxHeap.Count > m_minHeap.Count ? m_maxHeap.Peek() : m_minHeap.Count > m_maxHeap.Count ? m_minHeap.Peek() : ((m_maxHeap?.Peek() ?? 0) + (m_minHeap?.Peek() ?? 0)) / 2;

    public override string ToString()
      => $"<{EffectiveMedian()} [{m_minHeap.Count}:{m_maxHeap.Count}]>";
  }
}
