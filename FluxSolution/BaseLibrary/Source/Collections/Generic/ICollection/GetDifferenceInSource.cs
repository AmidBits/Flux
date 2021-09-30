namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence of elements that are in the collection but not in the specified sequence. Uses the specified equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> GetDifferenceInSource<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      var targetSet = new System.Collections.Generic.HashSet<T>(target, equalityComparer);

      foreach (var t in source)
        if (!targetSet.Contains(t))
          yield return t;
    }
    /// <summary>Creates a new sequence of elements that are in the collection but not in the specified sequence. Uses the default equality comparer.</summary>
    public static System.Collections.Generic.IEnumerable<T> GetDifferenceInSource<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => GetDifferenceInSource(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
