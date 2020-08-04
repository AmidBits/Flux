namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Reports the length (or count) of equal.</summary>
    public static bool EndsWith<T>(this System.Span<T> source, System.Span<T> target, System.Collections.Generic.IEqualityComparer<T>? comparer = null)
    {
      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      if (sourceIndex < targetIndex) return false;

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      while (--sourceIndex >= 0 && --targetIndex >= 0)
        if (!comparer.Equals(source[sourceIndex], target[targetIndex]))
          return false;

      return true;
    }
    /// <summary>Reports the length (or count) of equal, using the default comparer.</summary>
    public static bool EndsWith<T>(this System.Span<T> source, System.Span<T> target)
      => EndsWith(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
