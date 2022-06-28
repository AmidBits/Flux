//namespace Flux
//{
//  public ref partial struct SpanBuilder<T>
//  {
//    /// <summary>Creates a new list from the specified array from the specified offset and count.</summary>
//    public System.Collections.Generic.List<T> ToList(int offset, int count)
//    {
//      if (offset < 0 || offset >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(offset));
//      if (count < 0 || offset + count >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(count));

//      var target = new System.Collections.Generic.List<T>(count);
//      while (count-- > 0)
//        target.Add(m_buffer[offset++]);
//      return target;
//    }
//    /// <summary>Creates a new list from the specified array from the specified offset to the end.</summary>
//    public System.Collections.Generic.List<T> ToList(int offset)
//      => ToList(offset, m_bufferPosition - offset);
//    /// <summary>Creates a new list from the specified span builder.</summary>
//    public System.Collections.Generic.List<T> ToList()
//      => ToList(0, m_bufferPosition);
//  }
//}
