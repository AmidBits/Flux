namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Determines the index of the first occurence of target in source. Uses the specified comparer.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, T target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (int index = 0, length = source.Length; index < length; index++)
        if (comparer.Equals(source[index], target))
          return index;

      return -1;
    }
    /// <summary>Determines the index of the first occurence of target in source. Uses the default comparer.</summary>
    public static int IndexOf<T>(this System.ReadOnlySpan<T> source, T target)
      => IndexOf(source, target, System.Collections.Generic.EqualityComparer<T>.Default);

    public static int IndexOfX<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceLength = source.Length;
      var targetLength = target.Length;

      for (int sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
        for (int targetIndex = 0; targetIndex < targetLength; targetIndex++)
          if (!comparer.Equals(source[sourceIndex + targetIndex], target[targetIndex]))
            break;
          else if (targetLength - targetIndex == 1)
            return sourceIndex;

      return -1;
    }
  }
}
