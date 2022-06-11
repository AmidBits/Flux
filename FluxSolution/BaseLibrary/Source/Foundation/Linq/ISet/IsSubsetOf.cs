//namespace Flux
//{
//  public static partial class ISet
//  {
//    /// <summary>Determines whether the source set is a subset of the specified target set.</summary>
//    public static bool IsSubsetOf<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
//      => !source.Any() // An empty set is a subset of any set.
//      || (source.SetCounts(target, false) is var (unfoundCount, uniqueCount) && unfoundCount >= 0 && uniqueCount == source.Count);
//  }
//}
