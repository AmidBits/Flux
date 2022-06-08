namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source collection is equal to the specified target sequence. Using set equality: duplicates and order are ignored. Uses the specified equality comparer.</summary>
    public static bool Equality<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => !(
      source is System.Collections.Generic.ICollection<T> sc &&
      target is System.Collections.Generic.ICollection<T> tc &&
      sc.Any() is var sa && tc.Any() is var ta && ((!sa && ta) || (sa && !ta)) // If either set is empty and the other set is not, they cannot be equal.
      )
      && (Counts(source, target, true, equalityComparer, out var sourceCount) is var (unfoundCount, uniqueCount) && unfoundCount == 0 && uniqueCount == sourceCount);
    /// <summary>Determines whether the source collection is equal to the specified target sequence. Using set equality: duplicates and order are ignored. Uses the default equality comparer.</summary>
    public static bool Equality<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => Equality(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
