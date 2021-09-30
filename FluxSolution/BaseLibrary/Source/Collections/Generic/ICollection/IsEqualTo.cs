namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Determines whether the current set is equal to the specified collection. Using set equality: duplicates and order are ignored. Uses the specified equality comparer.</summary>
    public static bool IsEqualTo<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
      => GetCounts(source, target, true, equalityComparer) is var (uniqueCount, unfoundCount) && unfoundCount == 0 && uniqueCount == source.Count;
    /// <summary>Determines whether the current set is equal to the specified collection. Using set equality: duplicates and order are ignored. Uses the default equality comparer.</summary>
    public static bool IsEqualTo<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.IEnumerable<T> target)
      => IsEqualTo(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
