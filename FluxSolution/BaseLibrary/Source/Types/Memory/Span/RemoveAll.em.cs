using System.Linq;

namespace Flux
{
  public static partial class SystemSpanEm
  {
    /// <summary>Remove (in-place) all elements satisfying the predicate.</summary>
    public static System.Span<T> RemoveAll<T>(this System.Span<T> source, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var sourceLength = source.Length;

      var removedIndex = 0;

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
      {
        var sourceValue = source[sourceIndex];

        if (!predicate(sourceValue))
          source[removedIndex++] = sourceValue;
      }

      return source.Slice(0, removedIndex).ToArray();
    }
    /// <summary>Remove (in-place) the specified elements. Uses the specified comparer.</summary>
    public static System.Span<T> RemoveAll<T>(ref this System.Span<T> source, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, params T[] remove)
      => RemoveAll(source, t => remove.Contains(t, comparer));
    /// <summary>Remove (in-place) the specified elements. Uses the default comparer.</summary>
    public static System.Span<T> RemoveAll<T>(ref this System.Span<T> source, params T[] remove)
      => RemoveAll(source, remove.Contains);
  }
}
