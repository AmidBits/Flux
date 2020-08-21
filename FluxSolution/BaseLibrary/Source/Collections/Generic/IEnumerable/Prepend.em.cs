using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns a sequence with the elements from the source and the specified elements.</summary>
    public static System.Collections.Generic.IEnumerable<T> Prepend<T>(this System.Collections.Generic.IEnumerable<T> source, params T[] others)
      => others.ThrowOnNull(nameof(others)).Concat(source.ThrowOnNull());
    /// <summary>Returns a sequence with the elements from the source and the specified sequences.</summary>
    public static System.Collections.Generic.IEnumerable<T> Prepend<T>(this System.Collections.Generic.IEnumerable<T> source, params System.Collections.Generic.IEnumerable<T>[] others)
      => others.ThrowOnNull(nameof(others)).SelectMany(other => other).Concat(source.ThrowOnNull());
  }
}
