//namespace Flux
//{
//  public static partial class ISet
//  {
//    /// <summary>Determines whether the source collection overlaps with the specified target sequence.</summary>
//    public static bool Overlaps<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
//      => !(
//        !source.Any() ||
//        (target is System.Collections.Generic.ICollection<T> tc && !tc.Any())
//      ) // If either set is empty, there can be no overlap.
//      && source.ContainsAny(target);
//  }
//}
