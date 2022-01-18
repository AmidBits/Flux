using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
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

      return source[..removedIndex].ToArray();
    }
    /// <summary>Remove (in-place) the specified elements. Uses the specified comparer.</summary>
    public static System.Span<T> RemoveAll<T>(ref this System.Span<T> source, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, params T[] remove)
      => RemoveAll(source, t => remove.Contains(t, equalityComparer));
    /// <summary>Remove (in-place) the specified elements. Uses the default comparer.</summary>
    public static System.Span<T> RemoveAll<T>(ref this System.Span<T> source, params T[] remove)
      => RemoveAll(source, remove.Contains);
  }
}
