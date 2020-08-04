namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Determines the index of the last occurence of target in source. Uses the specified comparer.</summary>
    public static int LastIndexOf<T>(this System.Span<T> source, T target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = source.Length - 1; index >= 0; index--)
        if (comparer.Equals(source[index], target))
          return index;

      return -1;
    }
    /// <summary>Determines the index of the last occurence of target in source. Uses the default comparer.</summary>
    public static int LastIndexOf<T>(this System.Span<T> source, T target)
      => LastIndexOf(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
