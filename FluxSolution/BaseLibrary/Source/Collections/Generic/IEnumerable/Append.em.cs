using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns a sequence with the elements from the source and the specified elements.</summary>
    public static System.Collections.Generic.IEnumerable<T> Append<T>(this System.Collections.Generic.IEnumerable<T> source, params T[] others)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Concat(others ?? throw new System.ArgumentNullException(nameof(others)));
    /// <summary>Returns a sequence with the elements from the source and the specified sequences.</summary>
    public static System.Collections.Generic.IEnumerable<T> Append<T>(this System.Collections.Generic.IEnumerable<T> source, params System.Collections.Generic.IEnumerable<T>[] others)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).Concat((others ?? throw new System.ArgumentNullException(nameof(source))).SelectMany(other => other));
  }
}
