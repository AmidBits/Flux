namespace Flux
{
  public static partial class SetOps
  {
    public static System.Text.Rune UnicodeSupersetOf => (System.Text.Rune)0x2283;

    /// <summary>Determines whether the source set is a proper (strict) superset of a specified target set.</summary>
    public static bool IsProperSupersetOf<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => (source is System.Collections.Generic.ICollection<T> sc && sc.Any()) // An empty set is not a proper superset of any set.
      && (
        (target is System.Collections.Generic.ICollection<T> tc && !tc.Any()) // If target is an empty set then this is a superset.
        || (Counts(source, target, true, out var sourceCount) is var (unfoundCount, uniqueCount) && unfoundCount == 0 && uniqueCount < sourceCount)
      );
  }
}
