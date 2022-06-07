namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source collection contains any of the elements in the specified target sequence.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
    //=> (target is System.Collections.Generic.ICollection<T> ic && ic.Count == 0) // If target is empty, there is nothing to include, any is false.
    //|| System.Linq.Enumerable.Any(Intersection(source, target));
    //|| System.Linq.Enumerable.Any(target, t => System.Linq.Enumerable.Contains(source, t));
    {
      if ((source is System.Collections.Generic.ICollection<T> sc && sc.Count == 0) || (target is System.Collections.Generic.ICollection<T> tc && tc.Count == 0))
        return false; // If either sequence is empty, the result is nothing contains anything so the result is false.

      var ths = new System.Collections.Generic.HashSet<T>(target);

      if (ths.Count == 0) // If target is empty, 
        return false;

      return source.Any(s => ths.Contains(s));
    }
  }
}
