namespace Flux
{
  public static partial class SetOps
  {
    public static System.Text.Rune UnicodeIntersection => (System.Text.Rune)0x2229;

    /// <summary>Creates a new sequence of elements that are in both the source collection and the target sequence. I.e. all elements that both collections have in common.</summary>
    public static System.Collections.Generic.IEnumerable<T> Intersection<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if ((source is System.Collections.Generic.ICollection<T> sc && sc.Count == 0) || (target is System.Collections.Generic.ICollection<T> tc && tc.Count == 0))
        return System.Linq.Enumerable.Empty<T>(); // If either sequence is empty, the result is empty.

      var ths = new System.Collections.Generic.HashSet<T>(target, equalityComparer);

      if(ths.Count == 0)
        return System.Linq.Enumerable.Empty<T>(); // If either sequence is empty, the result is empty.

      return source.Where(s => ths.Contains(s));
    }
    public static System.Collections.Generic.IEnumerable<T> Intersection<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => Intersection(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
