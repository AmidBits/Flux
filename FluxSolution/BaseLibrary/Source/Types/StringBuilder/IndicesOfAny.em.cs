namespace Flux
{
	public static partial class StringBuilderEm
	{
		/// <summary>Reports all first indices of the specified characters within the string builder (-1 if not found). Uses the specified comparer.</summary>
		public static System.Collections.Generic.IDictionary<char, int> IndicesOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets, [System.Diagnostics.CodeAnalysis.DisallowNull] System.Collections.Generic.IEqualityComparer<char> equalityComparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

			var indices = new System.Collections.Generic.Dictionary<char, int>();

			var sourceLength = source.Length;
			var targetsCount = targets.Count;

			for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
				if (source[sourceIndex] is var sourceChar && !indices.ContainsKey(sourceChar))
					for (var targetIndex = 0; targetIndex < targetsCount; targetIndex++)
						if (equalityComparer.Equals(sourceChar, targets[targetIndex]))
							indices.Add(sourceChar, sourceIndex);

			return indices;
		}
		/// <summary>Reports all first indices of the specified characters within the string builder (-1 if not found). Uses the default comparer.</summary>
		public static System.Collections.Generic.IDictionary<char, int> IndicesOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets)
			=> IndicesOfAny(source, targets, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}