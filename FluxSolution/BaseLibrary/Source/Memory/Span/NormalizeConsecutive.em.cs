using System.Linq;

namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Trim all occurences of sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static void NormalizeConsecutive<T>(ref this System.Span<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] trim)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var trimIndex = 0;

      var sourceValuePrevious = default(T);

      for (int sourceIndex = 0, sourceLength = source.Length; sourceIndex < sourceLength; sourceIndex++)
      {
        var sourceValue = source[sourceIndex];

        if (sourceIndex == 0 || !comparer.Equals(sourceValue, sourceValuePrevious) || (trim.Length > 0 && !trim.Contains(sourceValue, comparer)))
        {
          source[trimIndex++] = sourceValue;
        }

        sourceValuePrevious = sourceValue;
      }

      source = source.Slice(0, trimIndex);
    }
    public static void NormalizeConsecutive<T>(ref this System.Span<T> source, params T[] trim)
      => NormalizeConsecutive(ref source, System.Collections.Generic.EqualityComparer<T>.Default, trim);
  }
}
