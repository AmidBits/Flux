namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the specified comparer.</summary>
    /// <param name="minLength">The smaller length of the two spans.</param>
    public int CountEqualAtEnd(System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var sourceIndex = m_bufferPosition;
      var targetIndex = target.Length;

      for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
        if (!equalityComparer.Equals(m_buffer[sourceIndex], target[targetIndex]))
          return atEnd;

      return 0;
    }
    /// <summary>Reports the count of elements equal at the end of the sequences. Using the default comparer.</summary>
    public int CountEqualAtEnd(System.ReadOnlySpan<T> target)
      => CountEqualAtEnd(target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
