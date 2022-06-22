namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the specified comparer.</summary>
    public bool EndsWith(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var sourceIndex = m_bufferPosition;
      var targetIndex = target.Length;

      if (sourceIndex < targetIndex) return false;

      while (--sourceIndex >= 0 && --targetIndex >= 0)
        if (!equalityComparer.Equals(m_buffer[sourceIndex], target[targetIndex]))
          return false;

      return true;
    }
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the default comparer.</summary>
    public bool EndsWith(System.ReadOnlySpan<T> target)
      => EndsWith(target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
