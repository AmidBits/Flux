using System.Linq;

namespace Flux
{
  public static partial class XtendReadOnlySpan
  {
    /// <summary>Remove all <typeparamref name="T"/>'s satisfying the predicate.</summary>
    public static T[] RemoveAll<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
    {
      var sourceLength = source.Length;

      var buffer = new T[sourceLength];
      var bufferIndex = 0;

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
      {
        var sourceValue = source[sourceIndex];

        if (!(predicate ?? throw new System.ArgumentNullException(nameof(predicate)))(sourceValue))
          buffer[bufferIndex++] = sourceValue;
      }

      System.Array.Resize(ref buffer, bufferIndex);

      return buffer;
    }
    /// <summary>Remove the specified elements. Uses the specified comparer.</summary>
    public static T[] RemoveAll<T>(this System.ReadOnlySpan<T> source, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, params T[] remove)
      => RemoveAll(source, t => remove.Contains(t, comparer));
    /// <summary>Remove the specified elements. Uses the default comparer.</summary>
    public static T[] RemoveAll<T>(this System.ReadOnlySpan<T> source, params T[] remove)
      => RemoveAll(source, t => remove.Contains(t));
  }
}
