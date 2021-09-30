namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence of elements that are in both collections. I.e. all elements that both collections has in common.</summary>
    public static System.Collections.Generic.IEnumerable<T> GetIntersection<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      foreach (var t in target)
        if (source.Contains(t))
          yield return t;
    }
  }
}
