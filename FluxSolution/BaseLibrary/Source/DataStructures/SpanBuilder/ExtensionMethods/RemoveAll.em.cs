namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Remove all items where the <paramref name="predicate"/> is satisfied.</summary>
    public static SpanBuilder<T> RemoveAll<T>(this SpanBuilder<T> source, System.Func<T, bool>? predicate = null)
    {
      var removedIndex = 0;

      for (var index = 0; index < source.Count; index++)
      {
        var value = source[index];

        if (!predicate?.Invoke(value) ?? true)
          source[removedIndex++] = value;
      }

      return source.Remove(removedIndex, source.Count - removedIndex);
    }

    /// <summary>Remove all <paramref name="removeValues"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static SpanBuilder<T> RemoveAll<T>(this SpanBuilder<T> source, System.Collections.Generic.IList<T> removeValues, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return source.RemoveAll(t => removeValues.Contains(t, equalityComparer));
    }
  }
}
