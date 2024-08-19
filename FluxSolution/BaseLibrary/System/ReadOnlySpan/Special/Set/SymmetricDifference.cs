namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new sequence of elements that are present either in the source set or in the specified target set, but not both. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.HashSet<T> SymmetricDifference<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int additionalCapacity = 0, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      //if (source == target) return new System.Collections.Generic.HashSet<T>(additionalCapacity, equalityComparer); // A symmetric difference of a set with itself is an empty set.

      //if (source.Length == 0) return target.ToHashSet(equalityComparer, additionalCapacity); // If source is empty, target is the result.

      //if (target.Length == 0) return source.ToHashSet(equalityComparer, additionalCapacity); // If target is empty, source is the result.

      if (source == target) return new System.Collections.Generic.HashSet<T>(additionalCapacity, equalityComparer); // A symmetric difference of a set with itself is an empty set.

      if (source.Length == 0) return target.ToHashSet(equalityComparer, additionalCapacity); // If source is empty, target is the result.

      if (target.Length == 0) return source.ToHashSet(equalityComparer, additionalCapacity); // If target is empty, source is the result.

      //if (source.Length > target.Length) SymmetricDifference(target, source, additionalCapacity, equalityComparer);

      //var sd = new System.Collections.Generic.List<T>(source.Length + target.Length + additionalCapacity);
      //sd.AddRange(source.Except(target, equalityComparer));
      //sd.AddRange(target.Except(source, equalityComparer));
      var sd = source.Except(target, equalityComparer, target.Length + additionalCapacity);
      sd.UnionWith(target.Except(source, equalityComparer));
      return sd;
    }
  }
}
