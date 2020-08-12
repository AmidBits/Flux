using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>The MostFreqKDistance is a string metric technique for quickly estimating how similar two ordered sets or strings are.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Most_frequent_k_characters#Most_frequent_K_hashing"/> 
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> MostFreqKHashing<T>(System.Collections.Generic.ICollection<T> source, int K, System.Collections.Generic.IEqualityComparer<T>? comparer = null)
    {
      foreach (var kvp in source.GroupBy(c => c, comparer).Select(g => new System.Collections.Generic.KeyValuePair<T, int>(g.Key, g.Count())).OrderByDescending(g => g.Value).Take(K))
      {
        yield return kvp;
      }
    }

    /// <summary>The MostFreqKDistance is a string metric technique for quickly estimating how similar two ordered sets or strings are, according to the specified comparer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Most_frequent_k_characters#String_distance_wrapper_function"/> 
    public static int MostFreqKSDF<T>(this System.Collections.Generic.ICollection<T> source, System.Collections.Generic.ICollection<T> target, int K, int maxDistance, System.Collections.Generic.IEqualityComparer<T>? comparer = null) => MostFreqKSimilarity(MostFreqKHashing(source, K, comparer), MostFreqKHashing(target, K, comparer), maxDistance, comparer);

    /// <summary>The MostFreqKDistance is a string metric technique for quickly estimating how similar two ordered sets or strings are.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Most_frequent_k_characters#Most_frequent_K_distance"/> 
    public static int MostFreqKSimilarity<T>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> hashing1, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<T, int>> hashing2, int limit, System.Collections.Generic.IEqualityComparer<T>? comparer = null)
    {
      if (hashing1 is null) throw new System.ArgumentNullException(nameof(hashing1));
      if (hashing2 is null) throw new System.ArgumentNullException(nameof(hashing2));

      comparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var similarity = 0;

      foreach (var hash1 in hashing1)
      {
        if (hashing2.Any(hash2 => comparer.Equals(hash1.Key, hash2.Key)))
        {
          similarity += hash1.Value;
        }
      }

      return limit - similarity;
    }
  }
}
