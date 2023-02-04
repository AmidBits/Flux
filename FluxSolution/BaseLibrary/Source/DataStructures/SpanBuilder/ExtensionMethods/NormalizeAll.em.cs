namespace Flux
{
  public static partial class ExtensionMethodsSpanBuilder
  {
    /// <summary>Normalize where the <paramref name="predicate"/> is satisfied using the <paramref name="normalizedValue"/> throughout the builder. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static SpanBuilder<T> NormalizeAll<T>(this SpanBuilder<T> source, T normalizedValue, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var normalizedIndex = 0;

      var isPrevious = true; // Set to true in order for trimming to occur on the left.

      for (var index = 0; index < source.Length; index++)
      {
        var character = source.GetValue(index);

        var isCurrent = predicate(character);

        if (!(isPrevious && isCurrent))
        {
          source.SetValue(normalizedIndex++, isCurrent ? normalizedValue : character);

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedIndex--;

      return source.Remove(normalizedIndex, source.Length - normalizedIndex);
    }

    /// <summary>Normalize the <paramref name="normalizeValues"/> using the <paramref name="normalizedValue"/> throughout the builder. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the specified equality comparer.</summary>
    public static SpanBuilder<T> NormalizeAll<T>(this SpanBuilder<T> source, T normalizedValue, System.Collections.Generic.IList<T> normalizeValues, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return source.NormalizeAll(normalizedValue, t => normalizeValues.Contains(t, equalityComparer));
    }
  }
}
