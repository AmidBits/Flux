using System.Linq;

namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    public static System.ReadOnlySpan<T> Union<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var union = new System.Collections.Generic.List<T>();

      var set = new System.Collections.Generic.HashSet<T>(equalityComparer);

      foreach (var element in source)
        if (set.Add(element))
          union.Add(element);

      foreach (var element in target)
        if (set.Add(element))
          union.Add(element);

      return union.AsSpan();
    }
  }
}
