namespace Flux
{
	public static partial class SystemTextStringBuilderEm
	{
		/// <summary>Reports all first indices of the specified characters within the string builder (-1 if not found). Uses the specified comparer.</summary>
		public static int[] IndicesOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var indices = new int[targets.Count];

			System.Array.Fill(indices, -1);

			for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
			{
				var sourceChar = source[sourceIndex];

				for (var valueIndex = 0; valueIndex < targets.Count; valueIndex++)
				{
					if (indices[valueIndex] == -1 && comparer.Equals(sourceChar, targets[valueIndex]))
					{
						indices[valueIndex] = sourceIndex;

						if (!System.Array.Exists(indices, i => i == -1))
							return indices;
					}
				}
			}

			return indices;
		}
		/// <summary>Reports all first indices of the specified characters within the string builder (-1 if not found). Uses the default comparer.</summary>
		public static int[] IndicesOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets)
			=> IndicesOfAny(source, targets, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
