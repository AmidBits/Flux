using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns a sequence with the elements from the source and the specified elements.</summary>
    public static System.Collections.Generic.IEnumerable<T> Prepend<T>(this System.Collections.Generic.IEnumerable<T> source, params T[] others)
      => System.Linq.Enumerable.Concat(others, source);
    /// <summary>Returns a sequence with the elements from the source and the specified sequences.</summary>
    public static System.Collections.Generic.IEnumerable<T> Prepend<T>(this System.Collections.Generic.IEnumerable<T> source, params System.Collections.Generic.IEnumerable<T>[] others)
      => System.Linq.Enumerable.Concat(others.SelectMany(other => other), source);
  }
}
