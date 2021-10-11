namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Returns the last indices of any specified characters found within the string builder. Uses the specified comparer.</summary>
		public static System.Collections.Generic.IDictionary<char, int> LastIndicesOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets, System.Collections.Generic.IEqualityComparer<char> equalityComparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

			var indices = new System.Collections.Generic.Dictionary<char, int>();

			var targetsCount = targets.Count;

			for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
				if (source[sourceIndex] is var sourceChar && !indices.ContainsKey(sourceChar))
					for (var targetIndex = 0; targetIndex < targetsCount; targetIndex++)
						if (equalityComparer.Equals(sourceChar, targets[targetIndex]))
							indices.Add(sourceChar, sourceIndex);

			return indices;
		}
		/// <summary>Returns the last indices of any specified characters found within the string builder. Uses the default comparer.</summary>
		public static System.Collections.Generic.IDictionary<char, int> LastIndicesOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets)
			=> LastIndicesOfAny(source, targets, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
