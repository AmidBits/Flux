namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements that are in the source set but not in the specified target set. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> SourceDifference<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      var targetSet = new System.Collections.Generic.HashSet<T>(target, equalityComparer);

      foreach (var t in source)
        if (!targetSet.Contains(t))
          yield return t;
    }
    /// <summary>Creates a new sequence of elements that are in the source set but not in the specified target set. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> SourceDifference<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => SourceDifference(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
