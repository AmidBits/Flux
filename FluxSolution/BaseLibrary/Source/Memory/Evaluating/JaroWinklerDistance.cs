//namespace Flux.Metrical
//{
//  /// <summary>
//  /// <para>The Jaro–Winkler distance is a string metric measuring an edit distance between two sequences. The lower the Jaro–Winkler distance for two sequences is, the more similar the sequences are. The score is normalized such that 0 means an exact match and 1 means there is no similarity. The Jaro–Winkler similarity is the inversion, (1 - Jaro–Winkler distance).</para>
//  /// <para><see href="https://en.wikipedia.org/wiki/Jaro-Winkler_distance"/></para>
//  /// <para><seealso href="https://stackoverflow.com/questions/19123506/jaro-winkler-distance-algorithm-in-c-sharp"/></para>
//  /// <para><seealso href="http://alias-i.com/lingpipe/docs/api/com/aliasi/spell/JaroWinklerDistance.html"/></para>
//  /// </summary>
//  public sealed class JaroWinklerDistance<T>
//  {
//    private double m_boostThreshold = 0.7;
//    private int m_prefixSize = 4;

//    public System.Collections.Generic.IEqualityComparer<T> EqualityComparer { get; }

//    public JaroWinklerDistance(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
//      => EqualityComparer = equalityComparer ?? throw new System.ArgumentNullException(nameof(equalityComparer));
//    public JaroWinklerDistance()
//      : this(System.Collections.Generic.EqualityComparer<T>.Default)
//    { }

//    /// <summary>BoostThreshold is the minimum score for a sequence that gets boosted. This value was set to 0.7 in Winkler's papers.</summary>
//    public double BoostThreshold { get => m_boostThreshold; set => m_boostThreshold = value; }

//    /// <summary>PrefixSize is the size of the initial prefix considered. This value was set to 4 in Winkler's papers.</summary>
//    public int PrefixSize { get => m_prefixSize; set => m_prefixSize = value; }

//    /// <summary>Computes a normalized edit distance between <paramref name="source"/> and <paramref name="target"/>, i.e. (1.0 - GetNormalizedSimilarity(a, b)), which is a value in the range [0, 1] (from less to more edits).</summary>
//    public double GetNormalizedDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
//      => 1 - GetNormalizedSimilarity(source, target);

//    /// <summary>Computes the Jaro-Winkler similarity between <paramref name="source"/> and <paramref name="target"/>, which is a normalized value in the range [0, 1] (from less to greater match).</summary>
//    public double GetNormalizedSimilarity(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
//    {
//      var sourceCount = source.Length;
//      var targetCount = target.Length;

//      if (sourceCount == 0) return targetCount == 0 ? 1 : 0;
//      else if (targetCount == 0) return sourceCount == 0 ? 1 : 0;

//      var sourceFlags = new System.Collections.BitArray(sourceCount, false);
//      var targetFlags = new System.Collections.BitArray(targetCount, false);

//      // Step 1: Matches. The match phase is a greedy alignment step of items in the source sequence against the items in the target sequence.

//      var matches = 0;
//      var searchRange = int.Max(sourceCount, targetCount) / 2 - 1; // The maximum distance at which items may be matched.

//      for (var i = 0; i < sourceCount; i++)
//      {
//        var loLimit = int.Max(0, i - searchRange);
//        var hiLimit = int.Min(i + searchRange + 1, targetCount);

//        for (var j = loLimit; j < hiLimit; j++)
//        {
//          if (targetFlags[j] || !EqualityComparer.Equals(source[i], target[j]))
//            continue;

//          sourceFlags[i] = true;
//          targetFlags[j] = true;

//          matches++;

//          break;
//        }
//      }

//      if (matches == 0)
//        return 0; // Return complete mismatch.

//      // Step 2, Transpositions. The subsequence of items actually matched in both sequences are counted and will be the same length. 

//      var transpositions = 0; // The number of items in the source sequence that do not line up (by index in the matched subsequence) with identical items in the target sequence is the number of "half transpositions".

//      for (int i = 0, j = 0; i < sourceCount; i++)
//      {
//        if (!sourceFlags[i])
//          continue;

//        while (!targetFlags[j])
//          j++;

//        if (!EqualityComparer.Equals(source[i], target[j]))
//          transpositions++;

//        j++;
//      }

//      transpositions /= 2; // The total number of transpositons is the number of half transpositions divided by two, rounding down.

//      var score = ((double)matches / sourceCount + (double)matches / targetCount + (matches - transpositions) / (double)matches) / 3d; // The measure is the average of; the percentage of the first string matched, the percentage of the second string matched, and the percentage of matches that were not transposed.

//      if (score <= m_boostThreshold)
//        return score; // Below boost threshold, return Jaro distance score unmodified.

//      // Step 3: Winkler Modification. The Winkler modification to the Jaro comparison, resulting in the Jaro-Winkler comparison, boosts scores for strings that match character for character initially.
//      // Let BoostThreshold be 
//      // The second parameter for the Winkler modification is 
//      // If the Jaro score is below the boost threshold, or if the prefixCount is zero, the Jaro score is returned unadjusted.

//      int prefixCount = 0, maxLength = int.Min(int.Min(m_prefixSize, sourceCount), targetCount);
//      while (prefixCount < maxLength && EqualityComparer.Equals(source[prefixCount], target[prefixCount]))
//        prefixCount++;

//      if (prefixCount == 0)
//        return score; // No initial match, return Jaro distance score unmodified.

//      return score + 0.1 * prefixCount * (1d - score); // Return the Winkler modified distance score.
//    }
//  }
//}
