namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new sequence of elements that are in the source set but not in the specified target set. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.HashSet<T> SourceDifference<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int additionalCapacity = 0)
    {
      if (source == target)
        return new System.Collections.Generic.HashSet<T>(additionalCapacity, equalityComparer); // A set minus itself is an empty set.

      if (source.Length == 0)
        return new System.Collections.Generic.HashSet<T>(additionalCapacity, equalityComparer); // If source is empty, the result must be empty.

      if (target.Length == 0)
        return source.ToHashSet(equalityComparer, additionalCapacity); // If target is empty, source is the result.

      return source.Except(target, equalityComparer, additionalCapacity);
    }
  }
}
