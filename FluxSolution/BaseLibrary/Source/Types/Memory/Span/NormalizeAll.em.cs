using System.Linq;

namespace Flux
{
  public static partial class XtendSpan
  {
    /// <summary>Normalize (in-place) all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static System.Span<T> NormalizeAll<T>(this System.Span<T> source, T normalizeWith, System.Func<T, bool> predicate)
    {
      var normalizeIndex = 0;

      var previous = true; // Set to true in order for trimming to occur on the left.

      for (int sourceIndex = 0, sourceLength = source.Length; sourceIndex < sourceLength; sourceIndex++)
      {
        var current = (predicate ?? throw new System.ArgumentNullException(nameof(predicate)))(source[sourceIndex]);

        if (!(previous && current))
        {
          source[normalizeIndex++] = current ? normalizeWith : source[sourceIndex];

          previous = current;
        }
      }

      return source.Slice(0, previous ? normalizeIndex - 1 : normalizeIndex);
    }
    /// <summary>Normalize (in-place) all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the specified equality comparer.</summary>
    public static System.Span<T> NormalizeAll<T>(this System.Span<T> source, T normalizeWith, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] normalize)
      => NormalizeAll(source, normalizeWith, t => normalize.Contains(t, comparer));
    /// <summary>Normalize (in-place) all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the default equality comparer.</summary>
    public static System.Span<T> NormalizeAll<T>(this System.Span<T> source, T normalizeWith, params T[] normalize)
      => NormalizeAll(source, normalizeWith, normalize.Contains);
  }
}
