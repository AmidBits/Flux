namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Returns the index within source where the rotation of the target begins, or -1 if not found.</summary>
    public int IndexOfRotation(System.ReadOnlySpan<T> target)
    {
      if (m_bufferPosition != target.Length)
        return -1; // If length is different, target cannot be a rotation in source. They have to be equal in length.

      return new SpanBuilder<T>(Replicate(1)).IndexOf(target);
    }
  }
}
