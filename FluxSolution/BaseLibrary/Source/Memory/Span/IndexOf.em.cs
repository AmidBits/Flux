namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Determines the index of the first occurence of target in source. Uses the specified comparer.</summary>
    public static int IndexOf<T>(this System.Span<T> source, T target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (int index = 0, length = source.Length; index < length; index++)
        if (comparer.Equals(source[index], target))
          return index;

      return -1;
    }
    /// <summary>Determines the index of the first occurence of target in source. Uses the default comparer.</summary>
    public static int IndexOf<T>(this System.Span<T> source, T target)
      => IndexOf(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
