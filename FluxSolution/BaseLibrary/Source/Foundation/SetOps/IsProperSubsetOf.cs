namespace Flux
{
  public static partial class SetOps
  {
    public static System.Text.Rune UnicodeSubsetOf => (System.Text.Rune)0x2282;

    /// <summary>Determines whether the source set is a proper (strict) subset of a specified target set.</summary>
    public static bool IsProperSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => (source is System.Collections.Generic.ICollection<T> sc && !sc.Any() && target is System.Collections.Generic.ICollection<T> tc)
      ? (tc.Any()) // An empty set is a proper subset of anything but the empty set.
      : (Counts(source, target, false, out var sourceCount) is var (unfoundCount, uniqueCount) && unfoundCount > 0 && uniqueCount == sourceCount);
  }
}
