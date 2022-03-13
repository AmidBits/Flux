using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns whether the source contains any of the items in subset, using the specified comparer.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => subset.Any(v => source.Contains(v, equalityComparer));
    /// <summary>Returns whether the source contains any of the items in subset.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset)
      => ContainsAny(source, subset, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
