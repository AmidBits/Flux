//namespace Flux
//{
//  public ref partial struct SpanBuilder<T>
//  {
//    /// <summary>Creates a new list from the specified array from the specified offset and count.</summary>
//    public T[] ToArray(int offset, int count)
//    {
//      if (offset < 0 || offset >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(offset));
//      if (count < 0 || offset + count >= m_bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(count));

//      var target = new T[count];
//      AsReadOnlySpan().Slice(offset, count).CopyTo(target);
//      return target;
//    }
//    /// <summary>Creates a new list from the specified array from the specified offset to the end.</summary>
//    public T[] ToArray(int offset)
//      => ToArray(offset, m_bufferPosition - offset);
//    /// <summary>Creates a new list from the specified span builder.</summary>
//    public T[] ToArray()
//      => ToArray(0, m_bufferPosition);
//  }
//}
