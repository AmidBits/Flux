namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Returns a subset containing the right most specified number of elements, if available, otherwise as many as there are.</summary>
    public System.ReadOnlySpan<T> LeftMost(int maxCount)
      => m_buffer[..System.Math.Min(m_bufferPosition, maxCount)];
  }
}
