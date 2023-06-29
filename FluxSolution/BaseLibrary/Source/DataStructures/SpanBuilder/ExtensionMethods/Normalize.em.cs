namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {

    /// <summary>Normalize the specified (or all if none specified) consecutive <paramref name="values"/> in the string normalized. Uses the specfied <paramref name="equalityComparer"/>, or default if null.</summary>
    public static SpanBuilder<T> NormalizeAdjacent<T>(this SpanBuilder<T> source, System.Collections.Generic.IList<T> values, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var targetIndex = 0;
      var previous = default(T);

      for (var sourceIndex = 0; sourceIndex < source.Count; sourceIndex++)
      {
        var current = source[sourceIndex];

        if (!equalityComparer.Equals(current, previous) || (values.Count > 0 && !values.Contains(current, equalityComparer)))
        {
          source[targetIndex++] = current;

          previous = current;
        }
      }

      return source.Remove(targetIndex, source.Count - targetIndex);
    }

    /// <summary>Normalize where the <paramref name="predicate"/> is satisfied using the <paramref name="normalizedValue"/> throughout the builder. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static SpanBuilder<T> NormalizeAll<T>(this SpanBuilder<T> source, T normalizedValue, System.Func<T, bool>? predicate = null)
    {
      var normalizedIndex = 0;

      var isPrevious = true; // Set to true in order for trimming to occur on the left.

      for (var index = 0; index < source.Count; index++)
      {
        var character = source[index];

        var isCurrent = predicate?.Invoke(character) ?? true;

        if (!(isPrevious && isCurrent))
        {
          source[normalizedIndex++] = isCurrent ? normalizedValue : character;

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedIndex--;

      return source.Remove(normalizedIndex, source.Count - normalizedIndex);
    }

    /// <summary>Normalize the <paramref name="normalizeValues"/> using the <paramref name="normalizedValue"/> throughout the builder. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the specified equality comparer.</summary>
    public static SpanBuilder<T> NormalizeAll<T>(this SpanBuilder<T> source, T normalizedValue, System.Collections.Generic.IList<T> normalizeValues, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return NormalizeAll(source, normalizedValue, t => normalizeValues.Contains(t, equalityComparer));
    }
  }
}
