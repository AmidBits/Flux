namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed.</summary>
    public System.ReadOnlySpan<T> RemoveAll(System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var sourceLength = m_bufferPosition;

      var target = new T[sourceLength];

      var removedIndex = 0;

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
      {
        var sourceValue = m_buffer[sourceIndex];

        if (!predicate(sourceValue))
          target[removedIndex++] = sourceValue;
      }

      return target[..removedIndex].ToArray();
    }
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the specified comparer.</summary>
    public System.ReadOnlySpan<T> RemoveAll([System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, System.Collections.Generic.IList<T> remove)
      => RemoveAll(t => remove.Contains(t, equalityComparer));
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the default comparer.</summary>
    public System.ReadOnlySpan<T> RemoveAll(System.Collections.Generic.IList<T> remove)
      => RemoveAll(remove.Contains);
  }
}
