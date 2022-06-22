namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the specified comparer.</summary>
    public int CountEqualAtStart(System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var minLength = System.Math.Min(m_bufferPosition, target.Length);

      var index = 0;
      while (index < minLength && equalityComparer.Equals(m_buffer[index], target[index]))
        index++;
      return index;
    }
    /// <summary>Reports the length (or count) of equality at the start of the sequences. Using the default comparer.</summary>
    public int CountEqualAtStart(System.ReadOnlySpan<T> target)
      => CountEqualAtStart(target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
