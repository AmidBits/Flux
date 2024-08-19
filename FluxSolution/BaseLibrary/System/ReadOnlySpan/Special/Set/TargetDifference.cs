namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new sequence of elements that are in the specified target set but not in the source set. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.HashSet<T> TargetDifference<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, int additionalCapacity = 0, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source == target)
        return new System.Collections.Generic.HashSet<T>(additionalCapacity, equalityComparer); // A set minus itself is an empty set.

      if (target.Length == 0)
        return new System.Collections.Generic.HashSet<T>(additionalCapacity, equalityComparer); // If target is empty, the result must be empty.

      if (source.Length == 0)
        return target.ToHashSet(equalityComparer, additionalCapacity); // If source is empty, target is the result.

      return target.Except(source, equalityComparer, additionalCapacity); // If source has elements, adjust target.
    }
  }
}
