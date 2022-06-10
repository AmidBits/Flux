//namespace Flux
//{
//  public static partial class ISet
//  {
//    /// <summary>Determines whether the source set is a proper (strict) superset of a specified target set.</summary>
//    public static bool IsProperSupersetOf<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
//      => source.Any() // An empty set is not a proper superset of any set.
//      && (
//        (target is System.Collections.Generic.ICollection<T> tc && !tc.Any()) // If target is an empty set then this is a superset.
//        || (source.Counts(target, true) is var (unfoundCount, uniqueCount) && unfoundCount == 0 && uniqueCount < source.Count)
//      );
//  }
//}
