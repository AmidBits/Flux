namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Replace (in-place) all characters using the specified replacement selector function.</summary>
    public static void ReplaceAll<T>(this System.Span<T> source, System.Func<T, T> replacementSelector)
    {
      if (replacementSelector is null) throw new System.ArgumentNullException(nameof(replacementSelector));

      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = replacementSelector(source[index]);
    }

    /// <summary>Replace (in-place) all characters satisfying the predicate with the specified character.</summary>
    public static void ReplaceAll<T>(this System.Span<T> source, T replacement, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source[index]))
          source[index] = replacement;
    }
    /// <summary>Replace (in-place) all specified elements with the specified element. Uses the specified comparer.</summary>
    public static void ReplaceAll<T>(this System.Span<T> source, T replacement, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, params T[] replace)
      => ReplaceAll(source, replacement, t => System.Array.Exists(replace, e => comparer.Equals(e, t)));
    /// <summary>Replace (in-place) all specified elements with the specified element. Uses the default comparer.</summary>
    public static void ReplaceAll<T>(this System.Span<T> source, T replacement, params T[] replace)
      => ReplaceAll(source, replacement, System.Collections.Generic.EqualityComparer<T>.Default, replace);
  }
}
