using System.Linq;

namespace Flux
{
  public static partial class SystemCollectionsGenericEm
  {
    /// <summary>Returns whether the source contains all of the items in subset, using the specified comparer.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset, System.Collections.Generic.IEqualityComparer<T> comparer)
      => !subset.Except(source, comparer).Any();
    /// <summary>Returns whether the source contains all of the items in subset, using the default comparer.</summary>
    public static bool ContainsAll<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset)
      => ContainsAll(source, subset, System.Collections.Generic.EqualityComparer<T>.Default);

    /// <summary>Returns whether the source contains any of the items in subset, using the specified comparer.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset, System.Collections.Generic.IEqualityComparer<T> comparer)
      => subset.Any(item => source.Contains(item, comparer));
    /// <summary>Returns whether the source contains any of the items in subset.</summary>
    public static bool ContainsAny<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset)
      => ContainsAny(source, subset, System.Collections.Generic.EqualityComparer<T>.Default);

    public const int FewMaximum = 8, FewMinimum = 3;

    /// <summary>Returns whether the source contains a few (3-8) of the items in the subset, using the specified comparer. NOTE! Elements are counted as many times as they exists in the source.</summary>
    public static bool ContainsFew<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (subset is null) throw new System.ArgumentNullException(nameof(subset));

      using var e = source.GetEnumerator();

      for (int counter = 0, fewCutoff = FewMaximum + FewMinimum; counter < fewCutoff && e.MoveNext();)
      {
        if (subset.Contains(e.Current, comparer))
        {
          counter++;

          if (counter >= FewMinimum && counter <= FewMaximum)
            return true;
        }
      }

      return false;
    }
    /// <summary>Returns whether the source contains a few (3-8) of the items in the subset, using the specified comparer. NOTE! Elements are counted as many times as they exists in the source.</summary>
    public static bool ContainsFew<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> subset)
      => ContainsFew(source, subset, System.Collections.Generic.EqualityComparer<T>.Default);
    ///// <summary>Returns whether the source contains a few (3-9) of the items in the subset.</summary>
    //public static bool ContainsFew<T>(this System.Collections.Generic.IEnumerable<T> source, params T[] subset)
    //  => ContainsFew(source, subset, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
