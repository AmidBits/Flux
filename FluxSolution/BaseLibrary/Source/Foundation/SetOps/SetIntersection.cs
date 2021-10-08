namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements that are in both the source collection and the target sequence. I.e. all elements that both collections have in common.</summary>
    public static System.Collections.Generic.IEnumerable<T> SetIntersection<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => source.Count == 0 || (target is System.Collections.Generic.ICollection<T> ic && ic.Count == 0)
      ? System.Linq.Enumerable.Empty<T>()
      : System.Linq.Enumerable.Where(target, t => source.Contains(t));
  }
}
