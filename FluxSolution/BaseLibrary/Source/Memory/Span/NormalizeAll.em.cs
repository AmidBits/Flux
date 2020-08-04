using System.Linq;

namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static void NormalizeAll<T>(ref this System.Span<T> source, T normalizeWith, System.Func<T, bool> predicate)
    {
      var normalizeIndex = 0;

      var previous = true; // Set to true in order for trimming to occur on the left.

      for (int sourceIndex = 0, sourceLength = source.Length; sourceIndex < sourceLength; sourceIndex++)
      {
        var current = predicate(source[sourceIndex]);

        if (!(previous && current))
        {
          source[normalizeIndex++] = current ? normalizeWith : source[sourceIndex];

          previous = current;
        }
      }

      source = source.Slice(0, previous ? normalizeIndex - 1 : normalizeIndex);
    }
    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static void NormalizeAll<T>(ref this System.Span<T> source, T normalizeWith, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] normalize)
      => NormalizeAll(ref source, normalizeWith, t => normalize.Contains(t, comparer));
  }
}
