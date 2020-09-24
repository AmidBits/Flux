namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a new hashset containing the symmetric difference between the two sequences. Uses the specified equality comparer.</summary>
    /// <returns>The elements which are in either of the sets and not in their intersection.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Symmetric_difference"/>
    public static System.Collections.Generic.HashSet<T> SymmetricDifference<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      var difference = new System.Collections.Generic.HashSet<T>(source, comparer);

      difference.SymmetricExceptWith(target);

      return difference;
    }
    /// <summary>Returns a new hashset containing the symmetric difference between the two sequences. Uses the default equality comparer.</summary>
    /// <returns>The elements which are in either of the sets and not in their intersection.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Symmetric_difference"/>
    public static System.Collections.Generic.HashSet<T> SymmetricDifference<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => SymmetricDifference(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
