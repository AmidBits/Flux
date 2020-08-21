using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns whether the source contains all of the items in subset, using the specified comparer.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset, System.Collections.Generic.IEqualityComparer<T> comparer)
      => !subset.Except(source, comparer).Any();
    /// <summary>Returns whether the source contains all of the items in subset, using the default comparer.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, params T[] subset)
      => !subset.Except(source).Any();

    /// <summary>Returns whether the source contains any of the items in subset, using the specified comparer.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset, System.Collections.Generic.IEqualityComparer<T> comparer)
      => subset.Any(item => source.Contains(item, comparer));
    /// <summary>Returns whether the source contains any of the items in subset.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, params T[] subset)
      => subset.Any(source.Contains);

    public const int FewMaximum = 9, FewMinimum = 3;

    /// <summary>Returns whether the source contains a few (3-9) of the items in the subset, using the specified comparer.</summary>
    public static bool ContainsFew<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (subset is null) throw new System.ArgumentNullException(nameof(subset));

      switch (source)
      {
        case null: throw new System.ArgumentNullException(nameof(source));
        case System.Collections.Generic.ICollection<T> ict: return ict.Count >= FewMinimum && ict.Count <= FewMaximum;
        case System.Collections.ICollection ic: return ic.Count >= FewMinimum && ic.Count <= FewMaximum;
        default:
          var e = source.GetEnumerator();
          int counter = 0, fewCutoff = FewMaximum + FewMinimum;
          while (counter < fewCutoff && e.MoveNext() && subset.Contains(e.Current, comparer))
            counter++;
          return counter >= FewMinimum && counter <= FewMaximum;
      }
    }
    /// <summary>Returns whether the source contains a few (3-9) of the items in the subset.</summary>
    public static bool ContainsFew<T>(this System.Collections.Generic.IEnumerable<T> source, params T[] subset)
      => ContainsFew(source, subset);
  }
}
