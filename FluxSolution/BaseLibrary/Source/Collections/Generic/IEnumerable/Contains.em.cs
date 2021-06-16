using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns whether the source contains all of the items in subset, using the specified comparer.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset, System.Collections.Generic.IEqualityComparer<T> comparer)
      => !subset.Except(source, comparer).Any();
    /// <summary>Returns whether the source contains all of the items in subset, using the default comparer.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset)
      => ContainsAll(source, subset, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Returns whether the source contains any of the items in subset, using the specified comparer.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset, System.Collections.Generic.IEqualityComparer<T> comparer)
      => subset.Any(item => source.Contains(item, comparer));
    /// <summary>Returns whether the source contains any of the items in subset.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset)
      => ContainsAny(source, subset, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
