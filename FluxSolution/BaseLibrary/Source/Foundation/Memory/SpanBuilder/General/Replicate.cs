namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Returns the source replicated (copied) the specified number of times.</summary>
    public void Replicate(int count)
    {
      var slice = AsReadOnlySpan();

      while (count-- > 0)
        Insert(m_bufferPosition, slice);
    }
  }
}
