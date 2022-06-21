using System.Linq;

namespace Flux
{
  public static partial class SpanEm
  {
    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static System.ReadOnlySpan<T> NormalizeAll<T>(this System.ReadOnlySpan<T> source, T normalizeWith, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var target = new T[source.Length];

      var normalizedIndex = 0;

      var isPrevious = true; // Set to true in order for trimming to occur on the left.

      for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
      {
        var character = source[sourceIndex];

        var isCurrent = predicate(character);

        if (!(isPrevious && isCurrent))
        {
          target[normalizedIndex++] = isCurrent ? normalizeWith : character;

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedIndex--;

      return target[..normalizedIndex];
    }
    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the specified equality comparer.</summary>
    public static System.ReadOnlySpan<T> NormalizeAll<T>(this System.ReadOnlySpan<T> source, T normalizeWith, System.Collections.Generic.IEqualityComparer<T> equalityComparer, System.Collections.Generic.IList<T> normalize)
      => NormalizeAll(source, normalizeWith, t => normalize.Contains(t, equalityComparer));
    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the default equality comparer.</summary>
    public static System.ReadOnlySpan<T> NormalizeAll<T>(this System.ReadOnlySpan<T> source, T normalizeWith, System.Collections.Generic.IList<T> normalize)
      => NormalizeAll(source, normalizeWith, normalize.Contains);
  }
}
