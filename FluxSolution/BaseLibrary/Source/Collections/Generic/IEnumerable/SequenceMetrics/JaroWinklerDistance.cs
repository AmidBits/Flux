using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>The Jaro–Winkler distance is a string metric measuring an edit distance between two sequences. The lower the Jaro–Winkler distance for two strings is, the more similar the strings are. The score is normalized such that 0 means an exact match and 1 means there is no similarity. The Jaro–Winkler similarity is the inversion, (1 - Jaro–Winkler distance).</summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="boostThreshold">The minimum score for a string that gets boosted. This value was set to 0.7 in Winkler's papers.</param>
    /// <param name="prefixSize">The size of the initial prefix considered. This value was set to 4 in Winkler's papers.</param>
    /// <param name="comparer"></param>
    /// <see cref="https://en.wikipedia.org/wiki/Jaro–Winkler_distance"/>
    /// <seealso cref="http://alias-i.com/lingpipe/docs/api/com/aliasi/spell/JaroWinklerDistance.html"/>
    public static double JaroWinklerDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T> comparer, double boostThreshold = 0.7, int prefixSize = 4)
      => new SequenceMetrics.JaroWinklerDistance<T>() { BoostThreshold = boostThreshold, PrefixSize = prefixSize }.GetNormalizedDistance(source.ToArray(), target.ToArray(), comparer);
    /// <summary>The Jaro–Winkler distance is a string metric measuring an edit distance between two sequences. The lower the Jaro–Winkler distance for two strings is, the more similar the strings are. The score is normalized such that 0 means an exact match and 1 means there is no similarity. The Jaro–Winkler similarity is the inversion, (1 - Jaro–Winkler distance).</summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="boostThreshold">The minimum score for a string that gets boosted. This value was set to 0.7 in Winkler's papers.</param>
    /// <param name="prefixSize">The size of the initial prefix considered. This value was set to 4 in Winkler's papers.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Jaro–Winkler_distance"/>
    /// <seealso cref="http://alias-i.com/lingpipe/docs/api/com/aliasi/spell/JaroWinklerDistance.html"/>
    public static double JaroWinklerDistance<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, double boostThreshold = 0.7, int prefixSize = 4)
      => new SequenceMetrics.JaroWinklerDistance<T>() { BoostThreshold = boostThreshold, PrefixSize = prefixSize }.GetNormalizedDistance(source.ToArray(), target.ToArray(), System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
