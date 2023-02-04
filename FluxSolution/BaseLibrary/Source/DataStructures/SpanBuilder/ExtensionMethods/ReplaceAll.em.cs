namespace Flux
{
  public static partial class ExtensionMethodsSpanBuilder
  {
    /// <summary>Replace all values using the specified replacement selector.</summary>
    public static SpanBuilder<T> ReplaceAll<T>(this SpanBuilder<T> source, System.Func<T, T> replacementSelector)
    {
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = source.Length - 1; index >= 0; index--)
        source.SetValue(index, replacementSelector(source.GetValue(index)));

      return source;
    }

    /// <summary>Replace all values with <paramref name="replacement"/> where the <paramref name="predicate"/> is satisfied.</summary>
    public static SpanBuilder<T> ReplaceAll<T>(this SpanBuilder<T> source, T replacement, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source.GetValue(index)))
          source.SetValue(index, replacement);

      return source;
    }

    /// <summary>Replace all <paramref name="replaceValues"/> with <paramref name="replacement"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static SpanBuilder<T> ReplaceAll<T>(this SpanBuilder<T> source, T replacement, System.Collections.Generic.IList<T> replaceValues, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return source.ReplaceAll(replacement, t => replaceValues.Any(r => equalityComparer.Equals(r, t)));
    }
  }
}
