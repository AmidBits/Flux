using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns whether the source contains all of the items in subset, using the specified comparer.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => !subset.Except(source, equalityComparer).Any();
    /// <summary>Returns whether the source contains all of the items in subset, using the default comparer.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset)
      => ContainsAll(source, subset, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
