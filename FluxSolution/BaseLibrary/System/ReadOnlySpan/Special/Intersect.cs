namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    public static System.Collections.Generic.List<T> Intersect<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var set = new System.Collections.Generic.HashSet<T>(equalityComparer);

      set.AddSpan(target);

      var intersection = new System.Collections.Generic.List<T>();

      for (var index = 0; index < source.Length; index++)
      {
        if (source[index] is var item && set.Remove(item))
          intersection.Add(item);

        if (set.Count == 0)
          break;
      }

      return intersection;
    }
  }
}
