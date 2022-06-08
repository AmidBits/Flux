namespace Flux
{
  public static partial class SetOps
  {
    public static System.Text.Rune UnicodeSubsetOfOrEqualTo => (System.Text.Rune)0x2286;

    /// <summary>Determines whether the source set is a subset of the specified target set.</summary>
    public static bool IsSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
      => (source is System.Collections.Generic.ICollection<T> sc && !sc.Any()) // An empty set is a subset of any set.
      || (Counts(source, target, false, out var sourceCount) is var (unfoundCount, uniqueCount) && unfoundCount >= 0 && uniqueCount == sourceCount);
  }
}
