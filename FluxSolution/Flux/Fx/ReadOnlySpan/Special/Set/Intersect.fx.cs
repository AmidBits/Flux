namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <param name="additionalCapacity"></param>
    /// <returns></returns>
    public static System.Collections.Generic.HashSet<T> Intersect<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      var intersect = new System.Collections.Generic.HashSet<T>(int.Min(source.Length, target.Length));

      if (source.Length > 0 && target.Length > 0)
      {
        if (source.Length < target.Length)
          return Intersect(target, source, equalityComparer); // If source has more items, it's faster to switch it around with target.

        var intersectable = target.ToHashSet(equalityComparer);

        for (var index = source.Length - 1; index >= 0; index--)
        {
          if (source[index] is var item && intersectable.Remove(item))
            intersect.Add(item);

          if (intersectable.Count == 0)
            break;
        }
      }

      return intersect;
    }
  }
}
