//namespace Flux
//{
//  public static partial class ISet
//  {
//    /// <summary>Determines whether the source set is a superset of a specified target set.</summary>
//    public static bool IsSupersetOf<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
//      => (target is System.Collections.Generic.ICollection<T> tc && !tc.Any()) // If target is an empty set then source is a superset.
//      || source.ContainsAll(target);
//  }
//}