namespace Flux
{
  public static partial class ISet
  {
    /// <summary>Determines whether the source collection is equal to the specified target sequence. Using set equality: duplicates and order are ignored. Uses the specified equality comparer.</summary>
    public static bool Equality<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<T> target)
      => !(
      source.Any() is var sa &&
      target is System.Collections.Generic.ICollection<T> tc && tc.Any() is var ta &&
      ((!sa && ta) || (sa && !ta)) // If either set is empty and the other set is not, they cannot be equal.
      )
      && (source.Counts(target, true) is var (unfoundCount, uniqueCount) && unfoundCount == 0 && uniqueCount == source.Count);
  }
}
