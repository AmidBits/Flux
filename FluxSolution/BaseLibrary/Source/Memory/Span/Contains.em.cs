namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Determines whether the this instance contains the specified target. Uses the specified comparer.</summary>
    public static bool Contains<T>(this System.Span<T> source, T target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = source.Length - 1; index >= 0; index--)
        if (comparer.Equals(source[index], target))
          return true;

      return false;
    }
    /// <summary>Determines whether the this instance contains the specified target. Uses the default comparer.</summary>
    public static bool Contains<T>(this System.Span<T> source, T target)
      => Contains(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
