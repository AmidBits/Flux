using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Remove all <typeparamref name="T"/>'s satisfying the predicate.</summary>
    public static T[] RemoveAll<T>(this System.ReadOnlySpan<T> source, System.Func<T, bool> predicate)
    {
      var target = source.ToArray();
      RemoveAll(target, predicate);
      return target;
    }
    /// <summary>Remove the specified elements. Uses the specified comparer.</summary>
    public static T[] RemoveAll<T>(this System.ReadOnlySpan<T> source, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer, params T[] remove)
      => RemoveAll(source, t => remove.Contains(t, comparer));
    /// <summary>Remove the specified elements. Uses the default comparer.</summary>
    public static T[] RemoveAll<T>(this System.ReadOnlySpan<T> source, params T[] remove)
      => RemoveAll(source, remove.Contains);
  }
}
