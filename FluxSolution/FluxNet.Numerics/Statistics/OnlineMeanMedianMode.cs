//namespace Flux.Statistics
//{
//  /// <summary>Online mean, median and mode is fairly memory heavy because of the two max/min binary heaps for median and the histogram for mode.</summary>
//  /// <see href="https://stackoverflow.com/questions/10657503/find-running-median-from-a-stream-of-integers"/>
//  public class OnlineMeanMedianMode<TSelf>
//    where TSelf : System.Numerics.INumber<TSelf>
//  {
//    private readonly DataStructures.Heaps.BinaryHeapMax<TSelf> m_maxHeap = new();
//    private readonly DataStructures.Heaps.BinaryHeapMin<TSelf> m_minHeap = new();

//    private int m_count;
//    private TSelf m_sum = TSelf.Zero;

//    private readonly DataStructures.Histogram<TSelf, int> m_histogram = new();

//    public OnlineMeanMedianMode(System.Collections.Generic.IEnumerable<TSelf> values) => AddRange(values);

//    public int Count => m_count;

//    public double Mean => m_count == 0 ? m_count : double.CreateChecked(m_sum) / m_count;
//    public double Median => EffectiveMedian();
//    public System.Collections.Generic.KeyValuePair<TSelf, int> Mode => ModeHistogram.First();

//    public System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TSelf, int>> ModeHistogram => m_histogram.OrderByDescending(kvp => kvp.Value);

//    public TSelf Sum => m_sum;

//    public void Add(TSelf value)
//    {
//      m_count++;
//      m_sum += value;

//      m_histogram.Add(value, 1);

//      if (m_minHeap.Count is var minCount && minCount == 0)
//        m_minHeap.Insert(value);
//      else if (m_maxHeap.Count is var maxCount && maxCount == 0)
//        m_maxHeap.Insert(value);
//      else if (m_maxHeap.Peek() is var maxRoot && value < maxRoot)
//      {
//        m_maxHeap.Insert(value);

//        if (maxCount - minCount > 1)
//        {
//          m_minHeap.Insert(maxRoot);
//          m_maxHeap.Extract();
//        }
//      }
//      else
//      {
//        m_minHeap.Insert(value);

//        if (minCount - maxCount > 1)
//        {
//          m_maxHeap.Insert(m_minHeap.Peek());
//          m_minHeap.Extract();
//        }
//      }
//    }

//    public void AddRange(System.Collections.Generic.IEnumerable<TSelf> values)
//    {
//      foreach (var value in values)
//        Add(value);
//    }

//    public double EffectiveMedian()
//    {
//      var maxHeapCount = m_maxHeap.Count;
//      var minHeapCount = m_minHeap.Count;

//      if (maxHeapCount == 0 && minHeapCount == 0)
//        return 0;
//      else if (m_maxHeap.Peek() is var maxHeapPeek && maxHeapCount > minHeapCount) // Favor MaxHeap.
//        return double.CreateChecked(maxHeapPeek);
//      else if (m_minHeap.Peek() is var minHeapPeek && minHeapCount > maxHeapCount) // Favor MinHeap.
//        return double.CreateChecked(minHeapPeek);
//      else // Counts are equal and not zero. It's a tie with numbers available.
//        return double.CreateChecked(maxHeapPeek + minHeapPeek) / 2.0;
//    }

//    public override string ToString() => EffectiveMedian().ToString();
//  }
//}
