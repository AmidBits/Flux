namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Replace all elements using the specified replacement selector function.</summary>
    public static T[] ReplaceAll<T>(this System.ReadOnlySpan<T> source, System.Func<T, T> replacementSelector)
    {
      var buffer = new T[source.Length];

      for (var index = source.Length - 1; index >= 0; index--)
        buffer[index] = (replacementSelector ?? throw new System.ArgumentNullException(nameof(replacementSelector)))(source[index]);

      return buffer;
    }

    /// <summary>Replace all elements satisfying the predicate with the specified character.</summary>
    public static T[] ReplaceAll<T>(this System.ReadOnlySpan<T> source, T replacement, System.Func<T, bool> predicate)
    {
      var buffer = source.ToArray();

      for (var index = source.Length - 1; index >= 0; index--)
        buffer[index] = (predicate ?? throw new System.ArgumentNullException(nameof(predicate)))(source[index]) ? replacement : source[index];

      return buffer;
    }
    /// <summary>Replace all specified elements with the specified element. Uses the specified comparer.</summary>
    public static T[] ReplaceAll<T>(this System.ReadOnlySpan<T> source, T replacement, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, params T[] replace)
      => ReplaceAll(source, replacement, t => System.Array.Exists(replace, e => comparer.Equals(e, t)));
    /// <summary>Replace all specified elements with the specified element. Uses the default comparer.</summary>
    public static T[] ReplaceAll<T>(this System.ReadOnlySpan<T> source, T replacement, params T[] replace)
      => ReplaceAll(source, replacement, System.Collections.Generic.EqualityComparer<T>.Default, replace);
  }
}
