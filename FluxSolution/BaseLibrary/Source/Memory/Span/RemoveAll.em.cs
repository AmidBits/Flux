using System.Linq;

namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Remove (in-place) all elements satisfying the predicate.</summary>
    public static void RemoveAll<T>(ref this System.Span<T> source, System.Func<T, bool> predicate)
    {
      var sourceLength = source.Length;

      var replaceIndex = 0;

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
      {
        var sourceValue = source[sourceIndex];

        if (!(predicate ?? throw new System.ArgumentNullException(nameof(predicate)))(sourceValue))
        {
          source[replaceIndex++] = sourceValue;
        }
      }

      source = source.Slice(0, replaceIndex).ToArray();
    }
    /// <summary>Remove the specified elements. Uses the specified comparer.</summary>
    public static void RemoveAll<T>(ref this System.Span<T> source, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, params T[] remove)
      => RemoveAll(ref source, t => remove.Contains(t, comparer));
    /// <summary>Remove the specified elements. Uses the default comparer.</summary>
    public static void RemoveAll<T>(ref this System.Span<T> source, params T[] remove)
      => RemoveAll(ref source, t => remove.Contains(t));
  }
}
