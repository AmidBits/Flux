using System.Linq;

namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
    public static T[] TrimConsecutive<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> comparer, params T[] remove)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceLength = source.Length;

      var buffer = new T[sourceLength];
      var bufferIndex = 0;

      var sourceValuePrevious = default(T);

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
      {
        var sourceValue = source[sourceIndex];

        if (sourceIndex == 0 || !comparer.Equals(sourceValue, sourceValuePrevious) || (remove.Length > 0 && !remove.Contains(sourceValue, comparer)))
        {
          buffer[bufferIndex++] = sourceValue;
        }

        sourceValuePrevious = sourceValue;
      }

      System.Array.Resize(ref buffer, bufferIndex);

      return buffer;
    }
    public static T[] TrimConsecutive<T>(this System.ReadOnlySpan<T> source, params T[] remove)
      => TrimConsecutive(source, System.Collections.Generic.EqualityComparer<T>.Default, remove);
  }
}
