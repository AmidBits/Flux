namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of all elements (even duplicates) from both the source set and the specified target set.</summary>
    public static System.Collections.Generic.IEnumerable<T> SetUnionAll<T>(System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => System.Linq.Enumerable.Concat(source, target);
  }
}
