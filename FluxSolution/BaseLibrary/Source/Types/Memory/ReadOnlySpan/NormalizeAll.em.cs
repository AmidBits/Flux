using System.Linq;

namespace Flux
{
  public static partial class XtendReadOnlySpan
  {
    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static T[] NormalizeAll<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate, T normalizeWith)
    {
      var sourceLength = source.Length;

      var target = new T[sourceLength];
      var targetIndex = 0;

      var previous = true; // Set to true in order for trimming to occur on the left.

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
      {
        var current = (predicate ?? throw new System.ArgumentNullException(nameof(predicate)))(source[sourceIndex]);

        if (!(previous && current))
        {
          target[targetIndex++] = current ? normalizeWith : source[sourceIndex];

          previous = current;
        }
      }

      System.Array.Resize(ref target, previous ? targetIndex - 1 : targetIndex);

      return target;
    }
    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static T[] NormalizeAll<T>(this System.ReadOnlySpan<T> source, T normalizeWith, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] normalize)
      => NormalizeAll(source, t => normalize.Contains(t, comparer), normalizeWith);
  }
}
