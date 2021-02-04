namespace Flux
{
	public static partial class SystemTextEm
	{
		/// <summary>Returns the last index of the occurence of value in the string. Or -1 if not found.</summary>
		public static int LastIndexOf(this System.Text.StringBuilder source, char target, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (var index = source.Length - 1; index >= 0; index--)
				if (comparer.Equals(source[index], target))
					return index;

			return -1;
		}
		/// <summary>Returns the last index of the occurence of value in the string. Or -1 if not found.</summary>
		public static int LastIndexOf(this System.Text.StringBuilder source, char target)
			=> LastIndexOf(source, target, System.Collections.Generic.EqualityComparer<char>.Default);

		/// <summary>Returns the last index of the occurence of value in the string. Or -1 if not found.</summary>
		public static int LastIndexOf(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (int index = source.Length - target.Length; index >= 0; index--)
				if (Equals(source, index, target, comparer))
					return index;

			return -1;
		}
		/// <summary>Returns the last index of the occurence of value in the string. Or -1 if not found.</summary>
		public static int LastIndexOf(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target)
			=> LastIndexOf(source, target, System.Collections.Generic.EqualityComparer<char>.Default);

		/// <summary>Returns the last index of any of the specified characters. Or -1 if none were found.</summary>
		public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
				for (var valueIndex = 0; valueIndex < targets.Count; valueIndex++)
					if (comparer.Equals(source[sourceIndex], targets[valueIndex]))
						return sourceIndex;

			return -1;
		}
		/// <summary>Returns the last index of any of the specified characters. Or -1 if none were found.</summary>
		public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets)
			=> LastIndexOfAny(source, targets, System.Collections.Generic.EqualityComparer<char>.Default);

		/// <summary>Returns the last index of any of the specified values. or -1 if none is found.</summary>
		public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<string> targets, System.Collections.Generic.IEqualityComparer<char> comparer)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
				for (var valueIndex = targets.Count - 1; valueIndex >= 0; valueIndex--)
					if (Equals(source, sourceIndex, targets[valueIndex], comparer))
						return sourceIndex;

			return -1;
		}
		/// <summary>Returns the last index of any of the specified values. or -1 if none is found.</summary>
		public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<string> targets)
			=> LastIndexOfAny(source, targets, System.Collections.Generic.EqualityComparer<char>.Default);

		/// <summary>Returns all last indices of the specified characters within the string builder (-1 if not found). Uses the specified comparer.</summary>
		public static int[] LastIndicesOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets, System.Collections.Generic.IEqualityComparer<char>? comparer = null)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (targets is null) throw new System.ArgumentNullException(nameof(targets));

			comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

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
	}
}
