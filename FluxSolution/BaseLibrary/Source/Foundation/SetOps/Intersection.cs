namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements that are in both the source collection and the target sequence. I.e. all elements that both collections have in common.</summary>
    public static System.Collections.Generic.IEnumerable<T> Intersection<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      foreach (var t in target)
        if (source.Contains(t))
          yield return t;
    }
  }
}
