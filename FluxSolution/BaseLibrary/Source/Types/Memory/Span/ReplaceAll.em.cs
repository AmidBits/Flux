namespace Flux
{
  public static partial class SpanEm
  {
    /// <summary>In-place replacement of all characters using the specified replacement selector function.</summary>
    public static System.Span<T> ReplaceAll<T>(this System.Span<T> source, System.Func<T, int, T> replacementSelector)
    {
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = replacementSelector(source[index], index);

      return source;
    }

    /// <summary>In-place replacement of all characters satisfying the predicate with the specified character.</summary>
    public static System.Span<T> ReplaceAll<T>(this System.Span<T> source, T replacement, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source[index], index))
          source[index] = replacement;

      return source;
    }

    /// <summary>In-place replacement of all specified elements with the specified replacement element. Uses the specified comparer.</summary>
    public static System.Span<T> ReplaceAll<T>(this System.Span<T> source, T replacement, System.Collections.Generic.IEqualityComparer<T>? equalityComparer, params T[] replace)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return ReplaceAll(source, replacement, (e, i) => System.Array.Exists(replace, a => equalityComparer.Equals(a, e)));
    }
  }
}
