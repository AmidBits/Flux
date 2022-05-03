namespace Flux
{
  public static partial class SetOps
  {
    /// <summary>Determines whether the source collection is equal to the specified target sequence. Using set equality: duplicates and order are ignored. Uses the specified equality comparer.</summary>
    public static bool Equality<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => !(target is System.Collections.Generic.ICollection<T> ic && ((source.Count == 0 && ic.Count > 0) || (source.Count > 0 && ic.Count == 0))) // If either set is empty and the other set is not, they cannot be equal.
      && (Counts(source, target, true, equalityComparer) is var (uniqueCount, unfoundCount) && unfoundCount == 0 && uniqueCount == source.Count);
    /// <summary>Determines whether the source collection is equal to the specified target sequence. Using set equality: duplicates and order are ignored. Uses the default equality comparer.</summary>
    public static bool Equality<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => Equality(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
