namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence of elements that are in the specified sequence but not in the collection.</summary>
    public static System.Collections.Generic.IEnumerable<T> GetDifferenceInTarget<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      foreach (var t in target)
        if (!source.Contains(t))
          yield return t;
    }
  }
}
