using System.Linq;

namespace Flux
{
	public static partial class SpanMetricsEm
	{
		/// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences. Uses the specified comparer.</summary>
		/// <remarks>It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
		/// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
		public static int LongestCommonSubsequence<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> comparer)
			=> new SpanMetrics.LongestCommonSubsequence<T>(comparer).GetMetricLength((T[])source, (T[])target);
		/// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences. Uses the specified comparer.</summary>
		/// <remarks>It differs from problems of finding common substrings: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
		/// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
		public static int LongestCommonSubsequence<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
			=> new SpanMetrics.LongestCommonSubsequence<T>().GetMetricLength((T[])source, (T[])target);

		/// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences. Uses the specified comparer.</summary>
		/// <remarks>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
		/// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
		public static int LongestCommonSubsequence<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<T> comparer)
			=> new SpanMetrics.LongestCommonSubsequence<T>(comparer).GetMetricLength(source, target);
		/// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences. Uses the default comparer.</summary>
		/// <remarks>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
		/// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
		public static int LongestCommonSubsequence<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
			=> new SpanMetrics.LongestCommonSubsequence<T>().GetMetricLength(source, target);
	}

	namespace SpanMetrics
	{
		/// <summary>Finding the longest common subsequence (LCS) of two sequences. It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Longest_common_subsequence_problem"/> 
		/// <seealso cref="http://www.geeksforgeeks.org/longest-common-subsequence/"/>
		/// <seealso cref="https://www.ics.uci.edu/~eppstein/161/960229.html"/>
		/// <remarks>It differs from problems of finding common subsequences: unlike substrings, subsequences are not required to occupy consecutive positions within the original sequences.</remarks>
		/// <returns>The number of sequential characters, not necessarily consecutive, from source that occurs in target.</returns>
		public class LongestCommonSubsequence<T>
			: ASpanMetrics<T>, IFullMatrix<T>, IMetricDistance<T>, IMetricLength<T>, ISimpleMatchingCoefficient<T>, ISimpleMatchingDistance<T>
		{
			public LongestCommonSubsequence()
				: base(System.Collections.Generic.EqualityComparer<T>.Default)
			{
			}
			public LongestCommonSubsequence(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
				: base(equalityComparer)
			{
			}

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
			/// <summary></summary>
			public int[,] GetFullMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
			{
				var lcsg = new int[source.Length + 1, target.Length + 1];

				for (int si = 0; si <= source.Length; si++)
				{
					for (int ti = 0; ti <= target.Length; ti++)
					{
						if (si == 0 || ti == 0)
							lcsg[si, ti] = 0;
						else if (EqualityComparer.Equals(source[si - 1], target[ti - 1]))
							lcsg[si, ti] = lcsg[si - 1, ti - 1] + 1;
						else
							lcsg[si, ti] = System.Math.Max(lcsg[si - 1, ti], lcsg[si, ti - 1]);
					}
				}

				return lcsg;
			}

			/// <summary>Returns the items comprising the longest sub-sequence.</summary>
			public System.Collections.Generic.IList<T> GetList(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
			{
				var lcs = new System.Collections.Generic.List<T>();

				var lcsg = GetFullMatrix(source, target);

				var k = source.Length - 1;
				var l = target.Length - 1;

				while (k >= 0 && l >= 0)
				{
					if (EqualityComparer.Equals(source[k], target[l]))
					{
						lcs.Insert(0, source[k]);

						k--;
						l--;
					}
					else if (lcsg[k, l + 1] > lcsg[k + 1, l]) // If not same, then go in the direction of the greater one.
						k--;
					else
						l--;
				}

				return lcs;
			}
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional

			public int GetMetricDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
			=> source.Length + target.Length - 2 * GetMetricLength(source, target);

			public int GetMetricLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
			{
				OptimizeEnds(source, target, out source, out target, out var sourceCount, out var targetCount, out var equalAtStart, out var equalAtEnd);

				var v1 = new int[targetCount + 1];
				var v0 = new int[targetCount + 1];

				for (var i = sourceCount - 1; i >= 0; i--)
				{
					var swap = v1; v1 = v0; v0 = swap;

					for (var j = targetCount - 1; j >= 0; j--)
					{
						v0[j] = EqualityComparer.Equals(source[i], target[j]) ? v1[j + 1] + 1 : System.Math.Max(v1[j], v0[j + 1]);
					}
				}

				return v0[0] + equalAtStart + equalAtEnd;
			}

			public double GetSimpleMatchingCoefficient(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
				=> (double)GetMetricLength(source, target) / (double)System.Math.Max(source.Length, target.Length);

			public double GetSimpleMatchingDistance(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
				=> 1.0 - GetSimpleMatchingCoefficient(source, target);
		}
	}
}
