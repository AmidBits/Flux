//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    /// <summary>Returns a new hashset containing the symmetric difference between the two sequences. Uses the specified equality comparer.</summary>
//    /// <returns>The elements which are in either of the sets and not in their intersection.</returns>
//    /// <see cref="https://en.wikipedia.org/wiki/Symmetric_difference"/>
//    public static System.Collections.Generic.HashSet<TSource> SymmetricDifference<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEnumerable<TSource> target, System.Collections.Generic.IEqualityComparer<TSource> equalityComparer)
//    {
//      var difference = new System.Collections.Generic.HashSet<TSource>(source, equalityComparer);
//      difference.SymmetricExceptWith(target);
//      return difference;
//    }
//    /// <summary>Returns a new hashset containing the symmetric difference between the two sequences. Uses the default equality comparer.</summary>
//    /// <returns>The elements which are in either of the sets and not in their intersection.</returns>
//    /// <see cref="https://en.wikipedia.org/wiki/Symmetric_difference"/>
//    public static System.Collections.Generic.HashSet<TSource> SymmetricDifference<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEnumerable<TSource> target)
//      => SymmetricDifference(source, target, System.Collections.Generic.EqualityComparer<TSource>.Default);
//  }
//}
