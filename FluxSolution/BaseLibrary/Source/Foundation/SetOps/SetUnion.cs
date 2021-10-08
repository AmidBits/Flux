namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Creates a new sequence of all (unique) elements from both the source set and the specified target set.</summary>
    public static System.Collections.Generic.IEnumerable<T> SetUnion<T>(System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => System.Linq.Enumerable.Concat(source, System.Linq.Enumerable.Where(target, t => !source.Contains(t)));
  }
}
