namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of elements that are in the specified target set but not in the source set.</summary>
    public static System.Collections.Generic.IEnumerable<T> TargetDifference<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
    {
      foreach (var t in target)
        if (!source.Contains(t))
          yield return t;
    }
  }
}
