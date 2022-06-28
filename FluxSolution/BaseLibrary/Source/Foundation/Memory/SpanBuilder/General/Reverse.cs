//namespace Flux
//{
//  public ref partial struct SpanBuilder<T>
//  {
//    /// <summary>Reverse all characters in the range [startIndex, endIndex] within the span builder.</summary>
//    public void Reverse(int startIndex, int endIndex)
//    {
//      if (startIndex < 0 || startIndex >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
//      if (endIndex < startIndex || endIndex >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

//      while (startIndex < endIndex)
//        SwapImpl(startIndex++, endIndex--);
//    }
//    /// <summary>Reverse all characters within the span builder.</summary>
//    public void Reverse()
//      => Reverse(0, m_bufferPosition - 1);
//  }
//}
