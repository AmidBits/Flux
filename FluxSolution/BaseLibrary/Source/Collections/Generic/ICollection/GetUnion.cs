namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence of all elements from both collections.</summary>
    public static System.Collections.Generic.IEnumerable<T> GetUnion<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => System.Linq.Enumerable.Concat(source, target);
  }
}
