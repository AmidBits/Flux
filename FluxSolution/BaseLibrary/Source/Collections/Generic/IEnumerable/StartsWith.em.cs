using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence, using the specified element equality comparer.</summary>
    public static bool StartsWith<T>(this System.Collections.Generic.IEnumerable<T>? source, System.Collections.Generic.IEnumerable<T>? target, System.Collections.Generic.IEqualityComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      using var e = source?.GetEnumerator() ?? throw new System.ArgumentNullException(nameof(source));

      return target?.All(item => e.MoveNext() && comparer.Equals(e.Current, item)) ?? throw new System.ArgumentNullException(nameof(target));
    }
  }
}
