using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed.</summary>
    public static System.ReadOnlySpan<T> RemoveAll<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var sourceLength = source.Length;

      var target = new T[sourceLength];

      var removedIndex = 0;

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
      {
        var sourceValue = source[sourceIndex];

        if (!predicate(sourceValue))
          target[removedIndex++] = sourceValue;
      }

      return target[..removedIndex].ToArray();
    }
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the specified comparer.</summary>
    public static System.ReadOnlySpan<T> RemoveAll<T>(ref this System.ReadOnlySpan<T> source, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> equalityComparer, System.Collections.Generic.IList<T> remove)
      => RemoveAll(source, t => remove.Contains(t, equalityComparer));
    /// <summary>Creates a new readonlyspan with all elements satisfying the predicate removed. Uses the default comparer.</summary>
    public static System.ReadOnlySpan<T> RemoveAll<T>(ref this System.ReadOnlySpan<T> source, System.Collections.Generic.IList<T> remove)
      => RemoveAll(source, remove.Contains);
  }
}
