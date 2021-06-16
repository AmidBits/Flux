using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Normalize (in-place, destructive) all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static System.Span<T> NormalizeAll<T>(this System.Span<T> source, T normalizeWith, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var normalizedIndex = 0;

      var isPrevious = true; // Set to true in order for trimming to occur on the left.

      for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
      {
        var character = source[sourceIndex];

        var isCurrent = predicate(character);

        if (!(isPrevious && isCurrent))
        {
          source[normalizedIndex++] = isCurrent ? normalizeWith : character;

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedIndex--;

      return source.Slice(0, normalizedIndex);
    }
    /// <summary>Normalize (in-place, destructive) all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the specified equality comparer.</summary>
    public static System.Span<T> NormalizeAll<T>(this System.Span<T> source, T normalizeWith, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] normalize)
      => NormalizeAll(source, normalizeWith, t => normalize.Contains(t, comparer));
    /// <summary>Normalize (in-place, destructive) all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the default equality comparer.</summary>
    public static System.Span<T> NormalizeAll<T>(this System.Span<T> source, T normalizeWith, params T[] normalize)
      => NormalizeAll(source, normalizeWith, normalize.Contains);
  }
}
