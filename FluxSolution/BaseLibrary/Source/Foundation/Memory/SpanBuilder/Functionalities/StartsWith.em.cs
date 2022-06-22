namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the specified comparer.</summary>
    public bool StartsWith(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var targetIndex = target.Length;

      if (m_bufferPosition < targetIndex) return false;

      while (--targetIndex >= 0)
        if (!equalityComparer.Equals(m_buffer[targetIndex], target[targetIndex]))
          return false;

      return true;
    }
    /// <summary>Indicates whether the sequence ends with the other sequence. Uses the default comparer.</summary>
    public bool StartsWith(System.ReadOnlySpan<T> target)
       => StartsWith(target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
