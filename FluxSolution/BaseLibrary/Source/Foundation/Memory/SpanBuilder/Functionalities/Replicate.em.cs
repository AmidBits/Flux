namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Returns the source replicated (copied) the specified number of times.</summary>
    public T[] Replicate(int count)
    {
      var target = new T[m_bufferPosition * (count + 1)];

      for (var index = 0; index < count; index++)
        CopyTo(target, count * index);

      return target;
    }
  }
}
