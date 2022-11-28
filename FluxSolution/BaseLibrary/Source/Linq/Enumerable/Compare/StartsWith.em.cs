using System.Linq;

namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Determines whether the source sequence begins with the target sequence. Uses the specified equality comparer.</summary>
    public static bool StartsWith<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      using var e = source.GetEnumerator();

      return target.All(item => e.MoveNext() && equalityComparer.Equals(e.Current, item));
    }
  }
}
