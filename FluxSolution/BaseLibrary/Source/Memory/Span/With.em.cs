namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Determines whether the end of this ReadOnlySpan instance matches a specified target ReadOnlySpan.</summary>
    public static bool StartsWith<T>(this System.Span<T> source, System.Span<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      var targetLength = target.Length;

      if (source.Length < targetLength) return false;

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = 0; index < targetLength; index++)
        if (!comparer.Equals(source[index], target[index]))
          return false;

      return true;
    }
    /// <summary>Determines whether the end of this ReadOnlySpan instance matches a specified target ReadOnlySpan.</summary>
    public static bool StartsWith<T>(this System.Span<T> source, System.Span<T> target)
      => StartsWith(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Reports the length (or count) of equal.</summary>
    public static bool EndsWith<T>(this System.Span<T> source, System.Span<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
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
