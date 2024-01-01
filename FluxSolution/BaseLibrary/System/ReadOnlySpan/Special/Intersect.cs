using System.Linq;

namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    public static System.ReadOnlySpan<T> Intersect<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var intersection = new System.Collections.Generic.List<T>();

      var set = new System.Collections.Generic.HashSet<T>(equalityComparer);

      foreach (var element in target)
        set.Add(element);

      foreach (var element in source)
        if (set.Remove(element))
          intersection.Add(element);

      return intersection.AsSpan();
    }
  }
}
