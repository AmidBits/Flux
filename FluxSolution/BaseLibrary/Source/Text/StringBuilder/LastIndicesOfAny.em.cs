namespace Flux
{
	public static partial class SystemTextEm
	{
		/// <summary>Returns all last indices of the specified characters within the string builder (-1 if not found). Uses the specified comparer.</summary>
		public static int[] LastIndicesOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			var lastIndices = new int[targets.Count];

			System.Array.Fill(lastIndices, -1);

			for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
			{
				var sourceChar = source[sourceIndex];

				for (var valueIndex = targets.Count - 1; valueIndex >= 0; valueIndex--)
				{
					if (lastIndices[valueIndex] == -1 && comparer.Equals(sourceChar, targets[valueIndex]))
					{
						lastIndices[valueIndex] = sourceIndex;

						if (!System.Array.Exists(lastIndices, i => i == -1))
							return lastIndices;
					}
				}
			}

			return lastIndices;
		}
		/// <summary>Returns all last indices of the specified characters within the string builder (-1 if not found). Uses the default comparer.</summary>
		public static int[] LastIndicesOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets)
			=> LastIndicesOfAny(source, targets, System.Collections.Generic.EqualityComparer<char>.Default);
	}
}
