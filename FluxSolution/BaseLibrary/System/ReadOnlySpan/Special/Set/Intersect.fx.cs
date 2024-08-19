namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    public static System.Collections.Generic.List<T> Intersect<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int additionalCapacity = 0)
    {
      if (source.Length == 0 || target.Length == 0) return new System.Collections.Generic.List<T>(additionalCapacity);

      if (source.Length < target.Length) return Intersect(target, source, equalityComparer, additionalCapacity); // If source has more items, it's faster to switch it around with target.

      var intersect = new System.Collections.Generic.List<T>(int.Min(source.Length, target.Length) + additionalCapacity);

      var intersectable = target.ToHashSet(equalityComparer);

      for (var index = 0; index < source.Length; index++)
      {
        if (source[index] is var item && intersectable.Remove(item))
          intersect.Add(item);

        if (intersectable.Count == 0)
          break;
      }

      return intersect;
    }
  }
}
