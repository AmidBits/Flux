namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Copy the specified count from source into target at the specified offset.</summary>
    public void CopyTo(System.Span<T> target, int offset, int count)
    {
      for (count--; count >= 0; count--)
        target[offset + count] = m_buffer[count];
    }
    /// <summary>Copy the source into target at the specified offset.</summary>
    public void CopyTo(System.Span<T> target, int offset)
      => CopyTo(target, offset, m_bufferPosition - offset);
    /// <summary>Copy the source into target at the specified offset.</summary>
    public void CopyTo(System.Span<T> target)
      => CopyTo(target, 0, System.Math.Min(m_bufferPosition, target.Length));
  }
}
