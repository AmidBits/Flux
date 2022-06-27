//namespace Flux
//{
//  public ref partial struct SpanBuilder<T>
//  {
//    /// <summary>Locate the index of the minimum element and the index of the maximum element of the sequence. Uses the specified comparer.</summary>
//    public (int indexMinimum, int indexMaximum) GetExtremum<TValue>(System.Func<T, TValue> valueSelector, System.Collections.Generic.IComparer<TValue> comparer)
//    {
//      if (valueSelector is null) throw new System.ArgumentNullException(nameof(valueSelector));
//      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

//      var indexMin = -1;
//      var indexMax = -1;

//      for (var index = m_bufferPosition - 1; index >= 0; index--)
//      {
//        var key = valueSelector(m_buffer[index]);

//        if (indexMin < 0 || comparer.Compare(key, valueSelector(m_buffer[indexMin])) < 0)
//          indexMin = index;
//        if (indexMax < 0 || comparer.Compare(key, valueSelector(m_buffer[indexMax])) > 0)
//          indexMax = index;
//      }

//      return (indexMin, indexMax);
//    }
//    /// <summary>Locate the index of the minimum element and the index of the maximum element of the sequence. Uses the default comparer.</summary>
//    public (int indexMinimum, int indexMaximum) GetExtremum<TValue>(System.Func<T, TValue> valueSelector)
//      => GetExtremum(valueSelector, System.Collections.Generic.Comparer<TValue>.Default);
//  }
//}
