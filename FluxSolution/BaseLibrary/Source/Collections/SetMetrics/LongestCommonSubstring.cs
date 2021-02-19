namespace Flux
{
	namespace SetMetrics
	{
		/// <summary>Finding the longest consecutive sequence of elements common to two or more sequences.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Longest_common_substring_problem" /
		/// <seealso cref="http://www.geeksforgeeks.org/longest-common-substring/"/>
		public class LongestCommonSubstring<T>
		: ASetMetrics<T>, IFullMatrix<T>, IMeasuredLength<T>
		{
			public LongestCommonSubstring()
				: this(System.Collections.Generic.EqualityComparer<T>.Default)
			{
			}
			public LongestCommonSubstring(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
				: base(equalityComparer)
			{
			}

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
			private int[,] GetFullMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, out int length, out int sourceMaxIndex, out int targetMaxIndex)
			{
				var lcsg = new int[source.Length + 1, target.Length + 1];

				length = 0;

				sourceMaxIndex = 0;
				targetMaxIndex = 0;

				for (var si = 0; si <= source.Length; si++)
				{
					for (var ti = 0; ti <= target.Length; ti++)
					{
						if (si > 0 && ti > 0 && EqualityComparer.Equals(source[si - 1], target[ti - 1]))
						{
							var temporaryLength = lcsg[si, ti] = lcsg[si - 1, ti - 1] + 1;

							if (temporaryLength > length)
							{
								length = temporaryLength;

								sourceMaxIndex = si;
								targetMaxIndex = ti;
							}
						}
						else
							lcsg[si, ti] = 0;
					}
				}

				return lcsg;
			}
			public int[,] GetFullMatrix(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
				=> GetFullMatrix(source, target, out var _, out var _, out var _);

			public System.Collections.Generic.IList<T> GetList(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
			{
				var lcs = new System.Collections.Generic.List<T>();

				var lcsg = GetFullMatrix(source, target, out var length, out var sourceIndex, out var targetIndex);

				if (length > 0)
				{
					while (lcsg[sourceIndex, targetIndex] != 0)
					{
						lcs.Insert(0, source[sourceIndex - 1]); // Can also use target[targetIndex - 1].

						sourceIndex--;
						targetIndex--;
					}
				}

				return lcs;
			}
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional

			public int GetMeasuredLength(System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
			{
				var maxLength = 0;

				var v1 = new int[target.Length + 1];
				var v0 = new int[target.Length + 1];

				for (var i = source.Length - 1; i >= 0; i--)
				{
					var swap = v1; v1 = v0; v0 = swap;

					for (var j = target.Length - 1; j >= 0; j--)
					{
						if (EqualityComparer.Equals(source[i], target[j]))
						{
							v0[j] = v1[j + 1] + 1;

							maxLength = System.Math.Max(maxLength, v0[j]);
						}
						else
						{
							v0[j] = 0;
						}
					}
				}

				return maxLength;
			}
		}
	}
}
