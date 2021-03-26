namespace Flux
{
	namespace Metrics
	{
		/// <summary>The Jaro–Winkler distance is a string metric measuring an edit distance between two sequences. The lower the Jaro–Winkler distance for two sequences is, the more similar the sequences are. The score is normalized such that 0 means an exact match and 1 means there is no similarity. The Jaro–Winkler similarity is the inversion, (1 - Jaro–Winkler distance).</summary>
		/// <param name="boostThreshold">The minimum score for a sequence that gets boosted. This value was set to 0.7 in Winkler's papers.</param>
		/// <param name="prefixSize">The size of the initial prefix considered. This value was set to 4 in Winkler's papers.</param>
		/// <see cref="https://en.wikipedia.org/wiki/Jaro–Winkler_distance"/>
		/// <seealso cref="http://alias-i.com/lingpipe/docs/api/com/aliasi/spell/JaroWinklerDistance.html"/>
		public class JaroWinklerDistance<T>
			: AMetrics<T>, INormalizedDistance<T>
		{
			/// <summary>BoostThreshold is the minimum score for a sequence that gets boosted. This value was set to 0.7 in Winkler's papers.</summary>
			public double BoostThreshold { get; set; } = 0.7;
			/// <summary>PrefixSize is the size of the initial prefix considered. This value was set to 4 in Winkler's papers.</summary>
			public int PrefixSize { get; set; } = 4;

			public JaroWinklerDistance()
				: base(System.Collections.Generic.EqualityComparer<T>.Default)
			{
			}
			public JaroWinklerDistance(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
				: base(equalityComparer)
			{
			}

			public double GetNormalizedDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
				=> 1 - GetNormalizedSimilarity(source, target);

			public double GetNormalizedSimilarity(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
			{
				var sourceCount = source.Length;
				var targetCount = target.Length;

				if (sourceCount == 0) return targetCount == 0 ? 1 : 0;
				else if (targetCount == 0) return sourceCount == 0 ? 1 : 0;

				var sourceMatches = new System.Collections.BitArray(sourceCount, false);
				var targetMatches = new System.Collections.BitArray(targetCount, false);

				// Step 1: Matches. The match phase is a greedy alignment step of items in the source sequence against the items in the target sequence.

				var matches = 0;
				var matchDistance = System.Math.Max(sourceCount, targetCount) / 2 - 1; // The maximum distance at which items may be matched.

				for (var i = 0; i < sourceCount; i++)
				{
					var start = System.Math.Max(0, i - matchDistance);
					var end = System.Math.Min(i + matchDistance + 1, targetCount);

					for (var j = start; j < end; j++)
					{
						if (targetMatches[j] || !EqualityComparer.Equals(source[i], target[j])) continue;

						sourceMatches[i] = true;
						targetMatches[j] = true;

						matches++;

						break;
					}
				}

				if (matches == 0) return 0; // Return complete mismatch.

				// Step 2, Transpositions. The subsequence of items actually matched in both sequences are counted and will be the same length. 

				var transpositions = 0; // The number of items in the source sequence that do not line up (by index in the matched subsequence) with identical items in the target sequence is the number of "half transpositions".

				for (int i = 0, j = 0; i < sourceCount; i++)
				{
					if (!sourceMatches[i]) continue;

					while (!targetMatches[j]) j++;

					if (!EqualityComparer.Equals(source[i], target[j])) transpositions++;

					j++;
				}

				transpositions /= 2; // The total number of transpositons is the number of half transpositions divided by two, rounding down.

				var score = ((double)matches / sourceCount + (double)matches / targetCount + (matches - transpositions) / matches) / 3; // The measure is the average of; the percentage of the first string matched, the percentage of the second string matched, and the percentage of matches that were not transposed.

				if (score <= BoostThreshold) return score; // Below boost threshold, return Jaro distance score unmodified.

				// Step 3: Winkler Modification. The Winkler modification to the Jaro comparison, resulting in the Jaro-Winkler comparison, boosts scores for strings that match character for character initially.
				// Let boostThreshold be 
				// The second parameter for the Winkler modification is 
				// If the Jaro score is below the boost threshold, or if the prefixCount is zero, the Jaro score is returned unadjusted.

				int initialMatches = 0, maxLength = System.Math.Min(System.Math.Min(PrefixSize, sourceCount), targetCount);
				while (initialMatches < maxLength && EqualityComparer.Equals(source[initialMatches], target[initialMatches]))
					initialMatches++;

				if (initialMatches == 0)
					return score; // No initial match, return Jaro distance score unmodified.

				return score + 0.1 * initialMatches * (1 - score); // Return the Winkler modified distance score.
			}
		}
	}
}
